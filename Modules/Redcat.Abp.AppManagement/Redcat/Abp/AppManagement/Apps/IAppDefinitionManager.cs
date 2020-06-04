using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redcat.Abp.AppManagement.Apps
{
    public interface IAppDefinitionManager
    {
        AppDefinition Get(string name);
        Task<IReadOnlyList<AppDefinition>> GetList();
    }
}