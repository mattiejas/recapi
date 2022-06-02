using MediatR;

namespace Recapi.Application.Products.Queries.GetProductDetail
{
    public class GetProductDetailQuery : IRequest<ProductDetailVm>
    {
        public int Id { get; set; }
    }
}
