using System;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class EditModel
    {
        public long TimesheetID { get; set; }
        public string Comments { get; set; }
        public double Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime? TimesheetDate { get; set; }

        public string OperativeName { get; set; }

        public string TimesheetDateString
        {
            get { return TimesheetDate.GetValueOrDefault().ToShortDateString(); }
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}