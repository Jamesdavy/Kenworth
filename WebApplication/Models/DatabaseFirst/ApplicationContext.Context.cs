﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ApplicationEntities : DbContext
    {
        public ApplicationEntities()
            : base("name=ApplicationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblContact> tblContacts { get; set; }
        public virtual DbSet<tblDeliveryNote> tblDeliveryNotes { get; set; }
        public virtual DbSet<tblJob> tblJobs { get; set; }
        public virtual DbSet<tblUpload> tblUploads { get; set; }
        public virtual DbSet<tblUploadType> tblUploadTypes { get; set; }
        public virtual DbSet<tblStatus> tblStatuses { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblClient> tblClients { get; set; }
        public virtual DbSet<tblPurchaseOrder> tblPurchaseOrders { get; set; }
        public virtual DbSet<tblTimesheet> tblTimesheets { get; set; }
        public virtual DbSet<tblFile> tblFiles { get; set; }
        public virtual DbSet<tblLine> tblLines { get; set; }
        public virtual DbSet<tblJobFile> tblJobFiles { get; set; }
        public virtual DbSet<tblEventStore> tblEventStores { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<OrderReferenceNumberLookup> OrderReferenceNumberLookups { get; set; }
    
        public virtual ObjectResult<ClientConversionReport_Result> ClientConversionReport(Nullable<double> threshhold, Nullable<System.DateTime> startdate, Nullable<System.DateTime> enddate)
        {
            var threshholdParameter = threshhold.HasValue ?
                new ObjectParameter("Threshhold", threshhold) :
                new ObjectParameter("Threshhold", typeof(double));
    
            var startdateParameter = startdate.HasValue ?
                new ObjectParameter("Startdate", startdate) :
                new ObjectParameter("Startdate", typeof(System.DateTime));
    
            var enddateParameter = enddate.HasValue ?
                new ObjectParameter("Enddate", enddate) :
                new ObjectParameter("Enddate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientConversionReport_Result>("ClientConversionReport", threshholdParameter, startdateParameter, enddateParameter);
        }
    
        public virtual ObjectResult<TimesheetReport_Result> TimesheetReport(Nullable<double> threshhold, Nullable<double> estimatedHourlyRate, Nullable<System.DateTime> startdate, Nullable<System.DateTime> enddate)
        {
            var threshholdParameter = threshhold.HasValue ?
                new ObjectParameter("Threshhold", threshhold) :
                new ObjectParameter("Threshhold", typeof(double));
    
            var estimatedHourlyRateParameter = estimatedHourlyRate.HasValue ?
                new ObjectParameter("EstimatedHourlyRate", estimatedHourlyRate) :
                new ObjectParameter("EstimatedHourlyRate", typeof(double));
    
            var startdateParameter = startdate.HasValue ?
                new ObjectParameter("Startdate", startdate) :
                new ObjectParameter("Startdate", typeof(System.DateTime));
    
            var enddateParameter = enddate.HasValue ?
                new ObjectParameter("Enddate", enddate) :
                new ObjectParameter("Enddate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimesheetReport_Result>("TimesheetReport", threshholdParameter, estimatedHourlyRateParameter, startdateParameter, enddateParameter);
        }
    }
}
