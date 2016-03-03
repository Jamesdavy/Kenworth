using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.Timesheet;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;

namespace WebApplication.Controllers
{
    public class TimeSheetController : AbstractController
    {
        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _Create(long? id)
        {
            var vm = new ViewModel()
            {
                LineID = id
            };

            return PartialView(vm);
        }

        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult _Create(CreateCommand command)
        {
            var ids = DBSession.tblLines.Where(x => x.LineID == command.LineId).Select(x => new { x.JobID, x.JobLineID }).SingleOrDefault();
            var job = DBSession.tblJobs.Where(x => x.JobID == ids.JobID).SingleOrDefault();
            var user = DBSession.tblUsers.Where(x => x.UserID == command.UserId).SingleOrDefault();
            var timesheet = job.AddTimesheet(command.LineId, command.UserId, command.Comments, command.Hours, command.HourlyRate, command.TimesheetDate);
            DBSession.SaveChanges();

            var response = new CreateResponse()
            {
                JobID = ids.JobID,
                JobLineID = ids.JobLineID,
                LineId = command.LineId,
                TimesheetID = timesheet.TimesheetID,
                Comments = timesheet.Comments,
                HourlyRate = timesheet.HourlyRate,
                Hours = timesheet.Hours,
                TimesheetDate = timesheet.TimesheetDate,
                OperativeName = user.Forename + " " + user.Surname
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _Edit(long? id)
        {
            var vm = DBSession.tblTimesheets.Where(x => x.TimesheetID == id).ProjectTo<EditModel>().SingleOrDefault();
            return PartialView(vm);
        }

        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult _Edit(EditCommand command)
        {
            var line = DBSession.tblTimesheets.Where(x => x.TimesheetID == command.TimesheetID).Select(x => new { x.tblLine.JobID, x.LineID }).SingleOrDefault();
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == line.JobID);

            if (job == null)
                return HttpNotFound();

            job.UpdateTimesheet(command.TimesheetID, command.Comments, command.Hours, command.HourlyRate, command.TimesheetDate);
            DBSession.SaveChanges();

            var response = new EditResponse()
            {
                LineId = line.LineID,
                TimesheetId = command.TimesheetID,
                Comments = command.Comments,
                HourlyRate = command.HourlyRate,
                Hours = command.Hours,
                TimesheetDate = command.TimesheetDate
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [HttpDelete, JsonValidate]
        public ActionResult _Delete(DeleteCommand command)
        {
            var ids = DBSession.tblTimesheets.Where(x => x.TimesheetID == command.TimesheetId).Select(x => new { x.tblLine.JobID, x.LineID }).SingleOrDefault();
            var job = DBSession.tblJobs
                .Include(x => x.tblLines)
                .Include(c => c.tblLines.Select(b => b.tblPurchaseOrders))
                .SingleOrDefault(x => x.JobID == ids.JobID);
            job.DeleteTimesheet(command.TimesheetId);
            DBSession.SaveChanges();

            var response = new DeleteResponse() { TimesheetId = command.TimesheetId, LineId = ids.LineID };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }
    }
}