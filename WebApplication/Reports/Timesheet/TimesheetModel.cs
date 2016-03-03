using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Reports.Timesheet
{
    public class TimesheetModel
    {
        public TimesheetModel(double? threshhold, double? estimatedHourlyRate, DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Threshhold = threshhold;
            EstimatedHourlyRate = estimatedHourlyRate;
            Timesheets = new List<Timesheet>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double? Threshhold { get; set; }
        public double? EstimatedHourlyRate { get; set; }
        public List<Timesheet> Timesheets { get; set; } 

        public class Timesheet
        {
            public string ClientCompanyName { get; set; }
            public string Description { get; set; }
            public long JobId { get; set; }
            public long JobLineId { get; set; }

            public string UniqueId
            {
                get { return JobId + "/" + JobLineId; }
            }

            public double? Hours { get; set; }
            public double? TimesheetValue { get; set; }
            public double? EstimatedHours { get; set; }
            public double? EstimatedValue { get; set; }
            public double? TimeDiff { get; set; }
            public double? EstimatedLoss { get; set; }
        }
    }
}