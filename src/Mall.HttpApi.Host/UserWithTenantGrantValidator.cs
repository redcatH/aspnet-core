using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mall
{
    public class UserWithTenantGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "UserWithTenant";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {

            await Task.CompletedTask;
        }
    }
}
