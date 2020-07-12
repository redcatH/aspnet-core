using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Redcat.Abp.AppManagement.Apps;
using Redcat.Abp.Core;
using Redcat.Abp.Mall.Application.Contracts;
using Redcat.Abp.Mall.Domain;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Redcat.Abp.Mall.Application.Category
{
    public class ProductCategoryAppService:CrudAppService<ProductCategory,ProductCategoryDto,Guid,PagedAndSortedResultRequestDto,ProductCategoryCreateOrUpdateDto,ProductCategoryCreateOrUpdateDto>, IProductCategoryAppService
    {
        private readonly IRepository<ProductCategory, Guid> _categories;
        private readonly IRepository<AppProductCategory> _app;
        private readonly IAppDefinitionManager _appDefinitionManager;

        public ProductCategoryAppService(IRepository<ProductCategory, Guid> repository, IAppDefinitionManager appDefinitionManager, IRepository<AppProductCategory> app) : base(repository)
        {
            this._categories = repository;
            _appDefinitionManager = appDefinitionManager;
            _app = app;
            ObjectMapperContext = typeof(MallModule);
        }
        ///// <summary>
        ///// 返回所有分类
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<PagedResultDto<ProductCategoryDto>> GetList(PagedResultRequestDto input)
        //{
        //    var count = await _categories.GetCountAsync();
        //    var list = await _categories.PageBy(input).ToListAsync();
        //    var listDto = ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryDto>>(list);
        //    return new PagedResultDto<ProductCategoryDto>(count, listDto);
        //}

        public async Task<GetForEditOutput<ProductCategoryCreateOrUpdateDto>> GetForEdit(Guid id)
        {
            var find = await _categories.Include(p => p.AppProductCategories).FirstOrDefaultAsync(p => p.Id == id);
            var apps = await _appDefinitionManager.GetList();
            var ori = find ?? new ProductCategory();
            var schema = new JObject() { };
            const string jKey = "name";
            schema["apps"] = apps.GetJArray(jKey);
            if (schema["apps"] is JArray)
            {
                foreach (var item in schema["apps"])
                {
                    item["checked"] = false;
                }
            }

            if (find?.AppProductCategories == null && !(find?.AppProductCategories?.Count > 0))
            {
                return new GetForEditOutput<ProductCategoryCreateOrUpdateDto>(
                    ObjectMapper.Map<ProductCategory, ProductCategoryCreateOrUpdateDto>(find ?? new ProductCategory()),
                    schema);
            }

            {
                foreach (var app in find.AppProductCategories)
                {
                    var temp = schema["apps"]?.FirstOrDefault(p => p[jKey]?.ToString() == app.AppName);
                    if (temp != null)
                    {
                        temp["checked"] = true;
                    }
                }
            }

            return new GetForEditOutput<ProductCategoryCreateOrUpdateDto>(
                ObjectMapper.Map<ProductCategory, ProductCategoryCreateOrUpdateDto>(find ?? new ProductCategory()),
                schema);
        }


        protected override IQueryable<ProductCategory> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.ReadOnlyRepository.Include(p => p.AppProductCategories);
        }

        public override async Task<ProductCategoryDto> UpdateAsync(Guid id, ProductCategoryCreateOrUpdateDto input)
        {
            await base.CheckUpdatePolicyAsync();
            var entity= await _categories.Include(p => p.AppProductCategories).FirstOrDefaultAsync(p => p.Id == id);

            var enumerable = input.apps.ToDynamicList();
            foreach (var item in enumerable)
            {
                var app= await this._appDefinitionManager.GetList();
                var first = app.FirstOrDefault(p => p.name == item.Value<string>("label"));
                var label = item.Value<string>("label");
                var isChecked= item.Value<bool>("checked");
                var appEntity = entity?.AppProductCategories.FirstOrDefault(p => p.AppName == label);

                if (first==null)
                {
                    continue;
                }
                if (isChecked)
                {
                    if (appEntity==null)
                    {
                        await _app.InsertAsync(new AppProductCategory(first.name, entity.TenantId, id), true);
                    }
                }
                else
                {
                    if (appEntity != null)
                    {
                        await _app.DeleteAsync(appEntity);
                    }
                }
            }
            base.MapToEntity(input, entity);
            return base.MapToGetOutputDto(entity);
        }

        public  override async Task<ProductCategoryDto> CreateAsync(ProductCategoryCreateOrUpdateDto input)
        {
            await CheckCreatePolicyAsync();

            var entity = MapToEntity(input);
            TryToSetTenantId(entity);
            await Repository.InsertAsync(entity, autoSave: true);

            var enumerable = input.apps?.Where(p => p?.Value<bool>("checked") == true);
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    var app = await this._appDefinitionManager.GetList();
                    var first = app.FirstOrDefault(p => p.name == item.Value<string>("name"));
                    if (first != null)
                    {
                        await _app.InsertAsync(new AppProductCategory(first.name, entity.TenantId, entity.Id), true);
                    }
                }
            }
            return MapToGetOutputDto(entity);
        }
    }

    public static class SchemaEx
    {
        public static JArray GetJArray<T>(this IEnumerable<T> list, params string[] keys)
        {
            var jArray=new JArray();
            foreach (var i in list)
            {
                var obj = new JObject { };
                foreach (var key in keys)
                {
                    obj.TryAdd(key, typeof(T).GetProperty(key)?.GetValue(i)?.ToString());
                }
                jArray.Add(obj);
            }

            return jArray;
        }
    }
}