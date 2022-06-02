using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecapiDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RecapiDatabase")));

            services.AddScoped<IRecapiDbContext>(provider => provider.GetService<RecapiDbContext>());

            return services;
        }
    }
}
