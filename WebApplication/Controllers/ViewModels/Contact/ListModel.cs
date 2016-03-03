using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class ListModel
    {
        public ListModel()
        {
            Contacts = new List<Contact>();
        }


        public List<Contact> Contacts { get; set; } 

        public class Contact
        {
            public long ContactID { get; set; }
            public string Forename { get; set; }
            public string Surname { get; set; }
            public string Position { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }


        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}