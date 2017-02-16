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
using NLog;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Job;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;
using Microsoft.AspNet.Identity;
using WebApplication.Controllers.ViewModels.Enquiries;
using WebApplication.Infrastructure.Services;
using WebApplication.Reports.Quote;
using DeleteCommand = WebApplication.Controllers.ViewModels.Job.DeleteCommand;
using ILogger = WebApplication.Infrastructure.Logging.ILogger;
using SaveCommand = WebApplication.Controllers.ViewModels.Job.SaveCommand;
using ViewModel = WebApplication.Controllers.ViewModels.Job.ViewModel;

namespace WebApplication.Controllers
{
    public class JobController : AbstractController
    {

        private ApplicationUserManager _userManager;
        private IOrderReferenceNumberLookupService _orderReferenceLookupService;

        public JobController()
        {
        }

        
        public JobController(IOrderReferenceNumberLookupService orderReferenceLookupService)
        {
            _orderReferenceLookupService = orderReferenceLookupService;
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
            var jobs = DBSession.tblJobs.Where(x=>x.Status != 16).ProjectTo<ListModel>();
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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ActiveJobs()
        {
            return View(new ActiveJobsModel());
        }

        [AjaxAuthorise]
        public ActionResult _ActiveJobs([CustomDataSourceRequest]DataSourceRequest request)
        {
            var jobs = DBSession.tblJobs.Where(x => x.Status == 4 && x.tblClient.Status).ProjectTo<ActiveJobsModel.Job>();
            return Json(jobs.ToDataSourceResult(request));
        }


        //[Authorize]
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    var quote = new tblJob();
        //    quote.CreateQuote();
        //    DBSession.tblJobs.Add(quote);
        //    DBSession.SaveChanges();
            
        //    var vm = DBSession.tblJobs.Where(x => x.JobID == quote.JobID).ProjectTo<ViewModel>().SingleOrDefault();
        //    return View(vm);
        //}

        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var vm = DBSession.tblJobs.Where(x=>x.JobID == id).ProjectTo<ViewModel>();
            return View(vm.SingleOrDefault());
        }


        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult Save(SaveCommand command)
        {
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.Id);
            if (job == null)
                return HttpNotFound();

            job.ClientID = command.ClientId;
            job.ContactID = command.ContactId;
            job.Description = command.Description;
            job.Comments = command.Comments;
            job.CustomerRef = command.CustomerRef;
            job.ExpectedDeliveryDate = command.ExpectedDeliveryDate;

            DBSession.SaveChanges();

            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _Copy(CopyCommand command)
        {

            var job = DBSession.tblJobs
                .Include(x => x.tblLines).SingleOrDefault(x => x.JobID == command.JobId);

            if (job == null)
                return HttpNotFound();

            string userId = User.Identity.GetUserId();

            var copiedJob = job.Copy(userId, _orderReferenceLookupService);
            
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
                .Include(c => c.tblLines.Select(b => b.tblTimesheets)).SingleOrDefault(x => x.JobID == command.JobId);
            DBSession.tblJobs.Remove(job);
            DBSession.SaveChanges();

            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _EmailQuote(EmailQuoteCommand command)
        {
            //var _logger = LogManager.GetCurrentClassLogger();

            //Attachment attachment = null;
            //MailMessage mm = null;
            //SmtpClient smtp = null;

            //string toAddress = "";
            //string copyToAddress = "";
            //var debug = bool.Parse(ConfigurationManager.AppSettings["DebugEmail"]);
            
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.Id);

            if (job == null)
                return HttpNotFound();

            string userId = User.Identity.GetUserId();
            var userGuid = new Guid(userId);
            var user = UserManager.FindById(userId);

            var operative = DBSession.tblUsers.SingleOrDefault(x => x.UserID == userGuid);
            job.EmailQuote(operative, user);
            DBSession.SaveChanges();
            //string fromAddress = /*"info@kenworthengineering.co.uk";*/user.Email;

            //if (job == null)
            //    return HttpNotFound();

            //toAddress = job.tblContact.Email;
            //if (debug)
            //{
            //    toAddress = "james_davy@outlook.com";
            //    copyToAddress = "jamesgdavy@gmail.com";
            //}
            //else
            //{
            //    if (job.tblClient.CopyToAccounts)
            //    {
            //        copyToAddress = job.tblClient.AccountsEmail;
            //    }
            //}

            //try
            //{
            //    mm = new MailMessage(fromAddress, toAddress);

            //    _logger.Debug("From:" + toAddress);
            //    _logger.Debug("To:" + fromAddress);
            //    _logger.Debug("Copy To:" + copyToAddress);

            //    //Dim path As String = HttpContext.Current.Server.MapPath("~/MergeDocuments/")
            //    var quote = new Quote(command.Id, userGuid);
            //    var quoteFilePath = Server.MapPath("~/Documents/Quote-" + command.Id + ".pdf");
            //    quote.ExportToPdf(quoteFilePath);
            //    attachment = new Attachment(quoteFilePath);
            //    mm.Attachments.Add(attachment);
            //    mm.CC.Add("Quotations@kenworthengineering.co.uk");
            //    if (job.tblClient.CopyToAccounts && job.tblClient.AccountsEmail != "")
            //    {
            //        mm.CC.Add(copyToAddress);
            //    }

            //    mm.Bcc.Add(fromAddress);

            //    mm.Subject = "Please find attached the Quotation Ref:" + job.JobID;

            //    string body = "<p>Dear " + job.tblContact.Forename + "</p>";
            //    body = body +
            //           "<p>Thank you for giving us the opportunity to submit our costs for your current project. I have pleasure in attaching our quotation for your consideration.</p>";
            //    body = body +
            //           "<p>Should you wish to proceed, I look forward to receiving your official purchase order shortly.</p>";
            //    body = body +
            //           "<p>However, if you have any further questions or concerns please do not hesitate to contact me. I look forward to the opportunity to discuss the project with you further.</p>";
            //    body = body + "<p>Regards</p>";
            //    body = body + "<p>" + operative.Forename + " " + operative.Surname + "</p>";
            //    mm.Body = body;
            //    mm.IsBodyHtml = true;

            //    smtp = new SmtpClient();
            //    smtp.Send(mm);
            //    _logger.Debug("Send email to:" + toAddress);
            //}
            //catch (Exception ex)
            //{
            //    _logger.Fatal(ex);
            //    throw new ApplicationException("EmailQuote:SMTP Error:" + ex.Message);
            //}
            //finally
            //{
            //    if (mm != null) mm.Dispose();
            //    if (smtp != null) smtp.Dispose();
            //    if (attachment != null) attachment.Dispose();
            //}
    
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

                string userId = User.Identity.GetUserId();

                foreach (var tblJob in jobs)
                {
                    Console.WriteLine(tblJob.JobID);
                    foreach (var line in tblJob.tblLines)
                    {
                        if (line.Status == 2)
                        {
                            tblJob.UpdateLineStatus(line.LineID, 1, userId);    
                        }
                    }
                }
                DBSession.SaveChanges();

                return JsonActionResult(HttpStatusCode.OK, "Success", "");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _UploadFile(UploadFileCommand command)
        {
            //var jobId = DBSession.tblLines.Where(x => x.LineID == command.Id).Select(x => x.JobID).SingleOrDefault();
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.Id);

            if (job == null)
                return HttpNotFound();

            if (command.File != null)
            {
                var filename = Guid.NewGuid();
                var fullFileName = filename + command.File.ext;
                var contentType = command.File.type;
                job.AddFileToJob(filename, command.File.name, fullFileName, contentType);
                using (
                    var fileStream =
                        new System.IO.FileStream(Server.MapPath("~/Documents/" + fullFileName),
                            System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    fileStream.Write(command.FileBinary, 0, command.FileBinary.Length);
                }

                DBSession.SaveChanges();

                var response = new UploadFileResponse()
                {
                    JobId = command.Id,
                    FileID = filename,
                    ContentType = contentType,
                    FileName = fullFileName,
                    Name = command.File.name
                };

                return JsonActionResult(HttpStatusCode.OK, "Success", response);
            }

            return JsonActionResult(HttpStatusCode.BadRequest, "Success", "No file Selected");
        }

        [AjaxAuthorise]
        [HttpDelete, JsonValidate]
        public ActionResult _DeleteFile(DeleteFileCommand command)
        {
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.JobId);

            if (job == null)
                return HttpNotFound();

            job.RemoveFileFromJob(command.FileId);
            DBSession.SaveChanges();
            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpGet]
        public ActionResult _EventStore(long jobId)
        {
            var jobs = DBSession.tblJobs.Where(x => x.JobID == jobId).ProjectTo<EventStoreModel>().SingleOrDefault();
            return PartialView(jobs);
        }
    }
}