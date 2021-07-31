# [Functions](https://github.com/eduflornet/Serverless/Functions)

This demo contains a simple Azure Functions REST-style API that can be used for CRUD operations on a todo task list.

To test locally with the Azure Storage emulator, you will need the following `local.settings.json` file to be set up:

```js
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  },
  "Host": {
    "CORS": "*"
  }
}
```
A postman collection is also included with the basic structure to test each of the CRUD methods of the API.

I have using the following tools:

- Visual Studio 2019 
- Postman 8.9.1
- netcoreapp3.1
- Microsoft.NET.Sdk.Functions Version="3.0.13"
- Microsoft.Azure.WebJobs.Extensions.Storage Version="4.0.4"

## References:
- [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/)
- [Book: Serverless apps: Architecture, patterns, and Azure implementation](https://docs.microsoft.com/en-us/dotnet/architecture/serverless/)
- [Azure Table storage](https://docs.microsoft.com/en-us/azure/storage/tables/) 
