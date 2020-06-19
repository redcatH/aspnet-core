using Redcat.Abp.Shops.Domin;

namespace Redcat.Abp.Mall.Domain
{
    public static class MallShopExtensions
    {
        public static bool Equals(this MallShop shopData,IShopData shop)
        {
            return shopData.Id == shop.Id &&
                   shopData.TenantId == shop.TenantId &&
                   shopData.Name == shop.Name &&
                   shopData.ShortName == shop.ShortName &&
                   shopData.LogoImage == shop.LogoImage &&
                   shopData.CoverImage == shop.CoverImage &&
                   shopData.Description == shop.Description;
        }
    }
}