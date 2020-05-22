using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts.Authorization
{
    public static class AuditLogPermission
    {
        public const string GroupName = "RedCatAuditLogManagement";
        
        public static class AuditLogs
        {
            public const string Default = GroupName + ".AuditLog";
            //public const string Default = GroupName + ".AuditLog";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuditLogPermission));
        }
    }

}
