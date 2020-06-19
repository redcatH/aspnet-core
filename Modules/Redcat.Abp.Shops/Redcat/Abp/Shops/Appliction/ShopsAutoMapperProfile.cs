using AutoMapper;
using Redcat.Abp.Shops.Appliction.Dto;
using Redcat.Abp.Shops.Domin;

namespace Redcat.Abp.Shops.Appliction
{
    public class ShopsAutoMapperProfile:Profile
    {
        public ShopsAutoMapperProfile()
        {
            CreateMap<Shop, ShopDto>();
            CreateMap<ShopCreateOrUpdateDto, Shop>();
            CreateMap<Shop, ShopCreateOrUpdateDto>();
        }
    }
}