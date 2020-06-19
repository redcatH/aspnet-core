using System;
using Microsoft.Extensions.DependencyInjection;
using Redcat.Abp.Shops.EntityFrameworkCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Redcat.Abp.Shops
{
    public class ShopModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ShopsDbContext>(optionsBuilder =>
            {
                optionsBuilder.AddDefaultRepositories();
            });
            context.Services.AddAutoMapperObjectMapper<ShopModule>();
            Configure<AbpAutoMapperOptions>(p =>
            {
                p.AddMaps<ShopModule>(validate: false);
            });

            Configure<AbpAspNetCoreMvcOptions>(p =>
            {
                p.MinifyGeneratedScript = true;
                p.ConventionalControllers.Create(typeof(ShopModule).Assembly);
            });
        }
    }
}
