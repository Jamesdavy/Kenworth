using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblPurchaseOrder
    {
        protected tblPurchaseOrder() { }

        public tblPurchaseOrder(string description, decimal cost, long quantity, string comments, DateTime? purchaseOrderDate, string supplierRef)
        {
            Description = description;
            Cost = cost;
            PurchaseOrderDate = purchaseOrderDate;
            Quantity = quantity;
            Comments = comments ?? "";
            SupplierRef = supplierRef ?? "";
        }
    }
}