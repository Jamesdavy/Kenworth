using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WebApplication.Reports.DeliveryNote
{
    public class DeliveryNoteModel
    {

        public long DeliveryNoteID { get; set; }
        public DateTime DeliveryNoteDate { get; set; }
        public string tblClientsClientCompanyName { get; set; }
        public string JobId { get; set; }
        public string OurOrderReference { get; set; }
        public string CustomerRef { get; set; }

            
        public List<Line> tblLines { get; set; }

        public class Line
        {
            public string UniqueId
            {
                get { return JobLineID.ToString(); }
            }

            public long? JobID { get; set; }
            public long JobLineID { get; set; }
            public string Description { get; set; }
            public string DrawingNumber { get; set; }
            public double? Quantity { get; set; }
            public double QuantityAlreadyDispatched { get; set; }
            public double LastQuantityDispatched { get; set; }

            public double QuantityToFollow
            {
                get { return Quantity.GetValueOrDefault() - QuantityAlreadyDispatched; }
            }


        }
    }
}