using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;

namespace Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging
{

    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpObjectMappingModule)
        )]
    public class AuditLoggingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AuditLoggingModule>();
            Configure<AbpAutoMapperOptions>(p =>
            {
                p.AddMaps<AuditLoggingModule>(validate:true);
            });

            Configure<AbpAspNetCoreMvcOptions>(p =>
            {
                p.MinifyGeneratedScript = true;
                p.ConventionalControllers.Create(typeof(AuditLoggingModule).Assembly);
            });
        }
    }
}
