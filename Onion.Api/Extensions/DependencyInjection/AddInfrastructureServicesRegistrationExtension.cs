using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Interfaces.Common;
using Onion.Infrastructure.Identity.Entities;
using Onion.Infrastructure.Logging;
using Onion.Infrastructure.Mapping.Configs;
using Onion.Infrastructure.Persistence.DbContext;
using Onion.Infrastructure.Validators;

namespace Onion.Api.Extensions.DependencyInjection;

public static class AddInfrastructureServicesRegistrationExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Register fluentValidators
        services.AddValidatorsFromAssemblyContaining<SampleValidator>();
        #endregion
        
        #region Register Mapper:Mapster
        MappingConfig.Configure();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<MapsterMapper.IMapper, MapsterMapper.ServiceMapper>();
        services.AddScoped<IMapper, MappingServiceAdapter>();
        #endregion
        
        #region Register Logger 
        services.AddScoped(typeof(Application.Interfaces.Common.ILogger<>), typeof(LoggerAdapter<>));
        #endregion
        
        #region Register Identity

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        #endregion
        
        #region Register Ef Core

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        #endregion
        
        return services;
    }
}