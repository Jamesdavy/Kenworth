using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class ViewModel
    {
        public ViewModel()
        {
            PurchaseOrderDate = DateTime.Now;
        }

        public long? LineID { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }

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