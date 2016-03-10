using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class CreateViewModel
    {

        public CreateViewModel()
        {
            EstimatedHourlyRate = 42;
        }

        public long JobId { get; set; }
        public decimal EstimatedHourlyRate { get; set; }

        public string ExpectedDeliveryDateString
        {
            get
            {
                return DateTime.Now.AddMonths(1).ToShortDateString();
            }
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}