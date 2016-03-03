using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraExport;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Contact;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Controllers
{
    public class ContactController : AbstractController
    {

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult Index()
        {
            return View(new IndexModel());
        }

        [AjaxAuthorise]
        public ActionResult _Index([CustomDataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var clients = DBSession.tblContacts.ProjectTo<IndexModel>();
                return Json(clients.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _Create(long? id)
        {
            var vm = new CreateModel()
            {
                ClientId = id
            };

            return PartialView(vm);
        }

        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult _Create(CreateCommand command)
        {
            var client = DBSession.tblClients.Where(x => x.ClientID == command.ClientId).SingleOrDefault();
            var contact = new tblContact(command.Forename, command.Surname, command.Position, command.Email, command.Phone);
            client.tblContacts.Add(contact);

            DBSession.SaveChanges();

            var response = new CreateResponse()
            {
                ClientId = command.ClientId,
                ContactId = contact.ContactID,
                Forename = command.Forename,
                Surname = command.Surname,
                Position = command.Position,
                Phone = command.Phone,
                Email = command.Email,
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [Authorize]
        public ActionResult Edit(long id)
        {
            return View(new ViewModel() { ContactId = id });
        }

        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _Edit(long? id)
        {
            var vm = DBSession.tblContacts.Where(x => x.ContactID == id).ProjectTo<EditModel>().SingleOrDefault();
            return PartialView(vm);
        }

        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult _Edit(EditCommand command)
        {
            var contact = DBSession.tblContacts.Where(x => x.ContactID == command.ContactId).SingleOrDefault();
            if (contact == null)
                return HttpNotFound();

            contact.Forename = command.Forename;
            contact.Surname = command.Surname;
            contact.Phone = command.Phone;
            contact.Position = command.Position;
            contact.Email = command.Email;
            contact.Status = command.Status;
            
            DBSession.SaveChanges();

            var response = new EditResponse()
            {
                ContactId = contact.ContactID,
                Forename = command.Forename,
                Surname = command.Surname,
                Position = command.Position,
                Phone = command.Phone,
                Email = command.Email,
                Status = command.Status
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _List(long id)
        {
            var contacts = DBSession.tblContacts.Where(x => x.Status == true && x.ClientID == id).ProjectTo<ListModel.Contact>().ToList();
            var vm = new ListModel()
            {
                Contacts = contacts
            };

            return PartialView(vm);
        }
    }
}