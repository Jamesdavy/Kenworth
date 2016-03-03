using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class FileCommand
    {
        public string name { get; set; }
        public string size { get; set; }
        public string type { get; set; }

        public string ext
        {
            get
            {
                if (String.IsNullOrEmpty(name))
                    return "";

                return Path.GetExtension(name);
            }
        }

    }
}