using System;
using Recapi.Persistence;

namespace Recapi.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly RecapiDbContext _context;

        public CommandTestBase()
        {
            _context = RecapiContextFactory.Create();
        }

        public void Dispose()
        {
            RecapiContextFactory.Destroy(_context);
        }
    }
}