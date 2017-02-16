using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Web.App.Attributes;
using WebApplication.Controllers.ViewModels.BillOfMaterials;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Attributes;

namespace WebApplication.Controllers
{
    public class BillOfMaterialsController : AbstractController
    {
        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize()]
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
            var Ids = DBSession.tblLines.Where(x => x.LineID == command.LineId).Select(x => new { x.JobID, x.JobLineID  } ).SingleOrDefault();
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == Ids.JobID);
            var bom = job.AddBillOfMaterials(command.LineId, command.Description, command.Cost, command.Quantity,command.Comments, command.PurchaseOrderDate, command.SupplierRef);
            
            DBSession.SaveChanges();

            var bomLine = DBSession.tblLines.SingleOrDefault(x => x.LineID == bom.LineID);

            var response = new CreateResponse()
            {
                JobID = Ids.JobID,
                JobLineID = Ids.JobLineID,
                LineId = command.LineId,
                PurchaseOrderID = bom.PurchaseOrderID,
                Description = bom.Description,
                Cost = command.Cost,
                Quantity = command.Quantity,
                Comments = command.Comments,
                PurchaseOrderDate = command.PurchaseOrderDate,
                SupplierRef = command.SupplierRef,
                CalculatedUnitPrice = bomLine.CalculatedUnitPrice
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _Edit(long? id)
        {
            var vm =
                DBSession.tblPurchaseOrders.Where(x => x.PurchaseOrderID == id).ProjectTo<EditModel>().SingleOrDefault();
            return PartialView(vm);
        }

        [AjaxAuthorise()]
        [HttpPost, JsonValidate]
        public ActionResult _Edit(EditCommand command)
        {
            var line = DBSession.tblPurchaseOrders.Where(x => x.PurchaseOrderID == command.PurchaseOrderID).Select(x => new { x.tblLine.JobID, x.LineID }).SingleOrDefault();
            var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == line.JobID);

            if (job == null)
                return HttpNotFound();
                
            var bomLine = job.UpdateBillOfMaterials(command.PurchaseOrderID, command.Description, command.Cost, command.Quantity, command.Comments, command.PurchaseOrderDate, command.SupplierRef);
            DBSession.SaveChanges();

            var response = new EditResponse()
            {
                LineId = line.LineID,
                BillOfMaterialsId = command.PurchaseOrderID,
                Description = command.Description,
                Cost = command.Cost,
                Quantity = command.Quantity,
                Comments = command.Comments,
                PurchaseOrderDate = command.PurchaseOrderDate,
                SupplierRef = command.SupplierRef,
                CalculatedUnitPrice = bomLine.CalculatedUnitPrice
            };

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }

        [AjaxAuthorise]
        [HttpDelete, JsonValidate]
        public ActionResult _Delete(DeleteCommand command)
        {
            var ids = DBSession.tblPurchaseOrders.Where(x => x.PurchaseOrderID == command.BillOfMaterialsId).Select(x => new { x.tblLine.JobID, x.LineID }).SingleOrDefault();
            var job = DBSession.tblJobs.Where(x => x.JobID == ids.JobID)
                .Include(x=>x.tblLines)
                .Include(c => c.tblLines.Select(b => b.tblPurchaseOrders))
                .SingleOrDefault();

            job.DeleteBillOfMaterials(command.BillOfMaterialsId);
            DBSession.SaveChanges();

            var calculatedUnitPrice = DBSession.tblLines.Where(x => x.LineID == ids.LineID).Select(x=>x.CalculatedUnitPrice).SingleOrDefault();

            var response = new DeleteResponse() {BillOfMaterialsId = command.BillOfMaterialsId, LineId = ids.LineID, CalculatedUnitPrice = calculatedUnitPrice};

            return JsonActionResult(HttpStatusCode.OK, "Success", response);
        }


    }
}