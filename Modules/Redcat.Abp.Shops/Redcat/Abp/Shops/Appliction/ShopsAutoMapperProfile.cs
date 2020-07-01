using AutoMapper;
using Redcat.Abp.Shops.Appliction.Dto;
using Redcat.Abp.Shops.Domin;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Entities.Auditing;

namespace Redcat.Abp.Shops.Appliction
{
    public class ShopsAutoMapperProfile:Profile
    {
        public ShopsAutoMapperProfile()
        {
            CreateMap<Shop, ShopDto>();
            CreateMap<ShopCreateOrUpdateDto, Shop>().IgnoreFullAuditedObjectProperties();
            CreateMap<Shop, ShopCreateOrUpdateDto>();
        }
    }
}