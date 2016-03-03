using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms.VisualStyles;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblClient
    {
        public tblClient(string clientCompanyName, string clientAddress1,
            string clientAddress2, string clientTown, string clientCounty, string clientPostcode,
            string clientTelephone, string clientEmail, string clientFax, string clientWWW,
            string accountsEmail, bool copyToAccounts
            )
        {
            ClientCompanyName = clientCompanyName;
            ClientAddress1 = clientAddress1;
            ClientAddress2 = clientAddress2;
            ClientTown = clientTown;
            ClientCounty = clientCounty;
            ClientPostCode = clientPostcode;
            ClientTelephone = clientTelephone;
            ClientEmail = clientEmail;
            ClientFax = clientFax;
            ClientWWW = clientWWW;
            AccountsEmail = accountsEmail ?? "";
            CopyToAccounts = copyToAccounts;
            Status = true;
        }
    }
}