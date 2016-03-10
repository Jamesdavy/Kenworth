using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Line;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Controllers
{
    public class LineController : AbstractController
    {

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize()]
        public ActionResult Index()
        {
            return View(new IndexModel());
        }

        [AjaxAuthorise]
        public ActionResult _Index([CustomDataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var jobs = DBSession.tblLines.Where(x => x.tblJob.tblClient.Status).ProjectTo<QuotesModel>();
                return Json(jobs.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult Quotes()
        {
            return View(new QuotesModel());
        }

        [AjaxAuthorise]
        public ActionResult _Quotes([CustomDataSourceRequest]DataSourceRequest request)
        {
            var jobs = DBSession.tblLines.Where(x => x.Status == 2 && x.tblJob.tblClient.Status).ProjectTo<QuotesModel>();
            return Json(jobs.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult Jobs()
        {
            return View(new JobsModel());
        }

        [AjaxAuthorise]
        public ActionResult _Jobs([CustomDataSourceRequest]DataSourceRequest request)
        {
            var jobs = DBSession.tblLines.Where(x => x.Status == 4 && x.tblJob.tblClient.Status).ProjectTo<JobsModel>();
            return Json(jobs.ToDataSourceResult(request));
        }

        [AjaxAuthorise]
        [HttpGet]
        public ActionResult _Create(long id)
        {
            var viewModel = new CreateViewModel()
            {
                JobId = id
            };

            return PartialView(viewModel);
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _Create(CreateCommand command)
        {
            string fullFileName = "";
            string contentType = "";

            var job = DBSession.tblJobs.Where(x => x.JobID == command.JobId).SingleOrDefault();
            var line = job.AddLine(command.Description, 2, command.Quantity, command.UnitPrice, command.ExpectedDeliveryDate, command.DeliveryComments, command.DrawingNumber, command.CustomerRef, command.EstimatedHours, command.EstimatedHourlyRate);

            if (command.File != null)
            {
                var filename = Guid.NewGuid();
                fullFileName = filename + command.File.ext;
                contentType = command.File.type;
                line.tblFile = new tblFile(filename, filename + command.File.ext, command.File.type);

                using (
                    var _FileStream =
                        new System.IO.FileStream(Server.MapPath("~/Documents/" + fullFileName),
                            System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    _FileStream.Write(command.FileBinary, 0, command.FileBinary.Length);
                };
            }

            DBSession.SaveChanges();

            var status = DBSession.tblStatuses.Where(x => x.Id == line.Status).Select(x => x.Name).SingleOrDefault();
            var response = new CreateResponse()
            {
                JobId = command.JobId,
                LineId = line.LineID,
                Description = line.Description,
                JobLineId = line.JobLineID,
                StatusName = status,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice,
                ExpectedDeliveryDate = command.ExpectedDeliveryDate,
                DeliveryComments = command.DeliveryComments,
                DrawingNumber = command.DrawingNumber,
                FileName = fullFileName,
                ContentType = contentType,
                CustomerRef = command.CustomerRef,
                EstimatedHours = command.EstimatedHours,
                EstimatedHourlyRate = command.EstimatedHourlyRate,
                CalculatedUnitPrice = line.CalculatedUnitPrice
            };
            
            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [Authorize]
        public ActionResult Edit(long id)
        {
            return View(new ViewModel() { LineId = id });
        }

        [AjaxAuthorise]
        [HttpGet]
        public ActionResult _Edit(long id)
        {
            var vm = DBSession.tblLines.Where(x => x.LineID == id).ProjectTo<EditViewModel>().SingleOrDefault();
            return PartialView(vm);
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _Edit(EditCommand command)
        {
            string fullFileName = "";
            string contentType = "";

            var jobId = DBSession.tblLines.Where(x => x.LineID == command.LineId).Select(x => x.JobID).SingleOrDefault();
            var job = DBSession.tblJobs.Where(x => x.JobID == jobId).SingleOrDefault();
            var line = job.UpdateLine(command.LineId, command.Description, command.Quantity, command.UnitPrice, command.ExpectedDeliveryDate, command.DeliveryComments, command.DrawingNumber, command.CustomerRef, command.EstimatedHours, command.EstimatedHourlyRate );

            if (command.File != null)
            {
                var filename = Guid.NewGuid();
                fullFileName = filename + command.File.ext;
                contentType = command.File.type;
                line.tblFile = new tblFile(filename, filename + command.File.ext, command.File.type);    
                using (
                    var _FileStream =
                        new System.IO.FileStream(Server.MapPath("~/Documents/" + fullFileName),
                            System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    _FileStream.Write(command.FileBinary, 0, command.FileBinary.Length);
                };
            }

            DBSession.SaveChanges();

            var response = new EditResponse()
            {
                LineId = command.LineId,
                Description = command.Description,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice,
                ExpectedDeliveryDate = command.ExpectedDeliveryDate,
                DeliveryComments = command.DeliveryComments,
                DrawingNumber = command.DrawingNumber,
                FileName = fullFileName,
                ContentType = contentType,
                CustomerRef = command.CustomerRef,
                EstimatedHours = command.EstimatedHours,
                EstimatedHourlyRate = command.EstimatedHourlyRate,
                CalculatedUnitPrice = line.CalculatedUnitPrice
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [HttpDelete, JsonValidate]
        public ActionResult _Delete(DeleteCommand command)
        {
            var jobId = DBSession.tblLines.Where(x => x.LineID == command.LineId).Select(x => x.JobID).SingleOrDefault();
            var job = DBSession.tblJobs.Where(x => x.JobID == jobId).SingleOrDefault();
            job.DeleteLine(command.LineId);
            DBSession.SaveChanges();

            return JsonActionResult(HttpStatusCode.OK, "Success", "");
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _ChangeStatus(ChangeStatusCommand command)
        {
            var jobId = DBSession.tblLines.Where(x => x.LineID == command.LineId).Select(x => x.JobID).SingleOrDefault();
            var job = DBSession.tblJobs.Where(x => x.JobID == jobId).SingleOrDefault();
            job.UpdateLineStatus(command.LineId, command.Status);
            DBSession.SaveChanges();

            var status = DBSession.tblStatuses.Where(x => x.Id == command.Status).SingleOrDefault();
            var jobStatus = DBSession.tblStatuses.Where(x => x.Id == job.Status).SingleOrDefault();

            var response = new ChangeStatusResponse()
            {
                LineId = command.LineId,
                Status = status.Name,
                JobStatus = jobStatus.Name
            };


            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [HttpPost, JsonValidate]
        public ActionResult _DeleteDrawing(DeleteDrawingCommand command)
        {
            var jobId = DBSession.tblLines.Where(x => x.LineID == command.LineId).Select(x => x.JobID).SingleOrDefault();
            var job = DBSession.tblJobs.Where(x => x.JobID == jobId).SingleOrDefault();
            job.DeleteDrawing(command.LineId);

            var response = new DeleteDrawingResponse()
            {
                LineId = command.LineId
            }; 

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }
    }
}