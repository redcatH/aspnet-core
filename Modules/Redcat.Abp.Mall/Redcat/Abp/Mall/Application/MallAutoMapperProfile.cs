using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Redcat.Abp.Mall.Application;
using Redcat.Abp.Mall.Application.Contracts;
using Redcat.Abp.Mall.Domain;

namespace Redcat.Abp.Mall.Redcat.Abp.Mall.Application
{
    public class MallAutoMapperProfile: Profile
    {
        public MallAutoMapperProfile()
        {
            CreateMap<MallShop, MallShopDto>();
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();

        }
    }
}
