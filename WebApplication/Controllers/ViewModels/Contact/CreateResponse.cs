using System;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class CreateResponse
    {
        public long ClientId { get; set; }
        public long ContactId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}