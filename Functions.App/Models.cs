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

    public class EventTableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public string Location { get; set; }
        public string ResponsesJson { get; set; }
    }

    public class Response
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string IsPlaying { get; set; }
        public string ResponseCode { get; set; }
    }
}