using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Redcat.Abp.Mall.Domain;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Redcat.Abp.Mall.EntityFrameworkCore
{
    public static class MallDbContextModelCreatingExtensions
    {
        public static void ConfigureMall(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<AppProductCategory>(t =>
            {
                t.ToTable(MallConsts.DbTablePrefix + "AppProductCategory", MallConsts.DbSchema);
                t.ConfigureCreationAudited();
                t.HasKey(k => new {k.AppName,k.ProductCategoryId});
                t.Property(p => p.AppName).HasMaxLength(MallConsts.AppNameMaxLength);
                t.HasOne(x => x.ProductCategory).WithMany(x => x.AppProductCategories)
                    .HasForeignKey(x => x.ProductCategoryId).OnDelete(deleteBehavior:DeleteBehavior.Cascade);
            });

            builder.Entity<ProductCategory>(t =>
            {
                t.ToTable(MallConsts.DbTablePrefix + "ProductCategory",MallConsts.DbSchema);
                t.ConfigureFullAuditedAggregateRoot();
                t.Property(p => p.Name).IsRequired().HasMaxLength(MallConsts.CategoryMaxLength);
                t.Property(p => p.LogoImage).HasMaxLength(MallConsts.ImageMaxLength);
                t.Property(p => p.RedirectUrl).HasMaxLength(MallConsts.UrlMaxLength);
                t.Property(p => p.ShortName).HasMaxLength(MallConsts.ShortNameMaxLength);
                t.HasMany(p => p.ProductSpus).WithOne(p => p.ProductCategory).HasForeignKey(p => p.CategoryId);
                t.HasMany(p => p.AppProductCategories).WithOne(p => p.ProductCategory).HasForeignKey(p=>p.ProductCategoryId);
            });

            builder.Entity<ProductSpu>(t =>
            {
                t.ToTable(MallConsts.DbTablePrefix + "ProductSpu", MallConsts.DbSchema);
                t.ConfigureFullAuditedAggregateRoot();
                t.Property(p => p.Name).IsRequired().HasMaxLength(MallConsts.SpuNameMaxLength);
                t.Property(p => p.SoldCount).HasDefaultValue(0);
                t.Property(p => p.LimitBuyCount).HasDefaultValue(null);
                t.HasMany(p => p.ProductSkus).WithOne(p => p.ProductSpu).HasForeignKey(k => k.SpuId);
                t.HasOne(p => p.ProductCategory).WithMany(p => p.ProductSpus).HasForeignKey(k => k.CategoryId);

               
            });


            builder.Entity<ProductSku>(t =>
            {
                t.ToTable(MallConsts.DbTablePrefix + "ProductSku",MallConsts.DbSchema);
                t.ConfigureFullAuditedAggregateRoot();
                t.Property(p => p.Name).IsRequired().HasMaxLength(MallConsts.NameMaxLength);
                t.Property(p => p.LimitBuyCount).HasDefaultValue(null);
                t.Property(p => p.StockCount).HasDefaultValue(0);
                t.HasOne(p => p.ProductSpu).WithMany(p => p.ProductSkus).HasForeignKey(k => k.SpuId);
                t.Property(p => p.CoverImageUrls).HasConversion(
                    v => JsonConvert.SerializeObject(v,
                        new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore}),
                    v => JsonConvert.DeserializeObject<List<string>>(v,
                        new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore}));
            });

            builder.Entity<MallShop>(t =>
            {
                t.ToTable(MallConsts.DbTablePrefix + "MallShop", MallConsts.DbSchema);
                t.ConfigureFullAuditedAggregateRoot();
                t.Property(p => p.Name).IsRequired().HasMaxLength(MallConsts.NameMaxLength);
                t.Property(p => p.LogoImage).HasMaxLength(MallConsts.UrlMaxLength);
                t.Property(p => p.CoverImage).HasMaxLength(MallConsts.UrlMaxLength);
                t.Property(p => p.ShortName).IsRequired().HasMaxLength(MallConsts.NameMaxLength);
            });
        }
    }
}