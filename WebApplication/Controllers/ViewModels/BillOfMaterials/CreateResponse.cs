using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class CreateResponse
    {
        public long LineId { get; set; }
        public long? JobID { get; set; }
        public long JobLineID { get; set; }
        public string UniqueId
        {
            get { return JobID + "/" + JobLineID; }
        }

        public long PurchaseOrderID { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public long Quantity { get; set; }
        public string Comments { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string SupplierRef { get; set; }
        public decimal CalculatedUnitPrice { get; set; }

        public string PurchaseOrderDateString
        {
            get { return PurchaseOrderDate.GetValueOrDefault().ToShortDateString(); }
        }
    }
}