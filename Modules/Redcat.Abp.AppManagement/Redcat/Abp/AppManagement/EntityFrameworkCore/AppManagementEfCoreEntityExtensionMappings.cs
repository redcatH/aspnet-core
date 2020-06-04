using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Redcat.Abp.AppManagement.Domain;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Redcat.Abp.AppManagement.EntityFrameworkCore
{
    public static class AppManagementEfCoreEntityExtensionMappings
    {
        public static void ConfigureAppManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.Entity<App>(b =>
                {
                    b.ToTable(AppManagementConsts.DbTablePrefix + "Apps", AppManagementConsts.DbSchema);
                    b.ConfigureFullAuditedAggregateRoot();
                    b.Property(p => p.Name).IsRequired().HasMaxLength(AppManagementConsts.MaxNameLength);
                    b.Property(p => p.ClientName).IsRequired().HasMaxLength(AppManagementConsts.MaxNameLength);
                    b.Property(p => p.ProviderName).HasMaxLength(AppManagementConsts.MaxProviderNameLength);
                    b.Property(p => p.ProviderKey).HasMaxLength(AppManagementConsts.MaxProviderKeyLength);
                    b.Property(p => p.Value).HasConversion(
                        v=> JsonConvert.SerializeObject(v,new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore }),
                        v=> JsonConvert.DeserializeObject<Dictionary<string,string>>(v,new JsonSerializerSettings(){ NullValueHandling = NullValueHandling.Ignore}));
                }
            );
        }
    }
}
