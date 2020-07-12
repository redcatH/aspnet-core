using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Mall.Domain
{
    /// <summary>
    /// 产品分类，一个分类可以提供给多个APP
    /// </summary>
    public class ProductCategory:AggregateRoot<Guid>,IMultiTenant
    {
        public ProductCategory()
        {
        }

        public ProductCategory(Guid? tenantId)
        {
            this.TenantId = tenantId;
        }
        [NotNull]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoImage { get; set; }
        public string RedirectUrl { get; set; }
        public virtual List<ProductSpu> ProductSpus { get; set; }

        public virtual ICollection<AppProductCategory> AppProductCategories { get; set; }

        public Guid? TenantId { get; set; }
    }
}