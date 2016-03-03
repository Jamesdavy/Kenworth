using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraReports.UI;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.Quotes
{
    public partial class Quotes : DevExpress.XtraReports.UI.XtraReport
    {
        public Quotes()
        {
            InitializeComponent();
        }

        public Quotes(DateTime startDate, DateTime endDate)
            : this()
        {
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var quotes = db.tblJobs.Where(x => x.QuoteDate >= startDate && x.QuoteDate < endDate).ProjectTo<QuotesModel.Quote>().ToList();
            var model = new QuotesModel(startDate, endDate)
            {
                Quotes = quotes
            };

            bindingSource1.DataSource = model;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        }

    }
}
