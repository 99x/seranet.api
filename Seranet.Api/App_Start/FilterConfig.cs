using System.Web;
using System.Web.Mvc;
using Seranet.Api.Core;

namespace Seranet.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
