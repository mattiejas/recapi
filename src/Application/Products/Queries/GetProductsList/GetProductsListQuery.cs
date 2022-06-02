using MediatR;

namespace Recapi.Application.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<ProductsListVm>
    {
    }
}
