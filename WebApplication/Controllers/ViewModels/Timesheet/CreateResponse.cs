using System;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class CreateResponse
    {
        public long LineId { get; set; }
        public long TimesheetID { get; set; }
        public long? JobID { get; set; }
        public long JobLineID { get; set; }
        public string UniqueId
        {
            get { return JobID + "/" + JobLineID; }
        }

        public string OperativeName { get; set; }
        public string Comments { get; set; }
        public double Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime? TimesheetDate { get; set; }

        public string TimesheetDateString
        {
            get { return TimesheetDate.GetValueOrDefault().ToShortDateString(); }
        }
    }
}