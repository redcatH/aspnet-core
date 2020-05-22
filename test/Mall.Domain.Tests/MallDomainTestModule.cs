using Mall.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Mall
{
    [DependsOn(
        typeof(MallEntityFrameworkCoreTestModule)
        )]
    public class MallDomainTestModule : AbpModule
    {

    }
}