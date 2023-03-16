using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Services;

public class ProfileService : IProfileService
{
    /// <summary>
    /// The user claims principal factory.
    /// </summary>
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    /// <summary>
    /// The user manager.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// The role manager.
    /// </summary>
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileService"/> class.
    /// </summary>
    /// <param name="userClaimsPrincipalFactory">
    /// The user claims principal factory.
    /// </param>
    /// <param name="userManager">
    /// The user manager.
    /// </param>
    /// <param name="roleManager">
    /// The role manager.
    /// </param>
    public ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// The get profile data async.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

        var claims = userClaims.Claims.ToList();
        claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        
        if (_userManager.SupportsUserRole)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                if (!_roleManager.SupportsRoleClaims) continue;
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role != null)
                {
                    claims.AddRange(await _roleManager.GetClaimsAsync(role));
                }
            }
        }

        context.IssuedClaims = claims;
    }

    /// <summary>
    /// The is active async.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;
    }
}