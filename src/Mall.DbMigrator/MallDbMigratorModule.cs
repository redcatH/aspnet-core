using Mall.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Mall.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MallEntityFrameworkCoreDbMigrationsModule),
        typeof(MallApplicationContractsModule)
        )]
    public class MallDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
