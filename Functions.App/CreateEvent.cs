using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.App
{
    public static class CreateEvent
    {
        [FunctionName("CreateEvent")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = "createEvent")]
            HttpRequestMessage req,
            [Queue("emails", Connection = "AzureWebJobsStorage")]
            IAsyncCollector<EmailDetails> emailsQueue,
            ILogger log)
        {
            var eventDetails = await req.Content.ReadAsAsync<EventDetails>();

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
                    ResponseUrl = $"https://rayoflight.blob.core.windows.net/web/web/index.html?code={accessCode}"
                };
                await emailsQueue.AddAsync(emailDetails);
            }

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}