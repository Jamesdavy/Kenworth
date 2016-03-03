using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.DeliveryNote
{
    public class CreateDeliveryNoteCommand
    {
        public long JobId { get; set; }
        public long[] SelectedDeliveryNotes { get; set; }
    }
}