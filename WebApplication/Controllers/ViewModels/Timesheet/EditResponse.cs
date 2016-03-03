using System;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class EditResponse
    {
        public long LineId { get; set; }
        public long TimesheetId { get; set; }
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