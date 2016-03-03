using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraReports.UI;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.Jobs
{
    public partial class Jobs : DevExpress.XtraReports.UI.XtraReport
    {
        public Jobs()
        {
            InitializeComponent();
        }

        public Jobs(DateTime startDate, DateTime endDate)
            : this()
        {
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var jobs = db.tblJobs.Where(x => x.QuoteDate >= startDate && x.QuoteDate < endDate && (x.Status == 4 || x.Status == 8)).ProjectTo<JobsModel.Job>().ToList();
            var model = new JobsModel(startDate, endDate)
            {
                Jobs = jobs
            };

            bindingSource1.DataSource = model;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        }
    }
}
