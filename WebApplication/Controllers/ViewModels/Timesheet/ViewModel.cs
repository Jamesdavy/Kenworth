using System;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class ViewModel
    {
        public ViewModel()
        {
            TimesheetDate = DateTime.Now;
        }

        public long? LineID { get; set; }
        public DateTime? TimesheetDate { get; set; }
        public decimal HourlyRate { get { return 42; } }

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