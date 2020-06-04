using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redcat.Abp.AppManagement.Domain;
using Redcat.Abp.AppManagement.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Redcat.Abp.AppManagement.Apps
{
    public class EfCoreAppRepositiory : EfCoreRepository<IAppManagementDbContext, App, Guid>, IAppRepositiory
    {
        public EfCoreAppRepositiory(IDbContextProvider<IAppManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<App> FindAsync(string name, string providerName, string providerKey)
        {
            return await DbSet
                .WhereIf(name!=null,app=>app.Name== name)
                .WhereIf(!string.IsNullOrEmpty(providerName),app=>app.ProviderName==providerName)
                .WhereIf(!string.IsNullOrEmpty(providerKey),app=>app.ProviderKey==providerKey)
                .FirstOrDefaultAsync();
        }

        public Task<List<App>> GetListAsync(string providerName, string providerKey)
        {

            return DbSet
                .WhereIf(!string.IsNullOrEmpty(providerName), w => w.ProviderName == providerName)
                .WhereIf(!string.IsNullOrEmpty(providerKey), w => w.ProviderKey == providerKey)
                .ToListAsync();
        }
    }
}