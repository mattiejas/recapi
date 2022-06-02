using Microsoft.AspNetCore.Hosting;
using Recapi.WebUI.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace Recapi.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}