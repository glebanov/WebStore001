using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartServices _CartServices;

        public CartViewComponent(ICartServices CartServices) => _CartServices = CartServices;

        public IViewComponentResult Invoke() => View(_CartServices.GetViewModel());
    }
}