using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.App
{
    public static class CreateEvent
    {
        [FunctionName("CreateEvent")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "createEvent")]
            HttpRequestMessage req,
            [Queue("emails", Connection = "AzureWebJobsStorage")]
            IAsyncCollector<EmailDetails> emailsQueue,
            [Table("events", Connection = "AzureWebJobsStorage")]
            IAsyncCollector<EventTableEntity> eventsTable,
            ILogger log)
        {
            var eventDetails = await req.Content.ReadAsAsync<EventDetails>();
            var responses = new List<Response>();
            var eventId = Guid.NewGuid().ToString("n");

            foreach (var invitee in eventDetails.Invitees)
            {
                log.LogInformation($"Inviting {invitee.Name} ({invitee.Email})");
                var accessCode = Guid.NewGuid().ToString("n");
                var emailDetails = new EmailDetails
                {
                    EventDateAndTime = eventDetails.EventDateAndTime,
                    Location = eventDetails.Location,
                    Name = invitee.Name,
                    Email = invitee.Email,
                    ResponseUrl =
                        $"https://rayoflight.blob.core.windows.net/index.html?event={eventId}&code={accessCode}"
                };

                await emailsQueue.AddAsync(emailDetails);
                responses.Add(new Response
                {
                    Name = invitee.Name,
                    Email = invitee.Email,
                    IsPlaying = "unknown",
                    ResponseCode = accessCode
                });
            }

            await eventsTable.AddAsync(new EventTableEntity
            {
                PartitionKey = "event",
                RowKey = eventId,
                EventDateAndTime = eventDetails.EventDateAndTime,
                Location = eventDetails.Location,
                ResponsesJson = JsonConvert.SerializeObject(responses)
            });

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}