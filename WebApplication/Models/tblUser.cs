using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblUser
    {
        public tblUser(Guid userId, string forename, string surname, string telephone, string position, bool statusId)
        {
            UserID = userId;
            Forename = forename;
            Surname = surname;
            StatusID = statusId;
            Telephone = telephone;
            Position = position;
        }

        public tblUser(string forename, string surname, string telephone, string position, bool statusId)
        {
            UserID = Guid.NewGuid();
            Forename = forename;
            Surname = surname;
            StatusID = statusId;
            Telephone = telephone;
            Position = position;
        }
    }
}