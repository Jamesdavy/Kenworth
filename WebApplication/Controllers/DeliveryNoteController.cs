using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Microsoft.AspNet.Identity;
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
            var deliveryNotes =
                DBSession.tblLines.Where(x => x.JobID == id && ((x.Status & (8 | 4)) != 0))
                    .ProjectTo<ListModel.DeliveryNote>()
                    .ToList();
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
            var selectedDeliveryNotes =
                command.DeliveryNotes.Where(x => x.QuantityToDispatch > 0).Select(y => y.LineId).ToArray();

            DBSession.tblDeliveryNotes.Remove(deliveryNoteId);
            DBSession.tblDeliveryNotes.Add(newDeliveryNoteId);

            var fileName = "DeliveryNote-" + dnId + ".pdf";
            string userId = User.Identity.GetUserId();

            foreach (var deliveryNote in command.DeliveryNotes)
            {
                job.UpdateDeliveryNoteStatus(deliveryNote.LineId, dnId, deliveryNote.QuantityAlreadyDispatched,
                    deliveryNote.QuantityToDispatch, userId, fileName);
            }

            DBSession.SaveChanges();
            //job.UpdateDeliveryNoteStatuses(selectedDeliveryNotes, dnId);


            XtraReport rep = new XtraReport();
            var internalCopy = new DeliveryNote(job.JobID, job.ClientID.GetValueOrDefault(), dnId, selectedDeliveryNotes,
                "Office Use");
            var customerCopy = new DeliveryNote(job.JobID, job.ClientID.GetValueOrDefault(), dnId, selectedDeliveryNotes,
                "Customer Copy");
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


            var deliveryNoteFilePath = Server.MapPath("~/Documents/" + fileName);
            rep.ExportToPdf(deliveryNoteFilePath);

            Attachment attachment = null;
            MailMessage mm = null;
            SmtpClient smtp = null;

            string toAddress = "";
            string fromAddress = "";
            var debug = bool.Parse(ConfigurationManager.AppSettings["DebugEmail"]);

            if (debug)
            {
                toAddress = "james_davy@outlook.com";
                fromAddress = "jamesgdavy@gmail.com";
            }
            else
            {
                toAddress = "delivery@kenworthengineering.co.uk";
                fromAddress = "delivery@kenworthengineering.co.uk";
            }

            mm = new MailMessage(fromAddress, toAddress);
            attachment = new Attachment(deliveryNoteFilePath);
            mm.Attachments.Add(attachment);

            mm.Subject = job.OurOrderReference;

            try
            {
                smtp = new SmtpClient();
                smtp.Send(mm);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (mm != null) mm.Dispose();
                if (smtp != null) smtp.Dispose();
                if (attachment != null) attachment.Dispose();
            }

            var response = new CreateDeliveryNoteResponse()
            {
                DeliveryNoteURL = "/Documents/" + fileName
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }
    }
}