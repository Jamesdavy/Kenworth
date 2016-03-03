using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Operative;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Controllers
{
    public class OperativeController : AbstractController
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View(new ListModel());
        }

        [AjaxAuthorise]
        public ActionResult _Index([CustomDataSourceRequest]DataSourceRequest request)
        {
            var operatives = DBSession.tblUsers.ProjectTo<ListModel>();
            return Json(operatives.ToDataSourceResult(request));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new tblUser(command.Forename, command.Surname, command.Telephone, command.Position, command.StatusID);
            DBSession.tblUsers.Add(user);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var operative = DBSession.tblUsers.Where(x => x.UserID == new Guid(id)).ProjectTo<EditModel>().SingleOrDefault();
            return View(operative);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = DBSession.tblUsers.Where(x => x.UserID == command.UserID).SingleOrDefault();
            user.Forename = command.Forename;
            user.Surname = command.Surname;
            user.Telephone = command.Telephone;
            user.Position = command.Position;
            user.StatusID = command.StatusID;
            DBSession.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}