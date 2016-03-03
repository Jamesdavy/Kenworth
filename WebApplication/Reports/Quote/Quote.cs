using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraReports.UI;
using Kendo.Mvc.Extensions;
using System.Linq;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.Quote
{
    public partial class Quote : XtraReport
    {
        public Quote()
        {
            InitializeComponent();
        }

        public Quote(long id, Guid userId)
            : this()
        {
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var quote = db.tblJobs.Where(x => x.JobID == id).ProjectTo<QuoteModel>().SingleOrDefault();
            var fromName = db.tblUsers.Where(x => x.UserID == userId).Select(x => x.Forename + " " + x.Surname).SingleOrDefault();
            quote.From = fromName;
            bindingSource1.DataSource = quote;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        }
    }
}
