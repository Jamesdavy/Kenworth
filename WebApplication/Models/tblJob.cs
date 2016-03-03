using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Charts.Native;
using DevExpress.XtraRichEdit.Commands;
using WebApplication.Controllers.ViewModels.Line;


namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblJob
    {
        
        //private tblJob(string description, string comments, DateTime? expectedDeliveryDate, int? status) : this()
        //{
        //    Description = description;
        //    Comments = comments;
        //    ExpectedDeliveryDate = expectedDeliveryDate;
        //    Status = status;
        //}

        private tblJob(long? clientId, long? contactId, string description, string comments)
            : this()
        {
            Description = description;
            Comments = comments;
            ClientID = clientId;
            ContactID = contactId;
            CreateQuote();
        } 

        
        public void CreateQuote()
        {
            Status = 2;
            ExpectedDeliveryDate = DateTime.Now.AddMonths(1);
            QuoteDate = DateTime.Now;
        }

        public tblLine AddLine(string description, int? status, double? quantity, decimal? unitPrice, DateTime? expectedDeliveryDate, string deliveryComments, string drawingNumber, string customerRef, double estimatedHours)
        {
            var line = new tblLine(description, GetNextJobLineId(), status, quantity, unitPrice, expectedDeliveryDate, deliveryComments, drawingNumber, customerRef, estimatedHours);
            tblLines.Add(line);
            return line;
        }

        public tblLine UpdateLine(long lineId, string description, double quantity, decimal unitPrice, DateTime expectedDeliveryDate, string deliveryComments, string drawingNumber, string customerRef, double estimatedHours)
        {
            var line = GetLine(lineId);
            line.Description = description;
            line.Quantity = quantity;
            line.UnitPrice = unitPrice;
            line.ExpectedDeliveryDate = expectedDeliveryDate.Date;
            line.DeliveryComments = deliveryComments;
            line.DrawingNumber = drawingNumber;
            line.CustomerRef = customerRef;
            line.EstimatedHours = estimatedHours;
            return line;
        }


        public void UpdateLineStatus(long lineId, int status)
        {

            var line = GetLine(lineId);
            line.Status = status;

            switch (status)
            {
                case 1:
                {
                    line.StartedDate = null;
                    line.CompletedDate = null;
                    line.DeadDate = DateTime.Now;
                    break;    
                }
                case 2:
                {
                    line.StartedDate = null;
                    line.CompletedDate = null;
                    line.DeadDate = null;
                    break;
                }
                case 4:
                {
                    line.StartedDate = DateTime.Now;
                    line.CompletedDate = null;
                    line.DeadDate = null;
                    break;
                }
                case 8:
                {
                    line.CompletedDate = DateTime.Now;
                    line.DeadDate = null;
                    break;
                }
            }
            

            if (tblLines.All(x => x.Status == 1))
            {
                Status = 1;
                return;
            }

            if (tblLines.All(x => x.Status == 8 || x.Status == 1))
            {
                Status = 8;
                return;
            }

            if (tblLines.Any(x => x.Status == 4))
            {
                Status = 4;
                return;
            }

            Status = 2;
        }

        public void UpdateDeliveryNoteStatuses(long[] lineIds, long deliveryNoteId)
        {
            foreach (var lineId in lineIds)
            {
                var line = GetLine(lineId);
                line.DeliveryNoteSent = true;
                line.DeliveryNoteSentDate = DateTime.Now;
                line.DeliveryNoteId = deliveryNoteId;
            }
        }

        public tblPurchaseOrder AddBillOfMaterials(long lineId, string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            var line = GetLine(lineId);
            var bom = new tblPurchaseOrder(description, cost, quantity, comments, purchaseOrderDate, supplierRef);
            line.tblPurchaseOrders.Add(bom);
            return bom;
        }

        public tblPurchaseOrder AddBillOfMaterials(tblLine line, string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            var bom = new tblPurchaseOrder(description, cost, quantity, comments, purchaseOrderDate, supplierRef);
            line.tblPurchaseOrders.Add(bom);
            return bom;
        }


        public tblTimesheet AddTimesheet(long lineId, Guid userId, string comments, double hours, decimal hourlyRate, DateTime? timesheetDate)
        {
            var line = GetLine(lineId);
            var timesheet = new tblTimesheet(userId, comments, hours, hourlyRate, timesheetDate);
            line.tblTimesheets.Add(timesheet);
            return timesheet;
        }

        private long GetNextJobLineId()
        {
            if (tblLines == null)
                return 1;

            if (!tblLines.Any())
                return 1;

            return tblLines.Max(x => x.JobLineID) + 1;
        }

        private tblLine GetLine(long lineId)
        {
            var line = tblLines.Where(x => x.LineID == lineId).SingleOrDefault();
            return line;
        }

        public tblLine DeleteLine(long lineId)
        {
            var line = GetLine(lineId);
            tblLines.Remove(line);
            return line;
        }

        public void DeleteDrawing(long lineId)
        {
            var line = GetLine(lineId);
            line.tblFile.FileName = "";
            line.tblFile.ContentType = "";
            line.tblFile.FileID = Guid.Empty;
        }

        public void DeleteTimesheet(long timesheetId)
        {
            tblTimesheet timesheet;
            foreach (var tblLine in tblLines)
            {
                timesheet = tblLine.GetTimesheet(timesheetId);
                if (timesheet == null)
                    continue;
                tblLine.tblTimesheets.Remove(timesheet);
            }
        }

        public void UpdateTimesheet(long timesheetId, string comments, double hours, decimal hourlyRate, DateTime? timesheetDate)
        {
            var line = tblLines.SingleOrDefault(x => x.tblTimesheets.Any(y => y.TimesheetID == timesheetId));
            var timesheet = line.tblTimesheets.SingleOrDefault(x => x.TimesheetID == timesheetId);
            timesheet.Comments = comments ?? "";
            timesheet.Hours = hours;
            timesheet.HourlyRate = hourlyRate;
            timesheet.TimesheetDate = timesheetDate;
        }

        public void DeleteBillOfMaterials(long billOfMaterialsId)
        {
            tblPurchaseOrder billOfMaterials;
            foreach (var tblLine in tblLines)
            {
                billOfMaterials = tblLine.GetBillOfMaterials(billOfMaterialsId);
                if (billOfMaterials == null)
                    continue;
                {
                    tblLine.tblPurchaseOrders.Remove(billOfMaterials);

                }
            }
        }

        public void UpdateBillOfMaterials(long purchaseOrderId, string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            var line = tblLines.SingleOrDefault(x => x.tblPurchaseOrders.Any(y => y.PurchaseOrderID == purchaseOrderId));
            var billOfMaterials = line.tblPurchaseOrders.SingleOrDefault(x => x.PurchaseOrderID == purchaseOrderId);
            billOfMaterials.Description = description ?? "";
            billOfMaterials.PurchaseOrderDate = purchaseOrderDate;
            billOfMaterials.Quantity = quantity;
            billOfMaterials.Comments = comments ?? "";
            billOfMaterials.Cost = cost;
            billOfMaterials.SupplierRef = supplierRef;
        }

        public tblJob Copy()
        {
            var job = new tblJob(ClientID, ContactID, Description, Comments);
            foreach (var tblLine in tblLines)
            {
                var line = job.AddLine(tblLine.Description, 2, tblLine.Quantity, tblLine.UnitPrice,
                    DateTime.Now.AddMonths(1), tblLine.Comments, tblLine.DrawingNumber, "", tblLine.EstimatedHours);
                if (tblLine.tblFile != null)
                {
                    var file = new tblFile(Guid.NewGuid(), tblLine.tblFile.FileName, tblLine.tblFile.ContentType);
                    line.tblFile = file;
                }

                foreach (var tblPurchaseOrder in tblLine.tblPurchaseOrders)
                {
                    job.AddBillOfMaterials(line, tblPurchaseOrder.Description, tblPurchaseOrder.Cost,
                        tblPurchaseOrder.Quantity, tblPurchaseOrder.Comments, DateTime.Now, tblPurchaseOrder.SupplierRef);
                }

            }

            return job;
        }
    }
}