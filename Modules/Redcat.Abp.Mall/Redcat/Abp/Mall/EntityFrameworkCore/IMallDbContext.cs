using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Mall.Domain;
using Volo.Abp.EntityFrameworkCore;

namespace Redcat.Abp.Mall.EntityFrameworkCore
{
    public interface IMallDbContext : IEfCoreDbContext
    {
        #region 商品SPU,SKU、分类、分配APP

        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<ProductSku> ProductSkus { get; set; }
        DbSet<ProductSpu> ProductSpus { get; set; }
        DbSet<AppProductCategory> AppProductCategories { get; set; }
        #endregion

        DbSet<MallShop> MallShops { get; set; }
    }
}