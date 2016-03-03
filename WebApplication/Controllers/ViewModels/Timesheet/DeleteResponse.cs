using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class DeleteResponse
    {
        public long LineId { get; set; }
        public long TimesheetId { get; set; }

    }
}