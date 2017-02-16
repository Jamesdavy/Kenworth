using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Infrastructure.Services
{
    public interface IOrderReferenceNumberLookupService
    {
        string GetNextReference(DateTime quoteDate, int status);
    }
}