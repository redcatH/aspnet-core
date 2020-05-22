using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Mall.EntityFrameworkCore
{
    [DependsOn(
        typeof(MallEntityFrameworkCoreModule)
        )]
    public class MallEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MallMigrationsDbContext>();
        }
    }
}
