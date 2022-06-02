using System.Collections.Generic;
using Recapi.Application.Products.Queries.GetProductsFile;

namespace Recapi.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildProductsFile(IEnumerable<ProductRecordDto> records);
    }
}
