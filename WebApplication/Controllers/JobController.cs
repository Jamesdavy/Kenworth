using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity.Owin;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Job;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;
using Microsoft.AspNet.Identity;
using WebApplication.Reports.Quote;

namespace WebApplication.Controllers
{
    public class JobController : AbstractController
    {

        private ApplicationUserManager _userManager;

        public JobController()
        {
        }

        public JobController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View(new ListModel());
        }

        [AjaxAuthorise]
        public ActionResult _Index([CustomDataSourceRequest]DataSourceRequest request)
        {
            var jobs = DBSession.tblJobs.ProjectTo<ListModel>();
            return Json(jobs.ToDataSourceResult(request));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Quotes()
        {
            return View(new QuotesModel());
        }

        [AjaxAuthorise]
        public ActionResult _Quotes([CustomDataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var jobs = DBSession.tblJobs.Where(x => x.Status == 2 && x.tblClient.Status).ProjectTo<QuotesModel>();
                return Json(jobs.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Jobs()
        {
            return View(new JobsModel());
        }

        [AjaxAuthorise]
        public ActionResult _Jobs([CustomDataSourceRequest]DataSourceRequest request)
        {
            var jobs = DBSession.tblJobs.Where(x => x.Status == 4 && x.tblClient.Status).ProjectTo<JobsModel>();
            return Json(jobs.ToDataSourceResult(request));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var quote = new tblJob();
            quote.CreateQuote();
            DBSession.tblJobs.Add(quote);
            DBSession.SaveChanges();
            
            var vm = DBSession.tblJobs.Where(x => x.JobID == quote.JobID).ProjectTo<ViewModel>().SingleOrDefault();
            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var vm = DBSession.tblJobs.Where(x=>x.JobID == id).ProjectTo<ViewModel>().SingleOrDefault();
            return View("Create", vm);
        }


        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult Save(SaveCommand command)
        {
            var job = DBSession.tblJobs.Where(x => x.JobID == command.Id).SingleOrDefault();
            job.ClientID = command.ClientId;
            job.ContactID = command.ContactId;
            job.Description = command.Description;
            job.Comments = command.Comments;
            job.CustomerRef = command.CustomerRef;

            DBSession.SaveChanges();

            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _Copy(CopyCommand command)
        {

            var job = DBSession.tblJobs
                .Include(x => x.tblLines)
                .Where(x => x.JobID == command.JobId).SingleOrDefault();

            var copiedJob = job.Copy();
            
            DBSession.tblJobs.Add(copiedJob);
            DBSession.SaveChanges();

            var response = new CopyResponse() {NewJobId = copiedJob.JobID};
            return JsonActionResult(HttpStatusCode.OK, "Success", response);

        }

        [AjaxAuthorise]
        [HttpDelete, JsonValidate]
        public ActionResult _Delete(DeleteCommand command)
        {
            var job = DBSession.tblJobs
                .Include(x => x.tblLines)
                .Include(c => c.tblLines.Select(b => b.tblPurchaseOrders))
                .Include(c => c.tblLines.Select(b => b.tblTimesheets))
                .Where(x => x.JobID == command.JobId).SingleOrDefault();
            DBSession.tblJobs.Remove(job);
            DBSession.SaveChanges();

            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _EmailQuote(EmailQuoteCommand command)
        {
            string toAddress = "";
            string copyToAddress = "";
            var debug = bool.Parse(ConfigurationManager.AppSettings["DebugEmail"]);
            
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.Id);
            string userId = User.Identity.GetUserId();
            var userGuid = new Guid(userId);
            var user = UserManager.FindById(userId);
            var operative = DBSession.tblUsers.SingleOrDefault(x => x.UserID == userGuid);

            string fromAddress = user.Email;

            if (job == null)
                return HttpNotFound();

            toAddress = job.tblContact.Email;
            if (debug)
            {
                toAddress = "enquiries@kenworthengineering.co.uk";
                copyToAddress = "enquiries@kenworthengineering.co.uk";
            }
            else
            {
                if (job.tblClient.CopyToAccounts)
                {
                    copyToAddress = job.tblClient.AccountsEmail;
                }
            }

            try
            {
                var mm = new MailMessage(fromAddress, toAddress);
                
                //Dim path As String = HttpContext.Current.Server.MapPath("~/MergeDocuments/")
                var quote = new Quote(command.Id, userGuid);
                var quoteFilePath = Server.MapPath("~/Documents/Quote-" + command.Id + ".pdf");
                quote.ExportToPdf(quoteFilePath);
                var attachment = new Attachment(quoteFilePath);
                mm.Attachments.Add(attachment);

                if (job.tblClient.CopyToAccounts && job.tblClient.AccountsEmail != "")
                {
                    mm.CC.Add(copyToAddress);
                }

                mm.Bcc.Add(user.Email);

                mm.Subject = "Please find attached the Quotation Ref:" + job.JobID;

                string body = "<p>Dear " + job.tblContact.Forename + "</p>";
                body = body +
                       "<p>Thank you for giving us the opportunity to submit our costs for your current project. I have pleasure in attaching our quotation for your consideration.</p>";
                body = body +
                       "<p>Should you wish to proceed, I look forward to receiving your official purchase order shortly.</p>";
                body = body +
                       "<p>However, if you have any further questions or concerns please do not hesitate to contact me. I look forward to the opportunity to discuss the project with you further.</p>";
                body = body + "<p>Regards</p>";
                body = body + "<p>" + operative.Forename + " " + operative.Surname + "</p>";
                mm.Body = body;
                mm.IsBodyHtml = true;

                var smtp = new SmtpClient();
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("EmailQuote:SMTP Error:" + ex.Message);
            }
    
            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _ArchiveQuotes()
        {
            try
            {
                var quoteCutOffDate = DateTime.Now.AddMonths(-2);
                var jobs = DBSession.tblJobs
                    .Include(x=>x.tblLines)
                    .Where(x => x.tblLines.Any(y => y.Status == 2) && x.QuoteDate < quoteCutOffDate).ToList();

                foreach (var tblJob in jobs)
                {
                    foreach (var line in tblJob.tblLines)
                    {
                        if (line.Status == 2)
                        {
                            tblJob.UpdateLineStatus(line.LineID, 1);    
                        }
                    }
                }

                return JsonActionResult(HttpStatusCode.OK, "Success", "");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

    
}