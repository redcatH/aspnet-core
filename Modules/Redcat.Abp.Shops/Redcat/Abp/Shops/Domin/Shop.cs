using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Shops.Domin
{
    public class Shop:FullAuditedAggregateRoot<Guid>,IShop,IMultiTenant
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string LogoImage { get; private set; }
        public string CoverImage { get; internal set; }
        public string Description { get; private set; }

        /// <summary>
        ///             CoverImage 没有在构造函数内,代表是选填内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="logoImage"></param>
        /// <param name="description"></param>
        public Shop(Guid id,string name, string shortName, string logoImage, string description)
        {
            this.Id = id;
            this.Name = name;
            this.ShortName = shortName;
            this.LogoImage = logoImage;
            this.Description = description;
        }

        public Shop()
        {
            ExtraProperties = new Dictionary<string, object>();
        }

        public Shop(ShopData shopData)
        {
            TenantId = shopData.TenantId;
            Id = shopData.Id;
            Name = shopData.Name;
            ShortName = shopData.ShortName;
            LogoImage = shopData.LogoImage;
            CoverImage = shopData.CoverImage;
            Description = shopData.Description;
        }

        internal void SetName(string name)
        {
            this.Name = name;
        }

        internal void SetShortName(string name)
        {
            this.ShortName = name;
        }

        //internal void SetId(Guid id)
        //{
        //    this.Id = id;
        //}

        internal void SetDescription(string description)
        {
            this.Description = description;
        }

        internal void SetLogoImg(string logoImg)
        {
            this.LogoImage = logoImg;
        }

        internal void SetCoverImage(string coverImage)
        {
            this.CoverImage = coverImage;
        }

        public Guid? TenantId { get; set; }
    }

    public class ShopData:IShopData
    {
        public Guid? Tenant { get; set; }
        public Guid Id { get; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoImage { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }

        public ShopData(Guid id, Guid? tenantId, string name, string shortName, string logoImage, string coverImage, string description)
        {
            Id = id;
            TenantId = tenantId;
            Name = name;
            ShortName = shortName;
            LogoImage = logoImage;
            CoverImage = coverImage;
            Description = description;
        }
    }

    public interface IShopData:IMultiTenant
    {
        Guid? Tenant { get; }
        Guid Id { get; }
        string Name { get; }
        string ShortName { get; }
        string LogoImage { get; }
        string CoverImage { get; }
        string Description { get; }
    }

    public interface IShop
    {
        Guid Id { get; }
        Guid? TenantId { get; }
        string Name { get; }
        string ShortName { get; }
        string LogoImage { get; }
        string CoverImage { get; }
        string Description { get; }
    }
}