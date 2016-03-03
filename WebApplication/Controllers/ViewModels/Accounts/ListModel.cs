using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelegateDecompiler;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class ListModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }

        [Computed]
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