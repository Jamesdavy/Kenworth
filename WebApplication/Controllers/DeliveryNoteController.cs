using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.DeliveryNote;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;
using WebApplication.Reports.DeliveryNote;

namespace WebApplication.Controllers
{
    public class DeliveryNoteController : AbstractController
    {
        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _List(long id)
        {
            var deliveryNotes = DBSession.tblLines.Where(x => x.JobID == id && (x.Status == 8)).ProjectTo<ListModel.DeliveryNote>().ToList();
            var vm = new ListModel(id)
            {
                DeliveryNotes = deliveryNotes
            };

            return PartialView(vm);
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _Create(CreateDeliveryNoteCommand command)
        {
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.JobId);

            if (job == null)
                return HttpNotFound();

            var deliveryNoteId = DBSession.tblDeliveryNotes.SingleOrDefault();
            var dnId = deliveryNoteId.DeliveryNoteNoteID + 1;
            var newDeliveryNoteId = new tblDeliveryNote {DeliveryNoteNoteID = dnId};
            DBSession.tblDeliveryNotes.Remove(deliveryNoteId);
            DBSession.tblDeliveryNotes.Add(newDeliveryNoteId);

            XtraReport rep = new XtraReport();
            var internalCopy = new DeliveryNote(job.ClientID.GetValueOrDefault(), dnId, command.SelectedDeliveryNotes, "Office Use");
            var customerCopy = new DeliveryNote(job.ClientID.GetValueOrDefault(), dnId, command.SelectedDeliveryNotes, "Customer Copy");
            internalCopy.CreateDocument();
            customerCopy.CreateDocument();

            foreach (Page page in internalCopy.Pages)
            {
                rep.Pages.Add(page);
            }

            foreach (Page page in customerCopy.Pages)
            {
                rep.Pages.Add(page);
            }

            rep.Watermark.Text = "Office Use";
            rep.Watermark.TextDirection = DirectionMode.ForwardDiagonal;
            rep.Watermark.TextTransparency = 50;
            rep.Watermark.ShowBehind = false;

            var fileName = "DeliveryNote-" + dnId + ".pdf";
            var deliveryNoteFilePath = Server.MapPath("~/Documents/" + fileName );
            rep.ExportToPdf(deliveryNoteFilePath);

            job.UpdateDeliveryNoteStatuses(command.SelectedDeliveryNotes, dnId);
            DBSession.SaveChanges();

            var response = new CreateDeliveryNoteResponse()
            {
                DeliveryNoteURL = "/Documents/" + fileName
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }
    }
}