using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Redcat.Abp.AppManagement.Application
{
    public class CreateOrUpdateAppDto:Entity<Guid>
    {
        public string Name { get; set; }
        public string ClientName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
    }
}