using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => product is null
            ? null
            : new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);

        public static ProductDTO ToDTO(this Product Product) => Product is null
            ? null
            : new ProductDTO
            {
                Id = Product.Id,
                Name = Product.Name,
                Order = Product.Order,
                Price = Product.Price,
                ImageUrl = Product.ImageUrl,
                Brand = Product.Brand.ToDTO(),
                Section = Product.Section.ToDTO(),
            };

        public static Product FromDTO(this ProductDTO Product) => Product is null
            ? null
            : new Product
            {
                Id = Product.Id,
                Name = Product.Name,
                Order = Product.Order,
                Price = Product.Price,
                ImageUrl = Product.ImageUrl,
                BrandId = Product.Brand?.Id,
                Brand = Product.Brand.FromDTO(),
                SectionId = Product.Section.Id,
                Section = Product.Section.FromDTO(),
            };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> Products) => Products.Select(ToDTO);

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> Products) => Products.Select(FromDTO);
    }
}