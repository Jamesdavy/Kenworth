using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraReports.UI;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.JobCard
{
    public partial class JobCard : DevExpress.XtraReports.UI.XtraReport
    {

        public JobCard()
        {
            InitializeComponent();
        }

        public JobCard(long id)
            : this()
        {
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var jobCards = db.tblLines.Where(x => x.JobID == id).ProjectTo<JobCardModel.JobCard>().ToList();
            var model = new JobCardModel()
            {
                JobCards = jobCards
            };

            bindingSource1.DataSource = model;
        }

        //private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        //}
    }
}
