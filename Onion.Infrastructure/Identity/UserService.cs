using Microsoft.AspNetCore.Identity;
using Onion.Application.Features.Users.Interfaces;
using Onion.Application.Interfaces;
using Onion.Infrastructure.Identity.Entities;

namespace Onion.Infrastructure.Identity;

public class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager)
    : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    
    
    
}