using System.Collections.Generic;

namespace WebStore.Domain.DTO
{
    public record PageProductsDTO(IEnumerable<ProductDTO> Products, int TotalCount);
    //{
    //    public IEnumerable<ProductDTO> Products{ get; init; }
    //    public int TotalCount { get; init; }
    //}
}