using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication.Infrastructure.ActionResults;
using WebApplication.Infrastructure.Services;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Infrastructure
{
    public class AbstractController : Controller
    {
        public ApplicationEntities DBSession { get; private set; }
        public UserManager<ApplicationUser> UserManager { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DBSession = StructureMap.ObjectFactory.GetInstance<ContextFactory>().GetDbSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //try
            //{
            //    DBSession.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    throw ex;
            //}
        }

        public ActionResult JsonActionResult(HttpStatusCode statusCode, string message, object content)
        {
            return new JsonActionResult(statusCode, message, content);
        }

        public ActionResult JsonActionResult(HttpStatusCode statusCode, string message, object content, List<string> errors)
        {
            return new JsonActionResult(statusCode, message, content, errors);
        }
    }
}