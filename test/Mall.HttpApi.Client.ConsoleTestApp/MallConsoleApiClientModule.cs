using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Mall.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(MallHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MallConsoleApiClientModule : AbpModule
    {
        
    }
}
