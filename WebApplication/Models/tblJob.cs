using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using DevExpress.Charts.Native;
using DevExpress.Web.Data;
using DevExpress.XtraRichEdit.Commands;
using Microsoft.AspNet.Identity;
using NLog;
using WebApplication.Controllers.ViewModels.Line;
using WebApplication.Infrastructure.Services;
using WebApplication.Reports.Quote;


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

        private tblJob(long? clientId, long? contactId, string description, string comments, string creatorId)
            : this()
        {
            Description = description;
            Comments = comments;
            ClientID = clientId;
            ContactID = contactId;
            //CreateEnquiry(creatorId);
        }

        private tblJob CopyEnquiry(string creatorId, IOrderReferenceNumberLookupService orderReferenceNumberLookupService)
        {
            var job = new tblJob(ClientID, ContactID, Description, Comments, creatorId);
            //Description = description;
            //Comments = comments;
            //ClientID = clientId;
            //ContactID = contactId;
            job.CreateEnquiry(creatorId, orderReferenceNumberLookupService); 
            return job;
        }


        public void CreateEnquiry(string creatorId, IOrderReferenceNumberLookupService orderReferenceNumberLookupService)
        {
            Status = 16;
            ExpectedDeliveryDate = DateTime.Now.AddMonths(1);
            QuoteDate = DateTime.Now;
            OurOrderReference = orderReferenceNumberLookupService.GetNextReference(QuoteDate.GetValueOrDefault(), Status.GetValueOrDefault());
            AddEvent(JobID, tblEventStore.ForeignKeyType.Job, tblEventStore.EventType.Enquiry, "New Enquiry", creatorId);
        }

        public void TurnEnquiryIntoQuote(string creatorId)
        {
            Status = 2;
            OurOrderReference = FormatOurOrderReference(OurOrderReference, Status.GetValueOrDefault());
            AddEvent(JobID, tblEventStore.ForeignKeyType.Job, tblEventStore.EventType.EnquiryTurnedIntoQuote, "Enquiry Converted to Quote", creatorId);
        }

        public tblLine AddLine(string description, int? status, bool legacyQuote, double? quantity, decimal? unitPrice, DateTime? expectedDeliveryDate, string deliveryComments, string drawingNumber, double estimatedHours, decimal estimatedHourlyRate, string userId)
        {
            var jobLineId = GetNextJobLineId();
            var line = new tblLine(this, description, jobLineId, status, legacyQuote, quantity, unitPrice, expectedDeliveryDate, deliveryComments, drawingNumber, estimatedHours, estimatedHourlyRate);
            tblLines.Add(line);
            AddEvent(jobLineId, tblEventStore.ForeignKeyType.Line, tblEventStore.EventType.NewLine, "Line " + jobLineId + ": " + description + " - Added", userId);
            CalculateJobStatusFromLines();
            OurOrderReference = FormatOurOrderReference(OurOrderReference, Status.GetValueOrDefault());
            return line;
        }

        public tblLine UpdateLine(long lineId, string description, bool legacyQuote, double quantity, decimal unitPrice, DateTime expectedDeliveryDate, string deliveryComments, string drawingNumber, double estimatedHours, decimal estimatedHourlyRate)
        {
            var line = GetLine(lineId);
            line.Description = description;
            line.LegacyQuote = legacyQuote;
            line.Quantity = quantity;
            line.UnitPrice = unitPrice;
            line.ExpectedDeliveryDate = expectedDeliveryDate.Date;
            line.DeliveryComments = deliveryComments;
            line.DrawingNumber = drawingNumber;
            //line.CustomerRef = customerRef ?? "";
            line.EstimatedHours = estimatedHours;
            line.EstimatedHourlyRate = estimatedHourlyRate;
            line.CalculatedUnitPrice = line.CalculateUnitPrice();
            return line;
        }


        public void UpdateLineStatus(long lineId, int status, string userId)
        {
            string newJobStatusName = "";
            string newLineStatusName = "";
            string statusLetter = "";
            var currentJobStatus = Status;
            
            var line = GetLine(lineId);
            var currentLineStatus = line.Status;
            line.Status = status;

            switch (status)
            {
                case 1:
                {
                    line.StartedDate = null;
                    line.CompletedDate = null;
                    line.DeadDate = DateTime.Now;
                    newLineStatusName = "Dead";
                    break;    
                }
                case 2:
                {
                    line.StartedDate = null;
                    line.CompletedDate = null;
                    line.DeadDate = null;
                    newLineStatusName = "Quote";
                    break;
                }
                case 4:
                {
                    line.StartedDate = DateTime.Now;
                    line.CompletedDate = null;
                    line.DeadDate = null;
                    newLineStatusName = "Job";
                    break;
                }
                case 8:
                {
                    line.CompletedDate = DateTime.Now;
                    line.DeadDate = null;
                    newLineStatusName = "Complete";
                    break;
                }
            }

            newJobStatusName = CalculateJobStatusFromLines();

            AddEvent(line.JobLineID, tblEventStore.ForeignKeyType.Line, tblEventStore.EventType.StatusChange, "Line " + line.JobLineID + ": " + line.Description + string.Format(" - Line Changed from {0} to {1}", line.tblStatus.Name, newLineStatusName), userId);

            if (currentJobStatus != Status)
            {
                OurOrderReference = FormatOurOrderReference(OurOrderReference, Status.GetValueOrDefault());

                AddEvent(JobID, tblEventStore.ForeignKeyType.Job, tblEventStore.EventType.StatusChange,
                    string.Format("Order Changed from {0} to {1}", tblStatus.Name, newJobStatusName), userId);
            }
        }

        private string CalculateJobStatusFromLines()
        {
            string newJobStatusName = "";

            if (tblLines.All(x => x.Status == 1))
            {
                Status = 1;
                newJobStatusName = "Dead";
            }
            else if (tblLines.All(x => x.Status == 8 || x.Status == 1))
            {
                Status = 8;
                newJobStatusName = "Complete";
            }
            else if (tblLines.Any(x => x.Status == 4))
            {
                Status = 4;
                newJobStatusName = "Job";
            }
            else
            {
                Status = 2;
                newJobStatusName = "Quote";
            }

            return newJobStatusName;
        }

        private string FormatOurOrderReference(string ourOrderReference, int status)
        {
            string statusLetter = "";

            switch (status)
            {
                case 1:
                {
                    statusLetter = "D";
                    break;    
                }
                case 2:
                {
                    statusLetter = "Q";
                    break;
                }
                case 4:
                {
                    statusLetter = "J";
                    break;
                }
                case 8:
                {
                    statusLetter = "C";
                    break;
                }
            }

            if (ourOrderReference.Length <= 1) return ourOrderReference;

            var newOrderReference = ourOrderReference.TrimEnd().Substring(0, ourOrderReference.Length - 1);
            ourOrderReference = newOrderReference + statusLetter;

            return ourOrderReference;
        }

        public void UpdateDeliveryNoteStatus(long lineId, long deliveryNoteId, double quantityAlreadyDispatched, double quantityToDispatch, string userId, string fileName)
        {
            var line = GetLine(lineId);
            if (quantityToDispatch > 0)
            {
                line.DeliveryNoteSent = true;
                line.DeliveryNoteSentDate = DateTime.Now;
                line.DeliveryNoteId = deliveryNoteId;
                line.LastQuantityDispatched = quantityToDispatch;
            }

            line.QuantityAlreadyDispatched = quantityAlreadyDispatched + quantityToDispatch;

            AddEvent(lineId, tblEventStore.ForeignKeyType.Line, tblEventStore.EventType.Despatch, "Line " + line.JobLineID + ": " + line.Description + string.Format(" - Disptach Qty: {0}", quantityToDispatch), userId, fileName);
        }
        
        //public void UpdateDeliveryNoteStatuses(long[] lineIds, long deliveryNoteId)
        //{
        //    foreach (var lineId in lineIds)
        //    {
        //        var line = GetLine(lineId);
        //        line.DeliveryNoteSent = true;
        //        line.DeliveryNoteSentDate = DateTime.Now;
        //        line.DeliveryNoteId = deliveryNoteId;
        //    }
        //}

        public tblPurchaseOrder AddBillOfMaterials(long lineId, string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            var line = GetLine(lineId);
            var bom = new tblPurchaseOrder(description, cost, quantity, comments, purchaseOrderDate, supplierRef);
            AddBomAndCalculateUnitPrice(line, bom);
            return bom;
        }

        public tblPurchaseOrder AddBillOfMaterials(tblLine line, string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            var bom = new tblPurchaseOrder(description, cost, quantity, comments, purchaseOrderDate, supplierRef);
            AddBomAndCalculateUnitPrice(line, bom);
            return bom;
        }

        private void AddBomAndCalculateUnitPrice(tblLine line, tblPurchaseOrder bom)
        {
            line.tblPurchaseOrders.Add(bom);
            line.CalculatedUnitPrice = line.CalculateUnitPrice();
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

        public void AddFileToLine(long lineId, Guid fileId, string name, string fileName, string contentType)
        {
            var line = GetLine(lineId);
            //line.AddFile(fileId, name, fileName, contentType);
            line.tblFiles.Add(new tblFile(fileId, name, fileName, contentType));
        }

        public void AddFileToJob(Guid fileId, string name, string fileName, string contentType)
        {
            tblJobFiles.Add(new tblJobFile(fileId, name,fileName, contentType));
        }

        public void RemoveFileFromJob(Guid fileId)
        {
            var file = tblJobFiles.SingleOrDefault(x => x.FileId == fileId);
            tblJobFiles.Remove(file);
        }

        public void RemoveFileFromLine(long lineId, Guid fileId)
        {
            var line = GetLine(lineId);
            var file = line.tblFiles.SingleOrDefault(x => x.FileID == fileId);
            line.tblFiles.Remove(file);
            //line.DeleteFile(fileId);
        }


        public tblLine DeleteLine(long lineId)
        {
            var line = GetLine(lineId);
            tblLines.Remove(line);
            return line;
        }

        //public void DeleteDrawing(long lineId)
        //{
        //    var line = GetLine(lineId);
        //    line.ClearFile();
        //    //line.tblFile.FileName = "";
        //    //line.tblFile.ContentType = "";
        //    //line.tblFile.FileID = Guid.Empty;
        //}

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
                    tblLine.CalculatedUnitPrice = tblLine.CalculateUnitPrice();
                }
                
            }
        }

        public tblLine UpdateBillOfMaterials(long purchaseOrderId, string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            var line = tblLines.SingleOrDefault(x => x.tblPurchaseOrders.Any(y => y.PurchaseOrderID == purchaseOrderId));
            var billOfMaterials = line.tblPurchaseOrders.SingleOrDefault(x => x.PurchaseOrderID == purchaseOrderId);
            billOfMaterials.Description = description ?? "";
            billOfMaterials.PurchaseOrderDate = purchaseOrderDate;
            billOfMaterials.Quantity = quantity;
            billOfMaterials.Comments = comments ?? "";
            billOfMaterials.Cost = cost;
            billOfMaterials.SupplierRef = supplierRef ?? "";
            line.CalculatedUnitPrice = line.CalculateUnitPrice();
            return line;
        }

        public tblJob Copy(string creatorId, IOrderReferenceNumberLookupService orderReferenceNumberLookupService)
        {
            //var job = new tblJob(ClientID, ContactID, Description, Comments, creatorId);
            var job = CopyEnquiry(creatorId, orderReferenceNumberLookupService); 

            foreach (var tblLine in tblLines)
            {
                var line = job.AddLine(tblLine.Description, 2, tblLine.LegacyQuote, tblLine.Quantity, tblLine.UnitPrice,
                    DateTime.Now.AddMonths(1), tblLine.Comments, tblLine.DrawingNumber, tblLine.EstimatedHours, tblLine.EstimatedHourlyRate, creatorId);

                foreach (var file in tblLine.tblFiles)
                {
                    var newFile = new tblFile(Guid.NewGuid(), file.Name, file.FileName, file.ContentType);
                    line.AddFile(newFile);
                }
                
                //if (tblLine.tblFiles.Count == 1)
                //{
                //    var currentFile = tblLine.tblFiles.FirstOrDefault();
                //    var file = new tblFile(Guid.NewGuid(), currentFile.Name, currentFile.FileName, currentFile.ContentType);
                //    line.AddFile(file);
                //}

                foreach (var tblPurchaseOrder in tblLine.tblPurchaseOrders)
                {
                    job.AddBillOfMaterials(line, tblPurchaseOrder.Description, tblPurchaseOrder.Cost,
                        tblPurchaseOrder.Quantity, tblPurchaseOrder.Comments, DateTime.Now, tblPurchaseOrder.SupplierRef);
                }

            }

            return job;
        }

        private void AddEvent(long foreignKeyId, tblEventStore.ForeignKeyType foreignKeyTypeId,
            tblEventStore.EventType eventTypeId, string description, string userId, string fileName = "")
        {
            var eventStore = new tblEventStore(foreignKeyId, foreignKeyTypeId, eventTypeId, description, userId, fileName);
            tblEventStores.Add(eventStore);
        }

        public void EmailQuote(tblUser operative, ApplicationUser user)
        {
            var _logger = LogManager.GetCurrentClassLogger();

            Attachment attachment = null;
            MailMessage mm = null;
            SmtpClient smtp = null;

            string toAddress = "";
            string copyToAddress = "";
            var debug = bool.Parse(ConfigurationManager.AppSettings["DebugEmail"]);

            //var job = DBSession.tblJobs.SingleOrDefault(x => x.JobID == command.Id);
            //string userId = User.Identity.GetUserId();
            //var userGuid = new Guid(userId);
            //var user = UserManager.FindById(userId);
            //var operative = DBSession.tblUsers.SingleOrDefault(x => x.UserID == userGuid);

            string fromAddress = /*"info@kenworthengineering.co.uk";*/user.Email;

            //if (job == null)
            //    return HttpNotFound();

            toAddress = tblContact.Email;
            if (debug)
            {
                toAddress = "james_davy@outlook.com";
                copyToAddress = "jamesgdavy@gmail.com";
            }
            else
            {
                if (tblClient.CopyToAccounts)
                {
                    copyToAddress = tblClient.AccountsEmail;
                }
            }

            try
            {
                mm = new MailMessage(fromAddress, toAddress);

                _logger.Debug("From:" + toAddress);
                _logger.Debug("To:" + fromAddress);
                _logger.Debug("Copy To:" + copyToAddress);

                //Dim path As String = HttpContext.Current.Server.MapPath("~/MergeDocuments/")
                var quote = new Quote(JobID, new Guid(user.Id));
                //var quoteFilePath = Server.MapPath("~/Documents/Quote-" + JobID + ".pdf");
                var fileName = "Quote-" + JobID + "-" + Guid.NewGuid() + ".pdf";
                
                var quoteFilePath = HttpRuntime.AppDomainAppPath + "/Documents/" + fileName;
                quote.ExportToPdf(quoteFilePath);
                attachment = new Attachment(quoteFilePath);
                mm.Attachments.Add(attachment);
                mm.CC.Add("Quotations@kenworthengineering.co.uk");
                if (tblClient.CopyToAccounts && tblClient.AccountsEmail != "")
                {
                    mm.CC.Add(copyToAddress);
                }

                mm.Bcc.Add(fromAddress);

                mm.Subject = "Please find attached the Quotation Ref:" + JobID;

                string body = "<p>Dear " + tblContact.Forename + "</p>";
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

                smtp = new SmtpClient();
                if (!debug)
                    smtp.Send(mm);

                AddEvent(JobID, tblEventStore.ForeignKeyType.Job, tblEventStore.EventType.SendQuote, "Quote Emailed to " + toAddress, user.Id, fileName);
                
                _logger.Debug("Send email to:" + toAddress);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                throw new ApplicationException("EmailQuote:SMTP Error:" + ex.Message);
            }
            finally
            {
                if (mm != null) mm.Dispose();
                if (smtp != null) smtp.Dispose();
                if (attachment != null) attachment.Dispose();
            }
        }

    }
}