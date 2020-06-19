using System;
using Microsoft.Extensions.DependencyInjection;
using Redcat.Abp.AppManagement;
using Redcat.Abp.Shops;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;

namespace Redcat.Abp.Mall
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpObjectMappingModule),
        typeof(AppManagementModule),
        typeof(ShopModule)
    )]
    public class MallModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MallModule>();
            Configure<AbpAutoMapperOptions>(p =>
            {
                p.AddMaps<MallModule>(validate: true);
            });

            Configure<AbpAspNetCoreMvcOptions>(p =>
            {
                p.MinifyGeneratedScript = true;
                p.ConventionalControllers.Create(typeof(MallModule).Assembly);
            });
        }
    }
}
