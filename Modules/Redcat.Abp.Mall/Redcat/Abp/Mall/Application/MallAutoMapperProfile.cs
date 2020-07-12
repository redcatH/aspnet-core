using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Redcat.Abp.Mall.Application;
using Redcat.Abp.Mall.Application.Contracts;
using Redcat.Abp.Mall.Application.Product;
using Redcat.Abp.Mall.Domain;
using Volo.Abp.AutoMapper;

namespace Redcat.Abp.Mall.Redcat.Abp.Mall.Application
{
    public class MallAutoMapperProfile: Profile
    {
        public MallAutoMapperProfile()
        {
            CreateMap<MallShop, MallShopDto>();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategoryCreateOrUpdateDto, ProductCategory>().Ignore(p => p.AppProductCategories);
            CreateMap<ProductSpu, ProductSpuDto>();

            CreateMap<ProductSpu, ProductSpuCreateOrUpdateDto>();

            CreateMap<ProductSpuCreateOrUpdateDto, ProductSpu>().ForMember(p => p.ProductSkus, k => k.Ignore());

            CreateMap<ProductSku, ProductSkuCreateOrUpdate>().ReverseMap();
        }
    }
}
