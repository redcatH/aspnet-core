using System;
using System.Collections.Generic;
using System.Text;
using Redcat.Abp.AppManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Reflection;
using Volo.Abp.Localization;

namespace Redcat.Abp.AppManagement.Application.Contracts.Authorization
{
    class AppManagementPermissionDefinitionProvider: PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group=context.AddGroup(AppManagentPermission.GroupName, L("Permission:AppManagement"));
            var apps=group.AddPermission(AppManagentPermission.AppManagent.Default, L("Permission:Apps"));
            apps.AddChild(AppManagentPermission.AppManagent.Create, L("Permission:Create"));
            apps.AddChild(AppManagentPermission.AppManagent.Update, L("Permission:Update"));
            apps.AddChild(AppManagentPermission.AppManagent.Delete, L("Permission:Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AppResource>(name);
        }
    }

    class AppManagentPermission
    {
        public const string GroupName = "AppManagement";

        public static class AppManagent
        {
            public const string Default = GroupName + ".Apps";
            public const string Create= GroupName + ".Create";
            public const string Update = GroupName + ".Update";
            public const string Delete = GroupName + ".Delete";
            //public const string Default = GroupName + ".AuditLog";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AppManagentPermission));
        }
    }
}
