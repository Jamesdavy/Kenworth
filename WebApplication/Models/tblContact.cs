using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblContact
    {
        public tblContact(string forename, string surname,
            string position, string phone, string email)
        {
            Forename = forename;
            Surname = surname;
            Position = position;
            Phone = phone;
            Email = email;
            Status = true;
        }
    }
    
    
}