using System;
using AutoMapper;
using Recapi.Application.Common.Mappings;
using Recapi.Persistence;
using Xunit;

namespace Recapi.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public RecapiDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = RecapiContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            RecapiContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}