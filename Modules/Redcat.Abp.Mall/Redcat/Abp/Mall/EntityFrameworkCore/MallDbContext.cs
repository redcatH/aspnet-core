using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Mall.Domain;
using Volo.Abp.EntityFrameworkCore;

namespace Redcat.Abp.Mall.EntityFrameworkCore
{
    public class MallDbContext:AbpDbContext<MallDbContext>, IMallDbContext
    {
        public MallDbContext(DbContextOptions<MallDbContext> options) : base(options)
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSku> ProductSkus { get; set; }
        public DbSet<ProductSpu> ProductSpus { get; set; }
        public DbSet<AppProductCategory> AppProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureMall();
        }
    }
}