using Seranet.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Seranet.Api.Plugins.Employee
{
    public class EmployeesController: ApiController
    {
        public IEnumerable<Employee> Get()
        {
            string connectionString = System.Environment.GetEnvironmentVariable("SERANET.API.PLUGINS.EMPLOYEE.HRISDB");
            return new HrisDbReader().Read(connectionString);
        }
    }
}
