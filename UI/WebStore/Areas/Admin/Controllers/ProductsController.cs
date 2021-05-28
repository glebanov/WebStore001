using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index() => View(_ProductData.GetProducts().Products.FromDTO());
        public IActionResult Edit(int id) =>
            _ProductData.GetProductById(id) is { } product
               ? View(product.FromDTO())
                : NotFound();

        public IActionResult Delete(int id) =>
               _ProductData.GetProductById(id) is { } product
                ? View(product.FromDTO())
                   : NotFound();
    }
}
