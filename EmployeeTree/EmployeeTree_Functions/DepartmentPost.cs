using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmployeeTree_Functions
{
    public class DepartmentPost
    {
        private readonly ILogger<DepartmentPost> _logger;

        public DepartmentPost(ILogger<DepartmentPost> logger)
        {
            _logger = logger;
        }

        [Function(nameof(DepartmentPost))]
        public async Task Run(
            [ServiceBusTrigger("departmentpost", Connection = "ServiceBus",IsSessionsEnabled = true)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            _logger.LogInformation("Data Posted Successfully");
            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
