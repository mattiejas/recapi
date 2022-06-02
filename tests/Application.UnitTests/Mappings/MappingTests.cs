using AutoMapper;
using Recapi.Application.Categories.Queries.GetCategoriesList;
using Recapi.Application.Customers.Queries.GetCustomerDetail;
using Recapi.Application.Customers.Queries.GetCustomersList;
using Recapi.Application.Employees.Queries.GetEmployeeDetail;
using Recapi.Application.Employees.Queries.GetEmployeesList;
using Recapi.Application.Products.Queries.GetProductDetail;
using Recapi.Application.Products.Queries.GetProductsFile;
using Recapi.Application.Products.Queries.GetProductsList;
using Recapi.Domain.Entities;
using Shouldly;
using Xunit;

namespace Recapi.Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapCategoryToCategoryDto()
        {
            var entity = new Category();

            var result = _mapper.Map<CategoryDto>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<CategoryDto>(result);
        }

        [Fact]
        public void ShouldMapCustomerToCustomerLookupDto()
        {
            var entity = new Customer();

            var result = _mapper.Map<CustomerLookupDto>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<CustomerLookupDto>(result);
        }

        [Fact]
        public void ShouldMapProductToProductDetailVm()
        {
            var entity = new Product();

            var result = _mapper.Map<ProductDetailVm>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<ProductDetailVm>(result);
        }

        [Fact]
        public void ShouldMapProductToProductDto()
        {
            var entity = new Product();

            var result = _mapper.Map<ProductDto>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<ProductDto>(result);
        }

        [Fact]
        public void ShouldMapProductToProductRecordDto()
        {
            var entity = new Product();

            var result = _mapper.Map<ProductRecordDto>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<ProductRecordDto>(result);
        }

        [Fact]
        public void ShouldMapCustomerToCustomerDetailVm()
        {
            var entity = new Customer();

            var result = _mapper.Map<CustomerDetailVm>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<CustomerDetailVm>(result);
        }

        [Fact]
        public void ShouldMapEmployeeToEmployeeLookupDto()
        {
            var entity = new Employee();

            var result = _mapper.Map<EmployeeLookupDto>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<EmployeeLookupDto>(result);
        }

        [Fact]
        public void ShouldMapEmployeeTerritoryToEmployeeTerritoryDto()
        {
            var entity = new EmployeeTerritory();

            var result = _mapper.Map<EmployeeTerritoryDto>(entity);

            ShouldBeNullExtensions.ShouldNotBeNull(result);
            ShouldBeTestExtensions.ShouldBeOfType<EmployeeTerritoryDto>(result);
        }
    }
}
