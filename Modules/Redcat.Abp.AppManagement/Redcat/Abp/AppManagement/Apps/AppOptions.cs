using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Collections;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Redcat.Abp.AppManagement.Apps
{
    public class AppOptions
    {
        public ITypeList<IAppDefinitionProvider> DefinitionProviders { get; }
        public ITypeList<IAppValueProvider> ValueProviderManager { get; }
        public AppOptions()
        {
            ValueProviderManager = new TypeList<IAppValueProvider>();
            DefinitionProviders = new TypeList<IAppDefinitionProvider>();
        }
    }

    public interface IAppValueProvider
    {
        string Name { get; }
        Task<Dictionary<string, string>> GetOrNullAsync([NotNull]AppDefinition appDefinition);

    }

    public class TenantAppValueProvider : IAppValueProvider
    {
        public IAppStore AppStore;
        public string ProviderName = "T";
        public ICurrentTenant CurrentTenant;
        public TenantAppValueProvider(IAppStore appStore, ICurrentTenant currentTenant)
        {
            AppStore = appStore;
            CurrentTenant = currentTenant;
        }

        public string Name => ProviderName;
        public Task<Dictionary<string, string>> GetOrNullAsync(AppDefinition appDefinition)
        {
            return AppStore.GetOrNullAsync(appDefinition.name, ProviderName, CurrentTenant.Name?.ToString());
        }
    }

    public class UserAppValueProvider : IAppValueProvider
    {
        public IAppStore AppStore;
        public string ProviderName = "U";
        public ICurrentUser CurrentUser;
        public UserAppValueProvider(IAppStore appStore, ICurrentUser currentUser)
        {
            AppStore = appStore;
            CurrentUser = currentUser;
        }
        
        public string Name => ProviderName;
        public Task<Dictionary<string, string>> GetOrNullAsync(AppDefinition appDefinition)
        {
            return AppStore.GetOrNullAsync(appDefinition.name, ProviderName, CurrentUser.UserName);
        }
    }

    public interface IAppStore
    {
        Task<Dictionary<string, string>> GetOrNullAsync([NotNull] string name, [CanBeNull] string providerName,
            [CanBeNull] string providerKey);
    }

    class AppStore : IAppStore, ITransientDependency
    {
        protected IAppManagementStore AppManagementStore;
        protected IAppRepositiory AppRepositiory;
        public AppStore(IAppRepositiory appRepositiory, IAppManagementStore appManagementStore)
        {
            AppRepositiory = appRepositiory;
            AppManagementStore = appManagementStore;
        }
        public async Task<Dictionary<string, string>> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return await AppManagementStore.GetOrNullAsync(name, providerName, providerKey);
        }
    }

    interface IAppManagementStore
    {
        Task<Dictionary<string, string>> GetOrNullAsync(string name, string providerName, string providerKey);
        //Task<List<AppDefinition>> GetListAsync();
    }


    public class AppManagementStore : IAppManagementStore, ITransientDependency
    {
        protected readonly IAppRepositiory AppRepositiory;
        public AppManagementStore(IAppRepositiory appRepositiory)
        {
            AppRepositiory = appRepositiory;
        }

        public async Task<Dictionary<string, string>> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return (await AppRepositiory.FindAsync(name, providerName, providerKey))?.Value;
        }
    }
}