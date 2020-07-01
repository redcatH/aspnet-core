using System.Collections.Generic;
using System.Linq;

namespace Redcat.Abp.AppManagement.Apps
{
    class AppDefinitionContext : IAppDefinitionContext
    {
        public AppDefinitionContext(Dictionary<string, AppDefinition> apps)
        {
            Apps = apps;
        }

        private Dictionary<string, AppDefinition> Apps { get;} 
        public AppDefinition GetOrNull(string name)
        {
            return Apps.GetOrDefault(name);
        }

        public void Add(params AppDefinition[] definitions)
        {
            if (Apps==null)
            {
                return;
            }

            foreach (var definition
                in definitions)
            {
                if (!Apps.Keys.Contains(definition.name))
                {
                    Apps[definition.name] = definition;
                }
            }
        }
    }
}