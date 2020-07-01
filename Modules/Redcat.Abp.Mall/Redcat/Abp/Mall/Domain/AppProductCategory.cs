using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Mall.Domain
{
    /// <summary>
    /// APP产品分类
    /// </summary>
    public class AppProductCategory:CreationAuditedEntity,IMultiTenant
    {
        protected AppProductCategory()
        {

        }
        public AppProductCategory(string appName, Guid? tenantId, ProductCategory productCategory)
        {
            AppName = appName;
            TenantId = tenantId;
            ProductCategory = productCategory;
        }
        
        public string AppName { get; set; }
        public override object[] GetKeys()
        {
            return new object[]
            {
                AppName,ProductCategoryId
            };
        }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public Guid? TenantId { get; set; }
    }
}