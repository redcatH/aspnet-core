﻿using System;
using Volo.Abp.Application.Dtos;

namespace Redcat.Abp.AuditLogging.Application.Contracts.Dtos
{
    public class EntityPropertyChangeDto:EntityDto<Guid>
    {
        public  Guid? TenantId { get;  set; }

        public  Guid EntityChangeId { get;  set; }

        public  string NewValue { get;  set; }

        public  string OriginalValue { get;  set; }

        public  string PropertyName { get;  set; }

        public  string PropertyTypeFullName { get;  set; }
    }
}