using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Redcat.Abp.AppManagement.Domain;
using Volo.Abp.Domain.Repositories;

namespace Redcat.Abp.AppManagement.Apps
{
    public interface IAppRepositiory:IBasicRepository<App,Guid>
    {
        Task<App> FindAsync(string name, string providerName, string providerKey);
        Task<List<App>> GetListAsync(string providerName, string providerKey);
    }
}