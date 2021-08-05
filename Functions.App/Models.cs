using System;
using System.Collections.Generic;

namespace Functions.App
{
    public class EventDetails
    {
        public DateTime EventDateAndTime { get; set; }
        public string Location { get; set; }
        public List<InviteeDetails> Invitees { get; set; }
    }

    public class InviteeDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class EmailDetails
    {
        public DateTime EventDateAndTime { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ResponseUrl { get; set; }
    }
}