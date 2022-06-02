using System;
using Microsoft.EntityFrameworkCore;
using Recapi.Domain.Entities;
using Recapi.Persistence;

namespace Recapi.Application.UnitTests.Common
{
    public class RecapiContextFactory
    {
        public static RecapiDbContext Create()
        {
            var options = new DbContextOptionsBuilder<RecapiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new RecapiDbContext(options);

            context.Database.EnsureCreated();

            context.Customers.AddRange(new[] {
                new Customer { CustomerId = "ADAM", ContactName = "Adam Cogan" },
                new Customer { CustomerId = "JASON", ContactName = "Jason Taylor" },
                new Customer { CustomerId = "BREND", ContactName = "Brendan Richards" },
            });

            context.Orders.Add(new Order
            {
                CustomerId = "BREND"
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(RecapiDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}