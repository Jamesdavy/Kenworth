using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.App.Attributes;
using WebApplication.Infrastructure;

namespace WebApplication.Controllers
{
    [AjaxAuthorise]
    public class JsonController : AbstractController
    {
        public ContentResult _GetMatchingJobLineID(string q, int limit, Int64 timestamp)
        {
            StringBuilder responseContentBuilder = new StringBuilder();

            var clients = DBSession.tblLines.Where(x =>
                (
                    x.JobID.ToString() + "/" + x.JobLineID.ToString()).StartsWith(q.ToLower())
                    && (x.Status == 2 || x.Status == 4 )
                    && (x.tblJob.Status == 2 || x.tblJob.Status == 4)
                    && x.tblJob.tblClient.Status
                ).Select(x => new SelectListItem
                {
                    ID = x.LineID.ToString(),
                    Name = x.JobID + "/" + x.JobLineID + " " + x.Description + " " + x.tblJob.Description + " (" + x.tblJob.tblClient.ClientCompanyName + ")"
                })
            .Take(limit)
            .ToList();

            if (clients.Count > 0)
            {
                foreach (var client in clients)
                    responseContentBuilder.Append(String.Format("{0}|{1}\n", client.ID, client.Name));
            }
            else
            {
                responseContentBuilder.Append(String.Format("{0}|{1}\n", "", "Not Found"));
            }

            return Content(responseContentBuilder.ToString());
        }

        public ContentResult _GetMatchingOperatives(string q, int limit, Int64 timestamp)
        {
            StringBuilder responseContentBuilder = new StringBuilder();

            var clients = DBSession.tblUsers.Where(x =>
                
                    x.Forename.StartsWith(q.ToLower())
                    && x.StatusID == true
                ).Select(x => new SelectListItem
                {
                    ID = x.UserID.ToString(),
                    Name = x.Forename + " " + x.Surname
                })
            .Take(limit)
            .ToList();

            foreach (var client in clients)
                responseContentBuilder.Append(String.Format("{0}|{1}\n", client.ID, client.Name));
            return Content(responseContentBuilder.ToString());
        }

        public ContentResult _GetMatchingLineDescription(string q, int limit, Int64 timestamp)
        {
            StringBuilder responseContentBuilder = new StringBuilder();

            var clients = DBSession.tblLines.Where(x =>

                    x.Description.Trim().StartsWith(q.ToLower().Trim())
                ).Select(x => new SelectListItem
                {
                    ID = x.Description,
                    Name = x.Description
                })
            .Take(limit).Distinct()
            .ToList();

            foreach (var client in clients)
                responseContentBuilder.Append(String.Format("{0}|{1}\n", client.ID, client.Name));
            return Content(responseContentBuilder.ToString());
        }

        public ContentResult _GetMatchingBOMDescription(string q, int limit, Int64 timestamp)
        {
            StringBuilder responseContentBuilder = new StringBuilder();

            var clients = DBSession.tblPurchaseOrders.Where(x =>

                    x.Description.Trim().Contains(q.ToLower().Trim())
                ).Select(x => new SelectListItem
                {
                    ID = x.Description,
                    Name = x.Description
                })
            .Take(limit).Distinct()
            .ToList();

            foreach (var client in clients)
                responseContentBuilder.Append(String.Format("{0}|{1}\n", client.ID, client.Name));
            return Content(responseContentBuilder.ToString());
        }

        public ContentResult _GetMatchingClients(string q, int limit, Int64 timestamp)
        {
            StringBuilder responseContentBuilder = new StringBuilder();

            var clients = DBSession.tblClients.Where(x => x.ClientCompanyName.ToLower().StartsWith(q.ToLower()) && x.Status).Select(x => new JsonController.SelectListItem()
            {
                ID = x.ClientID.ToString(),
                Name = x.ClientCompanyName
            }).ToList();

            foreach (var client in clients)
                responseContentBuilder.Append(String.Format("{0}|{1}\n", client.ID, client.Name));
            return Content(responseContentBuilder.ToString());
        }

        public ContentResult _GetMatchingContacts(long id, string q, int limit, Int64 timestamp)
        {
            StringBuilder responseContentBuilder = new StringBuilder();

            var clients = DBSession.tblContacts.Where(x => 
                x.Forename.ToLower().StartsWith(q.ToLower())
                && x.Status == true
                && x.ClientID == id
                ).Select(x => new JsonController.SelectListItem()
            {
                ID = x.ContactID.ToString(),
                Name = x.Forename + " " + x.Surname
            }).ToList();

            if (clients.Count > 0)
            {
                foreach (var client in clients)
                    responseContentBuilder.Append(String.Format("{0}|{1}\n", client.ID, client.Name));
            }
            else
            {
                responseContentBuilder.Append(String.Format("{0}|{1}\n", "", ""));
            }

            return Content(responseContentBuilder.ToString());
        }


        public class SelectListItem
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }

    }


}