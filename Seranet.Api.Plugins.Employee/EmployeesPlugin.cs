using Seranet.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seranet.Api.Plugins.Employee
{
    public class EmployeesPlugin : ISeranetApiPlugin
    {
        public IList<Type> GetControllers()
        {
            return new List<Type>() { typeof(EmployeesController) };
        }
    }
}
