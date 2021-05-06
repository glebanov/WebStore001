using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Domain.DTO;
using WebStore.Services.Mapping;

namespace WebStore.Services.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<SqlOrderService> _Logger;

        public SqlOrderService(
      WebStoreDB db,
      UserManager<User> UserManager,
      ILogger<SqlOrderService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => (await _db.Orders
          .Include(order => order.User)
           .Include(order => order.Items)
           .Where(order => order.User.UserName == UserName)
         .ToArrayAsync())
           .Select(order => order.ToDTO());

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
         .Include(order => order.User)
           .Include(order => order.Items)
                .FirstOrDefaultAsync(order => order.Id == id))
           .ToDTO();


        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден в БД");
            _Logger.LogInformation("Оформление нового заказа для {0}",
          UserName);

            var timer = Stopwatch.StartNew();

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user,
            };

            //var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            //var cart_products = await _db.Products
            //  .Where(p => product_ids.Contains(p.Id))
            //  .ToArrayAsync();

            //order.Items = Cart.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.Product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price,  // место, где могут быть применены скидки
            //        Quantity = cart_item.Quantity,
            //    }).ToArray();

            foreach (var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);
                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            _Logger.LogInformation("Заказа для {0} успешно сформирована за {1} с id:{2} на сумму {3}",
             UserName, timer.Elapsed, order.Id, order.Items.Sum(i => i.TotalItemPrice));

            return order.ToDTO();
        }
    }
}