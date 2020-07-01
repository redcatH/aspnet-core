using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
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
        private readonly IAppDefinitionManager _appDefinitionManager;

        public ProductCategoryAppService(IRepository<ProductCategory, Guid> repository, IAppDefinitionManager appDefinitionManager) : base(repository)
        {
            this._categories = repository;
            _appDefinitionManager = appDefinitionManager;
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
            var find = await _categories.Include(p=>p.AppProductCategories).FirstOrDefaultAsync(p => p.Id == id);
            var apps= await _appDefinitionManager.GetList();
            
            return new GetForEditOutput<ProductCategoryCreateOrUpdateDto>(ObjectMapper.Map<ProductCategory, ProductCategoryCreateOrUpdateDto>(find ?? new ProductCategory()));
        }



        //public async Task<ProductCategoryDto> CreateAsync(ProductCategoryCreateOrUpdateDto input)
        //{
        //    var categorie=await _categories.Include(p=>p.AppProductCategories).FirstOrDefaultAsync(p => p.Name == input.Id);
        //    if(categorie==null)
        //        new 
        //}


    }

    public interface IProductCategoryAppService
    {

    }
}