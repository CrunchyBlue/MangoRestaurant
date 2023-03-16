using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity.Constants;

/// <summary>
/// The static details.
/// </summary>
public static class SD
{
    /// <summary>
    /// The admin constant
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// The customer constant.
    /// </summary>
    public const string Customer = "Customer";

    /// <summary>
    /// The identity resources.
    /// </summary>
    public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile(),
    };

    /// <summary>
    /// The api scopes.
    /// </summary>
    public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
    {
        new ApiScope("mango", "Mango Server"),
        new ApiScope(name: "read", displayName: "Read data"),
        new ApiScope(name: "write", displayName: "Write data"),
        new ApiScope(name: "delete", displayName: "delete data"),
    };

    /// <summary>
    /// The clients.
    /// </summary>
    public static IEnumerable<Client> Clients = new List<Client>
    {
        new Client
        {
            ClientId = "client",
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"read", "write", "profile"}
        },
        new Client
        {
            ClientId = "mango",
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = {"https://localhost:44377/signin-oidc"},
            PostLogoutRedirectUris = {"https://localhost:44377/signout-callback-oidc"},
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "mango"
            }
        },
    };
}