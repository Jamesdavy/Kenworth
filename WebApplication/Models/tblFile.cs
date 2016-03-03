using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblFile
    {

        public tblFile() { }

        public tblFile(Guid fileId, string fileName, string contentType)
        {
            FileID = fileId;
            FileName = fileName;
            ContentType = contentType;
        }
    }

    
}