using Microsoft.EntityFrameworkCore;
using Redcat.Abp.AppManagement.EntityFrameworkCore;
using Redcat.Abp.Mall.EntityFrameworkCore;
using Redcat.Abp.Shops.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Mall.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See MallDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class MallMigrationsDbContext : AbpDbContext<MallMigrationsDbContext>
    {
        public MallMigrationsDbContext(DbContextOptions<MallMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();//审计部分缺少application
            builder.ConfigureIdentity(); 
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            //APP管理功能 setting start
            builder.ConfigureAppManagement();
            //APP end
            builder.Entity<IdentityUser>().ConfigureCustomUserProperties(); //增加属性
            /* Configure your own tables/entities inside the ConfigureMall method */

            //商城功能 start
            builder.ConfigureMall();
            //mall end

            //店铺功能 start
            builder.ConfigureShops();
            //shops end

            builder.ConfigureMallHost();

        }
    }
}