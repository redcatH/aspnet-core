using Volo.Abp.Modularity;

namespace Mall
{
    [DependsOn(
        typeof(MallApplicationModule),
        typeof(MallDomainTestModule)
        )]
    public class MallApplicationTestModule : AbpModule
    {

    }
}