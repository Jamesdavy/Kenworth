using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class DeleteResponse
    {
        public long LineId { get; set; }
        public long BillOfMaterialsId { get; set; }
        public decimal CalculatedUnitPrice { get; set; }
    }
}