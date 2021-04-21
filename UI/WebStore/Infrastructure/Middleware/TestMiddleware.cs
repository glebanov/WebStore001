using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next) => _Next = Next;
      
        public async Task InvokeAsync(HttpContext context)
        {
            // Действие до следующего узла в конвейере

            //context.Response.

            var next = _Next(context);
            
            //Действия во время того, как оставшаяся часть конвейера что-то делает с контекстом 
            
            await next; // Точка синхранизации


            // Действие по завершению работы оставшейся части конвейера
        }
    }
}
