using System;
using System.Collections.Generic;
using Redcat.Abp.AppManagement.Domain;
using Volo.Abp.Application.Dtos;

namespace Redcat.Abp.AppManagement.Application
{
    /// <summary>
    /// <see cref="App"/>
    /// </summary>
    public class AppDto:EntityDto<Guid>
    {
        public Dictionary<string, string> Value { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
    }
}