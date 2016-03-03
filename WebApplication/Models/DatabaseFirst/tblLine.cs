//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models.DatabaseFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLine()
        {
            this.tblPurchaseOrders = new HashSet<tblPurchaseOrder>();
            this.tblTimesheets = new HashSet<tblTimesheet>();
        }
    
        public long LineID { get; set; }
        public Nullable<long> JobID { get; set; }
        public string Description { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> ExpectedDeliveryDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string DeliveryComments { get; set; }
        public Nullable<int> Status { get; set; }
        public long JobLineID { get; set; }
        public string DrawingNumber { get; set; }
        public bool DeliveryNoteSent { get; set; }
        public Nullable<System.DateTime> DeliveryNoteSentDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> DeadDate { get; set; }
        public Nullable<System.DateTime> StartedDate { get; set; }
        public Nullable<long> DeliveryNoteId { get; set; }
        public double EstimatedHours { get; set; }
        public string CustomerRef { get; set; }
    
        public virtual tblFile tblFile { get; set; }
        public virtual tblJob tblJob { get; set; }
        public virtual tblStatus tblStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPurchaseOrder> tblPurchaseOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTimesheet> tblTimesheets { get; set; }
    }
}
