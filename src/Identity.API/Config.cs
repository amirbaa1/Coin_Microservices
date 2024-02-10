using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace Identity.API
{
    public class Config
    {
        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "CoinClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes={"CoinAPI"},
            },
            new Client
            {
                ClientId = "Coin_WebAPP_Client",
                ClientName = "Coin WebApp Client",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                RequirePkce = false,
                AllowRememberConsent = false,
                RedirectUris = new List<string>
                {
                    "https://localhost:7080/signout-callback-oidc"
                },
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes=new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    "basketapi",
                    "orderingapi",
                    "role"
                },

            }
        };
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CoinAPI","Coin Api"),
        };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
          };
        public static IEnumerable<IdentityResource> IdentityResources =>
         new IdentityResource[]
         {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
         };

        public static List<TestUser> TestUsers =>
           new List<TestUser>
           {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "amirba",
                    Password = "1234",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "amir"),
                        new Claim(JwtClaimTypes.FamilyName, "ba")
                    }
                }
           };

    }
}
