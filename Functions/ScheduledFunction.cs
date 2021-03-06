using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public static class ScheduledFunction
    {
        [FunctionName("ScheduledFunction")]
        // CRON every minute expression with TimeSpan "00:01:00" for the purpose of this test
        public static async Task Run([TimerTrigger("00:01:00")] TimerInfo myTimer,
            [Table("todos", Connection = "AzureWebJobsStorage")]
            CloudTable todoTable,
            ILogger log)
        {
            var query = new TableQuery<TodoTableEntity>();
            var segment = await todoTable.ExecuteQuerySegmentedAsync(query, null);
            var deleted = 0;
            foreach (var todo in segment)
                if (todo.IsCompleted)
                {
                    await todoTable.ExecuteAsync(TableOperation.Delete(todo));
                    deleted++;
                }

            log.LogInformation($"Deleted {deleted} items at {DateTime.Now}");
        }
    }
}