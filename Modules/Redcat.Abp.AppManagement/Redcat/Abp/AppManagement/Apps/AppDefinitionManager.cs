using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUglify.Helpers;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Redcat.Abp.AppManagement.Apps
{
    public class AppDefinitionManager: IAppDefinitionManager,ISingletonDependency
    {
        public Lazy<IDictionary<string, AppDefinition>> AppDefinitions { get; }
        public IServiceProvider ServiceProvider;
        protected AppOptions AppOptions;
        public AppDefinitionManager(IServiceProvider serviceProvider,IOptions<AppOptions>  appOptions)
        {
            ServiceProvider = serviceProvider;
            AppOptions = appOptions?.Value;
            AppDefinitions = new Lazy<IDictionary<string, AppDefinition>>(CreateAppDefinitions());
        }

        public AppDefinition Get(string name)
        {
            Check.NotNull(name,nameof(name));
            return AppDefinitions.Value.GetOrDefault(name);
        }

        public async Task<IReadOnlyList<AppDefinition>> GetList()
        {
            return await Task.FromResult(AppDefinitions.Value.Values.ToImmutableList());
        }

        /// <summary>
        /// 给所有IAppDefintionProvider实现者初始化Dictionary的AppDefinition
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, AppDefinition> CreateAppDefinitions()
        {
            var apps = new Dictionary<string,AppDefinition>();
            using var scope= ServiceProvider.CreateScope();
            //筛选IAppDefintionProvider DefinitionProviders
            var providers = AppOptions.DefinitionProviders
                .Select(type => scope.ServiceProvider.GetService(type) as IAppDefinitionProvider).ToList();

            foreach (var provider in providers)
            {
                provider.Define(new AppDefinitionContext(apps));
            }
            return apps;
        }
    }
}