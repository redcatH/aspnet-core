using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Redcat.Abp.AuditLogging.Redcat.Abp.AuditLogging.Application.Contracts.Authorization
{
    public class AuditLogPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var auditLogPermissionGroup = context.AddGroup(AuditLogPermission.GroupName, L("Permission:AuditLogging"));
            auditLogPermissionGroup.AddPermission(AuditLogPermission.AuditLogs.Default, L("Permission:AuditLogging"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuditLoggingResource>(name);
        }
    }
}
