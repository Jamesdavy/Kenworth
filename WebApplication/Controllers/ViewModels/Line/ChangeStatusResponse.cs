using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class ChangeStatusResponse
    {
        public long LineId { get; set; }
        public string Status { get; set; }
        public string JobStatus { get; set; }
        public string OurOrderReference { get; set; }
    }
}