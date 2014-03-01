using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seranet.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //This is second commit
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
