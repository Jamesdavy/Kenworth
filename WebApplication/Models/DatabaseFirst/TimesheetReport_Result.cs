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
    
    public partial class TimesheetReport_Result
    {
        public string clientcompanyname { get; set; }
        public string description { get; set; }
        public long jobid { get; set; }
        public long joblineid { get; set; }
        public Nullable<double> hours { get; set; }
        public Nullable<double> timesheetvalue { get; set; }
        public double Estimatedhours { get; set; }
        public Nullable<double> estimatedvalue { get; set; }
        public Nullable<double> timediff { get; set; }
        public Nullable<double> estimatedloss { get; set; }
    }
}
