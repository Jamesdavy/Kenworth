using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DevExpress.Charts.Native;
using DevExpress.XtraReports.UI;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.ClientConversion
{
    public partial class ClientConversion : DevExpress.XtraReports.UI.XtraReport
    {
        public ClientConversion()
        {
            InitializeComponent();
        }

        public ClientConversion(double? threshhold, DateTime startDate, DateTime endDate)
            : this()
        {
            var clients = new List<ClientConversionModel.Client>();
            
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var clientConvertsion = db.ClientConversionReport(threshhold, startDate, endDate);

            foreach (var clientConversionReportResult in clientConvertsion)
            {
                var client = new ClientConversionModel.Client()
                {
                    ClientCompanyName = clientConversionReportResult.ClientCompanyName,
                    QuotedValue = clientConversionReportResult.QuotedValue,
                    QuoteValue = clientConversionReportResult.QuoteValue,
                    WOPValue = clientConversionReportResult.WOPValue,
                    CompletedValue = clientConversionReportResult.CompletedValue,
                    DeadValue = clientConversionReportResult.DeadValue,
                    QuotePercentage = clientConversionReportResult.QuotePercentage,
                    CompletedPercentage = clientConversionReportResult.CompletedPercentage,
                    WOPPercentage = clientConversionReportResult.WOPPercentage,
                    DeadPercentage = clientConversionReportResult.DeadPercentage
                };
                clients.Add(client);
            }

            var model = new ClientConversionModel(threshhold, startDate, endDate)
            {
                Clients = clients
            };

            bindingSource1.DataSource = model;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        }

    }
}
