using System.Collections.Generic;

namespace Redcat.Abp.AppManagement.Apps
{
    public class AppDefinition
    {
        public AppDefinition(string name,  string clientName, string clientType=null, Dictionary<string, string> defaultValues=null)
        {
            Name = name;
            DefaultValues = defaultValues;
            Providers = new List<string>();
            ClientName = clientName;
            ClientType = clientType;
        }

        public string Name { get; set; }
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public Dictionary<string,string> DefaultValues { get; set; }
        public List<string> Providers { get; set; }
    }
}
