using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Shops.Domin;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Redcat.Abp.Shops.EntityFrameworkCore
{
    public class ShopsDbContext : AbpDbContext<ShopsDbContext>, IShopsDbContext
    {
        public ShopsDbContext(DbContextOptions<ShopsDbContext> options) : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureShops();
        }

    }

    public static class ShopsDbContextModelCreatingExtensions
    {
        public static void ConfigureShops(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.Entity<Shop>(t =>
            {
                t.ToTable(ShopsConsts.DbTablePrefix+ "Shop", ShopsConsts.DbSchema);
                t.ConfigureFullAuditedAggregateRoot();
                t.Property(p => p.Name).IsRequired().HasMaxLength(ShopsConsts.NameMaxLength);
                t.Property(p => p.LogoImage).HasMaxLength(ShopsConsts.UrlMaxLength);
                t.Property(p => p.CoverImage).HasMaxLength(ShopsConsts.UrlMaxLength);
                t.Property(p => p.Description).HasMaxLength(ShopsConsts.DesMaxLength);
                t.Property(p => p.ShortName).IsRequired().HasMaxLength(ShopsConsts.NameMaxLength);
            });
        }
    }
}

namespace Redcat.Abp.Shops.Domin
{
    public class ShopsConsts
    {
        public const string DbTablePrefix = "Shops_";

        public const string DbSchema = null;
        public static int NameMaxLength = 64;
        public static int UrlMaxLength = 254;
        public static int DesMaxLength = 512;
    }
}

