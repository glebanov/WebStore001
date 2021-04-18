using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebAPI.Employees) { }
    }
}