using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Reports.JobCard
{
    public class JobCardModel
    {

        public List<JobCard> JobCards { get; set; }

        public class JobCard
        {
            public long tblJobJobID { get; set; }
            public long JobLineID { get; set; }
            public string tblJobOurOrderReference { get; set; }

            public string UniqueId
            {
                get { return tblJobOurOrderReference + "/" + JobLineID.ToString(); }
            }

            public string ClientName { get; set; }
            public string Description { get; set; }
            public string DrawingNumber { get; set; }
            public double EstimatedHours { get; set; }
        }
    }
}