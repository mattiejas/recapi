using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Recapi.Application.Common.Interfaces;
using Recapi.Common;
using Recapi.Infrastructure.Files;
using Recapi.Infrastructure.Identity;

namespace Recapi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddScoped<IUserManager, UserManagerService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RecapiDatabase")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            if (environment.IsEnvironment("Test"))
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "Recapi.IntegrationTests",
                            AllowedGrantTypes = {GrantType.ResourceOwnerPassword},
                            ClientSecrets = {new Secret("secret".Sha256())},
                            AllowedScopes = {"Recapi.WebUIAPI", "openid", "profile"}
                        });
                    }).AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                            Username = "jason@Recapi",
                            Password = "Recapi1!",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "jason@Recapi")
                            }
                        }
                    });
            }
            else
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            }

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.Configure<JwtBearerOptions>(
                IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
                options =>
                {
                    var key = configuration["Jwt:Secret"];
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            return services;
        }
    }
}