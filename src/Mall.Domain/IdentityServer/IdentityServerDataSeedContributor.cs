using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace Mall.IdentityServer
{
    public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IConfiguration _configuration;
        //private readonly IClientCorsOrigin 

        public IdentityServerDataSeedContributor(
            IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IIdentityResourceDataSeeder identityResourceDataSeeder,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder,
            IConfiguration configuration)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
            _configuration = configuration;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await _identityResourceDataSeeder.CreateStandardResourcesAsync();
            await CreateApiResourcesAsync();
            await CreateClientsAsync();
        }

        private async Task CreateApiResourcesAsync()
        {
            var commonApiUserClaims = new[]
            {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role",
                "picture"
            };

            await CreateApiResourceAsync("Mall", commonApiUserClaims);
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                    new ApiResource(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

        private async Task CreateClientsAsync()
        {
            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address",
                "Mall",
                "picture"
            };

            var configurationSection = _configuration.GetSection("IdentityServer:Clients");

            //Web Client
            var webClientId = configurationSection["Mall_Web:ClientId"];
            if (!webClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["Mall_Web:RootUrl"].EnsureEndsWith('/');

                /* Mall_Web client is only needed if you created a tiered
                 * solution. Otherwise, you can delete this client. */

                await CreateClientAsync(
                    webClientId,
                    commonScopes,
                    new[] { "hybrid", "client_credentials", "password" },
                    (configurationSection["Mall_Web:ClientSecret"] ?? "1q2w3e*").Sha256(),
                    redirectUri: $"{webClientRootUrl}signin-oidc",
                    postLogoutRedirectUri: $"{webClientRootUrl}signout-callback-oidc", Origins: new List<string> {
                        "http://localhost:4200",
                        "https://localhost:4200",
                        "http://localhost:4201",
                        "https://localhost:4201",
                    "https://localhost:44351",
                    "http://localhost:44351"}
                );
            }


            var AngularClientId = configurationSection["Angular_Web:ClientId"];
            if (!AngularClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["Angular_Web:RootUrl"].EnsureEndsWith('/');

                /* Mall_Web client is only needed if you created a tiered
                 * solution. Otherwise, you can delete this client. */

                await CreateClientAsync(
                    AngularClientId,
                    commonScopes,
                    new[] { "hybrid", "client_credentials", "password" },
                    (configurationSection["Angular_Web:ClientSecret"] ?? "1q2w3e*").Sha256(),
                    redirectUri: $"{webClientRootUrl}signin-oidc",
                    postLogoutRedirectUri: $"{webClientRootUrl}signout-callback-oidc", Origins: new List<string> {
                        "http://localhost:4200",
                        "https://localhost:4200",
                        "http://localhost:4201",
                        "https://localhost:4201",
                    "https://localhost:44351",
                    "http://localhost:44351"}
                );
            }


            //Console Test Client
            var consoleClientId = configurationSection["Mall_App:ClientId"];
            if (!consoleClientId.IsNullOrWhiteSpace())
            {
                await CreateClientAsync(
                    consoleClientId,
                    commonScopes,
                    new[] { "password", "client_credentials" },
                    (configurationSection["Mall_App:ClientSecret"] ?? "1q2w3e*").Sha256()
                );
            }
        }

        private async Task<Client> CreateClientAsync(
            string name,
            IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret,
            string redirectUri = null,
            string postLogoutRedirectUri = null,
            IEnumerable<string> permissions = null,IEnumerable<string> Origins=null)
        {
            var client = await _clientRepository.FindByCliendIdAsync(name);
            
            if (client == null)
            {

                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false,
                        AllowAccessTokensViaBrowser=true, //增加允许浏览器token
                        
                    },
                    autoSave: true
                );
                
            }

            //增加Origins
            if (Origins != null)
            {
                foreach (var item in Origins)
                {

                    if (client.FindCorsOrigin(item) == null)
                    {
                        client.AddCorsOrigin(item);
                    }
                }
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (client.FindSecret(secret) == null)
            {
                client.AddSecret(secret);
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions
                );
            }
            if (Origins != null)
            {
                
            }
            return await _clientRepository.UpdateAsync(client);
        }
    }
}
