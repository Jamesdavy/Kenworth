using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Web.ASPxEditors;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class EditModel
    {
        public long ContactID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Enabled")]
        public bool Status { get; set; }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}