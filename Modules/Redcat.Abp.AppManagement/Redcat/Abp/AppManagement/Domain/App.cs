using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Redcat.Abp.AppManagement.Domain
{
    public class App : FullAuditedAggregateRoot<Guid>
    {
        public App(Guid id,
            [NotNull]string name,
            [NotNull] string providerKey, 
            [NotNull] string providerName, 
            [NotNull] string clientName,
            [CanBeNull]Dictionary<string, string> value=null
            )
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;
            ProviderKey = providerKey;
            ProviderName = providerName;
            ClientName = clientName;
            Value = value;
        }

        protected App()
        {
            
        }
        [CanBeNull]public Dictionary<string,string> Value { get; set; }
        [NotNull] public string Name { get; set; }
        [NotNull] public  string ClientName { get; set; }
        [NotNull] public virtual string ProviderName { get; set; }
        [NotNull] public virtual string ProviderKey { get; set; }

    }
}