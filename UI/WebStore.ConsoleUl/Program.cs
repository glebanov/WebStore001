using System;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Products;

namespace WebStore.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            var products_client = new ProductsClient(configuration);

            Console.WriteLine("К запросу готов!");
            Console.ReadLine();

            foreach (var product in products_client.GetProducts().Products)
                Console.WriteLine("{0} - {1}", product.Name, product.Price);

            Console.WriteLine("Запрос завершён");
            Console.ReadLine();
        }
    }
}