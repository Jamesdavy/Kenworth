using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class EditResponse
    {
        public long LineId { get; set; }
        public long BillOfMaterialsId { get; set; }
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