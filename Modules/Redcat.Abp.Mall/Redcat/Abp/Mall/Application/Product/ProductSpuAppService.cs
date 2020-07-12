using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Redcat.Abp.Core;
using Redcat.Abp.Mall.Application.Category;
using Redcat.Abp.Mall.Application.Contracts;
using Redcat.Abp.Mall.Domain;
using Redcat.Abp.Mall.Domain.Repository;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Mall.Application.Product
{
    public class ProductSpuAppService :CrudAppService<ProductSpu, ProductSpuDto,Guid,MallRequestDto ,ProductSpuCreateOrUpdateDto, ProductSpuCreateOrUpdateDto>, IProductSpuAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<ProductSpu, Guid> _spuRepository;
        private readonly IRepository<MallShop, Guid> _mallShopRepository;
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;
        private readonly IRepository<ProductSku, Guid> _skuRepository;
        private readonly IMallShopLookupService _mallShopLookupService;

        public ProductSpuAppService(IRepository<ProductSpu, Guid> repository, IRepository<MallShop, Guid> mallShopRepository,  IRepository<ProductCategory, Guid> productCategoryRepository, IRepository<ProductSku, Guid> skuRepository, IGuidGenerator guidGenerator, IMallShopLookupService mallShopLookupService) : base(repository)
        {
            _spuRepository = repository;
            _mallShopRepository = mallShopRepository;
            _productCategoryRepository = productCategoryRepository;
            _skuRepository = skuRepository;
            _guidGenerator = guidGenerator;
            _mallShopLookupService = mallShopLookupService;
            ObjectMapperContext = typeof(MallModule);
        }

        public async Task<GetForEditOutput<ProductSpuCreateOrUpdateDto>> GetForEdit(Guid id)
        {
            var spu = await _spuRepository.Include(p => p.ProductSkus).Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(p => p.Id == id) ??
                      new ProductSpu()
                      {
                          ProductSkus = new List<ProductSku>(){new ProductSku()}
                      };
            var shops = await _mallShopRepository.GetListAsync();
            var productCategory = await _productCategoryRepository.GetListAsync();
            var json = new JObject() { };
            json["shops"] = shops.GetJArray("Name", "Id");
            json["category"] = productCategory.GetJArray("ShortName", "Id");
            var dto = ObjectMapper.Map<ProductSpu, ProductSpuCreateOrUpdateDto>(spu);
            return new GetForEditOutput<ProductSpuCreateOrUpdateDto>(dto, json);
        }

        public override async Task<ProductSpuDto> UpdateAsync(Guid id, ProductSpuCreateOrUpdateDto input)
        {
            await base.CheckUpdatePolicyAsync();
            if (input.ProductSkus.Count == 0)
            {
                throw new UserFriendlyException("至少有一个Sku");
            }
            if (await _spuRepository.AnyAsync(p =>
                p.Code == input.Code || (p.CategoryId == input.CategoryId && p.Name == input.Name) && p.Id != id))
            {
                throw new UserFriendlyException("同一分类下已有同名称商品 或 商品代码错误");
            }

            var find = await Repository.Include(p => p.ProductSkus).FirstOrDefaultAsync(p => p.Id == id); 
            MapToEntity(input,find);
            
            await Repository.UpdateAsync(find, autoSave: true);

            if(find==null)
            {
                throw new UserFriendlyException("");
            }
            var ids = find.ProductSkus?.Select(p => p.Id).ToList();
            foreach (var inputSku in input.ProductSkus)
            {
                var findSku = await _skuRepository.FirstOrDefaultAsync(p => p.Name == inputSku.Name);
                if (findSku!=null)
                {
                    ids.Remove(findSku.Id);
                    ObjectMapper.Map(inputSku, findSku);
                    await _skuRepository.UpdateAsync(findSku, true);
                }else
                {
                    
                    var skuEntity = ObjectMapper.Map<ProductSkuCreateOrUpdate, ProductSku>(inputSku);
                    skuEntity.SpuId = find.Id;
                    skuEntity.SetTenantId(find.TenantId);
                    skuEntity.SetEntityGuid(_guidGenerator);
                    await _skuRepository.InsertAsync(skuEntity, true);
                }
            }
            foreach (var item in ids)
            {
               await _skuRepository.DeleteAsync(item, true);
            }
            return MapToGetOutputDto(find);
        }

        public override async Task<ProductSpuDto> CreateAsync(ProductSpuCreateOrUpdateDto input)
        {
            await base.CheckCreatePolicyAsync();

            var find = await _spuRepository.AnyAsync(p => p.Code == input.Code || (input.CategoryId == p.CategoryId));
            if (find)
            {
                throw new UserFriendlyException("同一分类下已有同名称商品 或 商品代码错误");
            }

            var spu = MapToEntity(input);
            TryToSetTenantId(spu);
            await Repository.InsertAsync(spu);
            foreach (var inputSku in input.ProductSkus)
            {
                //inputSku.SpuId = spu.Id;

                var sku = ObjectMapper.Map<ProductSkuCreateOrUpdate, ProductSku>(inputSku);
                sku.SpuId = spu.Id;
                sku.SetEntityGuid(_guidGenerator);
                sku.SetTenantId(spu.TenantId);
                await _skuRepository.InsertAsync(sku);
            }
            return MapToGetOutputDto(spu);
        }

        public override async Task<PagedResultDto<ProductSpuDto>> GetListAsync(MallRequestDto input)
        {
            await base.CheckGetListPolicyAsync();
            var pageDto=await base.GetListAsync(input);
            foreach (var item in pageDto.Items)
            {
                if (item.shopId.HasValue)
                {
                   var shop=await _mallShopLookupService.FindShopById((Guid)item.shopId);
                   if(shop==null)
                   {
                       continue;
                   }

                   item.Shop = ObjectMapper.Map<MallShop, MallShopDto>(shop);
                }
            }
            return pageDto;
        }


        
        protected override IQueryable<ProductSpu> CreateFilteredQuery(MallRequestDto input)
        {
            Repository.Include(p => p.ProductSkus).Include(p => p.ProductCategory)
                .IncludeIf(input.ShopId.HasValue, p => p.ShopId == input.ShopId);
            return base.CreateFilteredQuery(input);
        }
    }

    public class MallRequestDto: PagedAndSortedResultRequestDto
    {
        public Guid? ShopId { get; set; }
    }
    public interface IProductSpuAppService
    {

    }

    public class ProductSkuCreateOrUpdate
    {
        //public Guid SpuId { get; set; }
        //public Guid Id { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal? OriginPrice { get; set; }

        /// <summary>
        /// 活动价 VIP价
        /// </summary>
        public decimal? VipPrice { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public List<string> CoverImageUrls { get; set; }

        /// <summary>
        /// 开售时间
        /// </summary>
        public virtual DateTime? DateTimeStart { get; set; }

        /// <summary>
        /// 结束售卖时间
        /// </summary>
        public virtual DateTime? DateTimeEnd { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public virtual int? LimitBuyCount { get; set; }

        /// <summary>
        /// 已经售总数
        /// </summary>
        public virtual int? SoldCount { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public virtual int StockCount { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>

        public virtual string Code { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        public virtual string Desc { get; set; }

        /// <summary>
        /// 购买须知
        /// </summary>
        public virtual string PurchaseNotes { get; set; }
    }

    public class ProductSpuDto : EntityDto<Guid>
    {
        public Guid? shopId { get; set; }
        public MallShopDto Shop { get; set; }
        public ProductCategoryDto ProductCategory { get; set; }
        public List<ProductSkuDto> Skus { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string DescCommon { get; set; }
        public virtual string PurchaseNotesCommon { get; set; }
        public virtual DateTime? DateTimeStart { get; set; }
        public virtual DateTime? DateTimeEnd { get; set; }
        public virtual int? LimitBuyCount { get; set; }
        public virtual int? SoldCount { get; set; }
    }

    public class ProductSkuDto : EntityDto<Guid>
    {
        public decimal Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal? OriginPrice { get; set; }

        /// <summary>
        /// 活动价 VIP价
        /// </summary>
        public decimal? VipPrice { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public List<string> CoverImageUrls { get; set; }

        /// <summary>
        /// 开售时间
        /// </summary>
        public virtual DateTime? DateTimeStart { get; set; }

        /// <summary>
        /// 结束售卖时间
        /// </summary>
        public virtual DateTime? DateTimeEnd { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public virtual int? LimitBuyCount { get; set; }

        /// <summary>
        /// 已经售总数
        /// </summary>
        public virtual int? SoldCount { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public virtual int StockCount { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>

        public virtual string Code { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        public virtual string Desc { get; set; }

        /// <summary>
        /// 购买须知
        /// </summary>
        public virtual string PurchaseNotes { get; set; }
    }
}