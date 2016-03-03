using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Infrastructure.Logging;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController() { }

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize()]
        public ActionResult LiveLog()
        {
            return View();
        }

        public JsonResult AddLogEntry()
        {
            _logger.Info("Hello from AddLogEntry()!");
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}