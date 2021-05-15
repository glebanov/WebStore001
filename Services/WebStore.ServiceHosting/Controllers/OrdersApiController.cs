using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{

    /// <summary>Управление заказами</summary>
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        /// <summary>Получение всех заказов указанного пользователя</summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Перечень заказов, сделанных указанным пользователем</returns>
        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) =>
            await _OrderService.GetUserOrders(UserName);

        /// <summary>Получение заказа по его идентификатору</summary>
        /// <param name="id">Идентификатор запрашиваемого заказа</param>
        /// <returns>Информация о заказе</returns>
        [HttpGet("{id:int}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _OrderService.GetOrderById(id);

        /// <summary>Создание нового заказа</summary>
        /// <param name="UserName">Имя пользователя - заказчика</param>
        /// <param name="OrderModel">Информация о заказе</param>
        /// <returns>Информация о сформированном заказе</returns>
        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel) =>
            await _OrderService.CreateOrder(UserName, OrderModel);
    }
}