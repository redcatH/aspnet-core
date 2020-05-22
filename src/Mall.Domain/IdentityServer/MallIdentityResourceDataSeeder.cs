using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Mall.IdentityServer
{
    public class MallIdentityResourceDataSeeder : IdentityResourceDataSeeder,IIdentityResourceDataSeeder, ITransientDependency
    {


        public MallIdentityResourceDataSeeder(
            IIdentityResourceRepository identityResourceRepository, 
            IGuidGenerator guidGenerator, 
            IIdentityClaimTypeRepository claimTypeRepository
            ):base(identityResourceRepository, guidGenerator, claimTypeRepository)
        {

        }
        public override async Task CreateStandardResourcesAsync()
        {
            var resources = new[]
            {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Profile(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityServer4.Models.IdentityResources.Address(),
                new IdentityServer4.Models.IdentityResources.Phone(),
                new IdentityServer4.Models.IdentityResource("role", "Roles of the user", new[] {"role"}),
                new IdentityServer4.Models.IdentityResource("picture", "HeadImgUrl", new[] {"picture"}),
            };

            foreach (var resource in resources)
            {
                foreach (var claimType in resource.UserClaims)
                {
                    await AddClaimTypeIfNotExistsAsync(claimType);
                }

                await AddIdentityResourceIfNotExistsAsync(resource);
            }
        }

        //下面的AddIdentityResourceIfNotExistsAsync  和AddClaimTypeIfNotExistsAsync 和原函数一样 

        //protected override async Task AddIdentityResourceIfNotExistsAsync(IdentityServer4.Models.IdentityResource resource)
        //{
        //    if (await IdentityResourceRepository.CheckNameExistAsync(resource.Name))
        //    {
        //        return;
        //    }

        //    await IdentityResourceRepository.InsertAsync(
        //        new IdentityResource(
        //            GuidGenerator.Create(),
        //            resource
        //        )
        //    );
        //}

        //protected override async Task AddClaimTypeIfNotExistsAsync(string claimType)
        //{
        //    if (await ClaimTypeRepository.AnyAsync(claimType))
        //    {
        //        return;
        //    }

        //    await ClaimTypeRepository.InsertAsync(
        //        new IdentityClaimType(
        //            GuidGenerator.Create(),
        //            claimType,
        //            isStatic: true
        //        )
        //    );
        //}
    }
}
