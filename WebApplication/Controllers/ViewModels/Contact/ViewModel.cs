using System;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class ViewModel
    {
        public long? ContactId { get; set; }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}