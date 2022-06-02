using Microsoft.EntityFrameworkCore;

namespace Recapi.Persistence
{
    public class RecapiDbContextFactory : DesignTimeDbContextFactoryBase<RecapiDbContext>
    {
        protected override RecapiDbContext CreateNewInstance(DbContextOptions<RecapiDbContext> options)
        {
            return new RecapiDbContext(options);
        }
    }
}
