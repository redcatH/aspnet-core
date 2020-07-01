using System;
using System.Data;
using Redcat.Abp.Shops.Domin;
using Volo.Abp.Domain.Entities.Auditing;

namespace Redcat.Abp.Mall.Domain
{
    public class MallShop:FullAuditedAggregateRoot<Guid>,IShop, IShopUpdate
    {
        protected MallShop()
        {
        }

        public MallShop(IShopData shopData):base(shopData.Id)
        {
            TenantId = shopData.TenantId;
            ShopUpdate(shopData);
        }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string LogoImage { get; private set; }
        public string CoverImage { get; private set; }
        public string Description { get; private set; }

        public bool ShopUpdate(IShopData shopData)
        {
            if (Equals(shopData))
            {
                return false;
            }
            this.Name = shopData.Name;
            this.ShortName = shopData.ShortName;
            this.LogoImage = shopData.LogoImage;
            this.CoverImage = shopData.CoverImage;
            this.Description = shopData.Description;
            return true;
        }
        private bool Equals(IShopData shop)
        {
            return this.Id == shop.Id &&
                   this.TenantId == shop.TenantId &&
                   this.Name == shop.Name &&
                   this.ShortName == shop.ShortName &&
                   this.LogoImage == shop.LogoImage &&
                   this.CoverImage == shop.CoverImage &&
                   this.Description == shop.Description;
        }

        public Guid? TenantId { get; set; }
    }


}