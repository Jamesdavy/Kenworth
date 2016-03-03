using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class EditModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockedOut
        {
            get
            {
                var lockedDate = LockoutEndDateUtc.GetValueOrDefault().Date;
                var dateNow = DateTime.UtcNow.Date;

                if (lockedDate > dateNow)
                    return true;

                return false;
            }

        }

    }
}