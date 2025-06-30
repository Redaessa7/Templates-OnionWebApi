using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Onion.Application.Features.Authentication.DTOs;
using Onion.Application.Features.Users.Interfaces;
using Onion.Application.Interfaces;
using Onion.Infrastructure.Identity;

namespace Onion.Api.Extensions.DependencyInjection;

public static class AddApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,  IConfiguration configuration)
    {
        #region UserServices Registration
        services.AddScoped<IUserService, UserService>();
        #endregion

        #region JWT Registration

        var authOption = configuration.GetSection("JWT").Get<AuthOptions>();
        services.AddSingleton(authOption);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authOption.Issuer,
                    ValidAudience = authOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOption.SigningKey))
                };
            });

        #endregion
        
        return services;
    }
}