
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Redcat.Abp.AppManagement.Apps;
using Redcat.Abp.AppManagement.Domain;
using Redcat.Abp.AppManagement.EntityFrameworkCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;

namespace Redcat.Abp.AppManagement
{

    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpObjectMappingModule)
    )]
    public class AppManagementModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AppManagementModule>();
            context.Services.AddAbpDbContext<AppManagementDbContext>(builder =>
            {
                builder.AddDefaultRepositories();
                builder.AddRepository<App, EfCoreAppRepositiory>();
            });
            Configure<AbpAutoMapperOptions>(p =>
            {
                p.AddMaps<AppManagementModule>(validate: false);
            });

            Configure<AbpAspNetCoreMvcOptions>(p =>
            {
                p.MinifyGeneratedScript = true;
                p.ConventionalControllers.Create(typeof(AppManagementModule).Assembly);
            });

            Configure<AppOptions>(options =>
            {
                options.ValueProviderManager.Add<UserAppValueProvider>();
                options.ValueProviderManager.Add<TenantAppValueProvider>();
            });
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        private static void AutoAddDefinitionProviders(IServiceCollection Services)
        {
            var definitionProviders = new List<Type>();
            Services.OnRegistred(registredContext =>
            {
                if (typeof(IAppDefinitionProvider).IsAssignableFrom(registredContext.ImplementationType))
                {
                    definitionProviders.Add(registredContext.ImplementationType);
                }
            });

            Services.Configure<AppOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}