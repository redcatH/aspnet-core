using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Redcat.Abp.Shops.MultiShop;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Mall.Domain
{
    /// <summary>
    /// 标准产品SPU
    /// </summary>
    public class ProductSpu:AggregateRoot<Guid>,IMultiTenant, IMultiShop
    {
        public ProductSpu(Guid id,Guid categoryId)
        {

            base.Id = id;
            CategoryId = categoryId;
        }

        protected ProductSpu()
        {

        }

        public ProductSpu(Guid? shopId, [CanBeNull] string code, [NotNull] string name, [CanBeNull] string descCommon, [CanBeNull] string purchaseNotesCommon, DateTime? dateTimeStart, DateTime? dateTimeEnd, int? limitBuyCount, int? soldCount, [NotNull] ProductCategory productCategory, Guid categoryId, [CanBeNull] ICollection<ProductSku> productSkus, Guid? tenantId)
        {
            ShopId = shopId;
            Code = code;
            Name = name;
            DescCommon = descCommon;
            PurchaseNotesCommon = purchaseNotesCommon;
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
            LimitBuyCount = limitBuyCount;
            SoldCount = soldCount;
            ProductCategory = productCategory;
            CategoryId = categoryId;
            ProductSkus = productSkus;
            TenantId = tenantId;
        }
        public Guid? ShopId { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [CanBeNull]
        public virtual string Code { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [NotNull]public virtual string Name { get; set; }
        /// <summary>
        /// 公用商品详情
        /// </summary>
        [CanBeNull]public virtual string DescCommon { get; set; }

        /// <summary>
        /// 公用购买须知
        /// </summary>
        [CanBeNull]public virtual  string PurchaseNotesCommon { get; set; }

        /// <summary>
        /// 开售时间
        /// </summary>
        [CanBeNull]public virtual DateTime? DateTimeStart { get; set; }
        /// <summary>
        /// 结束售卖时间
        /// </summary>
        [CanBeNull]public virtual DateTime? DateTimeEnd { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        [CanBeNull] public virtual int? LimitBuyCount { get; set; }
        /// <summary>
        /// 已经售总数
        /// </summary>
        public virtual int? SoldCount { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        [ForeignKey("CategoryId")]
        [NotNull]public ProductCategory ProductCategory { get; set; }
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 商品SKUS
        /// </summary>
        [CanBeNull]
        public ICollection<ProductSku> ProductSkus { get; set; }

        public Guid? TenantId { get; }
    }
}