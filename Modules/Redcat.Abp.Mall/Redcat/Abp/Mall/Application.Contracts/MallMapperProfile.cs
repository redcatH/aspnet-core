using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Redcat.Abp.Mall.Application.Contracts;
using Redcat.Abp.Mall.Domain;

namespace Redcat.Abp.Mall.Redcat.Abp.Mall.Application.Contracts
{
    public class MallMapperProfile:Profile
    {
        public MallMapperProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryCreateOrUpdateDto, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryCreateOrUpdateDto>().ForMember(p => p.apps,
                p =>
                {
                    p.MapFrom(s=>s.AppProductCategories);
                });
        }
    }
}
