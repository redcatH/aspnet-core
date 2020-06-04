﻿using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Redcat.Abp.AuditLogging.Application.Contracts.Dtos
{
    public class EntityChangeDto:EntityDto<Guid>
    {
        public  Guid AuditLogId { get;  set; }

        public  Guid? TenantId { get;  set; }

        public  DateTime ChangeTime { get;  set; }

        public  Volo.Abp.Auditing.EntityChangeType ChangeType { get;  set; }

        public  Guid? EntityTenantId { get;  set; }

        public  string EntityId { get;  set; }

        public  string EntityTypeFullName { get;  set; }

        public  ICollection<EntityPropertyChangeDto> PropertyChanges { get;  set; }

        public  Dictionary<string, object> ExtraProperties { get;  set; }
    }
}
