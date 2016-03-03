using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class InformationController : Controller
    {
        [Authorize]
        public ActionResult Administration()
        {
            return View();
        }

        [Authorize]
        public ActionResult QuickLinks()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            return View();
        }
    }
}