using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class EditModel
    {
        public long? PurchaseOrderID { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public long Quantity { get; set; }
        public string Comments { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string SupplierRef { get; set; }

        public string PurchaseOrderDateString
        {
            get { return PurchaseOrderDate.GetValueOrDefault().ToShortDateString(); }
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}