using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.Timesheet
{
    public partial class Timesheet : DevExpress.XtraReports.UI.XtraReport
    {
       public Timesheet()
        {
            InitializeComponent();
        }

        public Timesheet(double? threshhold, double estimatedHourlyRate, DateTime startDate, DateTime endDate)
            : this()
        {
            var clients = new List<TimesheetModel.Timesheet>();
            
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var timesheets = db.TimesheetReport(threshhold, estimatedHourlyRate, startDate, endDate);

            foreach (var timesheet in timesheets)
            {
                var item = new TimesheetModel.Timesheet()
                {
                    ClientCompanyName = timesheet.clientcompanyname,
                    Description = timesheet.description,
                    JobId = timesheet.jobid,
                    JobLineId = timesheet.joblineid,
                    Hours = timesheet.hours,
                    TimesheetValue = timesheet.timesheetvalue,
                    EstimatedHours = timesheet.Estimatedhours,
                    EstimatedValue = timesheet.estimatedvalue,
                    TimeDiff = timesheet.timediff,
                    EstimatedLoss = timesheet.estimatedloss
                };
                clients.Add(item);
            }

            var model = new TimesheetModel(threshhold, estimatedHourlyRate, startDate, endDate)
            {
                Timesheets = clients
            };

            bindingSource1.DataSource = model;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        }

    }
}
