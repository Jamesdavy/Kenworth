using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.DeliveryNote
{
    public class ListModel
    {

        public ListModel(long jobId)
        {
            JobId = jobId;
            DeliveryNotes = new List<DeliveryNote>();
        }

        public long JobId { get; set; }
        public List<DeliveryNote> DeliveryNotes { get; set; }

        public class DeliveryNote
        {
            public long LineID { get; set; }
            public long JobLineID { get; set; }
            public long tblJobJobID { get; set; }

            public string UniqueId
            {
                get { return tblJobJobID + "/" + JobLineID; }
            }

            public string SentStatus
            {
                get
                {
                    if (DeliveryNoteSent)
                        return "Sent " + DeliveryNoteSentDate.GetValueOrDefault().ToShortDateString() + " " + DeliveryNoteId;
                    return "Not Sent";
                }
            }

            public string Description { get; set; }
            public DateTime? ExpectedDeliveryDate { get; set; }
            public double? Quantity { get; set; }
            public double QuantityAlreadyDispatched { get; set; }
            public double LastQuantityDispatched { get; set; }
            public bool DeliveryNoteSent { get; set; }
            public DateTime? DeliveryNoteSentDate { get; set; }
            public long? DeliveryNoteId { get; set; }
        }


        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }


        
    }
}