using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class ChangeStatusCommand
    {
        public long LineId { get; set; }
        public int Status { get; set; }
    }
}