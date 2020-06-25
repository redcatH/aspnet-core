using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Redcat.Abp.OssManagement
{
    public class OssManagementModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<OssManagementModule>();
            //context.Services.AddAbpDbContext<OssManagement>(builder =>
            //{
            //    builder.AddDefaultRepositories();
            //    builder.AddRepository<App, EfCoreAppRepositiory>();
            //});
            Configure<AbpAutoMapperOptions>(p => { p.AddMaps<OssManagementModule>(validate: false); });

            Configure<AbpAspNetCoreMvcOptions>(p =>
            {
                p.MinifyGeneratedScript = true;
                p.ConventionalControllers.Create(typeof(OssManagementModule).Assembly);
            });
        }
    }
}