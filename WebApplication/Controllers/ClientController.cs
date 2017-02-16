using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Client;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Controllers
{
    public class ClientController : AbstractController
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
            var clients = DBSession.tblClients.ProjectTo<ListModel>();
            return Json(clients.ToDataSourceResult(request));
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

            var client = new tblClient(command.ClientCompanyName, command.ClientAddress1, command.ClientAddress2, command.ClientTown, command.ClientCounty, command.ClientPostCode, command.ClientTelephone, command.ClientEmail, command.ClientFax, command.ClientWWW, command.AccountsEmail, command.CopyToAccounts);
            DBSession.tblClients.Add(client);
            DBSession.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var client = DBSession.tblClients.Where(x => x.ClientID == id).ProjectTo<EditModel>().SingleOrDefault();
            return View(client);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = DBSession.tblClients.SingleOrDefault(x => x.ClientID == command.ClientID);

            client.ClientCompanyName = command.ClientCompanyName;
            client.ClientAddress1 = command.ClientAddress1;
            client.ClientAddress2 = command.ClientAddress2;
            client.ClientTown = command.ClientTown;
            client.ClientCounty = command.ClientCounty;
            client.ClientPostCode = command.ClientPostCode;
            client.ClientTelephone = command.ClientTelephone;
            client.ClientEmail = command.ClientEmail;
            client.ClientFax = command.ClientEmail;
            client.ClientWWW = command.ClientWWW;
            client.AccountsEmail = command.AccountsEmail ?? "";
            client.CopyToAccounts = command.CopyToAccounts;
            client.Status = command.Status;
            
            DBSession.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}