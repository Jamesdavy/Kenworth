using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblTimesheet
    {
        protected tblTimesheet() { }

        public tblTimesheet(Guid userId, string comments, double hours, decimal hourlyRate, DateTime? timesheetDate)
        {
            UserID = userId;
            Comments = comments ?? "";
            Hours = hours;
            HourlyRate = hourlyRate;
            TimesheetDate = timesheetDate;
        }
    }
}