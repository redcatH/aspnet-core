using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Redcat.Abp.Shops.MultiShop;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Mall.Domain
{
    /// <summary>
    /// 产品规格型号 SKU
    /// </summary>
    public class ProductSku:AggregateRoot<Guid>,IMultiTenant,IMultiShop
    {
        public ProductSku()
        {

        }

        public ProductSku(string name)
        {
            this.Name = name;
        }
        public ProductSku(decimal price, decimal? originPrice, decimal? vipPrice, List<string> coverImageUrls, DateTime? dateTimeStart, DateTime? dateTimeEnd, int? limitBuyCount, int? soldCount, int stockCount, [CanBeNull] string code, string unit, [NotNull] string name, [CanBeNull] string desc, [CanBeNull] string purchaseNotes, Guid spuId, ProductSpu productSpu, Guid? tenantId, Guid? shopId)
        {
            Price = price;
            OriginPrice = originPrice;
            VipPrice = vipPrice;
            CoverImageUrls = coverImageUrls;
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
            LimitBuyCount = limitBuyCount;
            SoldCount = soldCount;
            StockCount = stockCount;
            Code = code;
            Unit = unit;
            Name = name;
            Desc = desc;
            PurchaseNotes = purchaseNotes;
            SpuId = spuId;
            ProductSpu = productSpu;
            TenantId = tenantId;
            ShopId = shopId;
        }

        #region Sku
        public  decimal Price { get; set; }
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
        [CanBeNull] public virtual DateTime? DateTimeStart { get; set; }
        /// <summary>
        /// 结束售卖时间
        /// </summary>
        [CanBeNull] public virtual DateTime? DateTimeEnd { get; set; }
        /// <summary>
        /// 限购数量
        /// </summary>
        [CanBeNull] public virtual int? LimitBuyCount { get; set; }
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
        [CanBeNull]
        public virtual string Code { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [NotNull] public virtual string Name { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        [CanBeNull] public virtual string Desc { get; set; }

        /// <summary>
        /// 购买须知
        /// </summary>
        [CanBeNull] public virtual string PurchaseNotes { get; set; }
        #endregion

        public virtual Guid SpuId { get; set; }
        [ForeignKey("SpuId")]
        public ProductSpu ProductSpu { get; set; }

        public Guid? TenantId { get; set; }
        public Guid? ShopId { get; set; }

        public void SetEntityGuid(IGuidGenerator guidGenerator)
        {
            this.Id = guidGenerator.Create();
        }

        public void SetTenantId(Guid? id)
        {
            this.TenantId = id;
        }
    }
}