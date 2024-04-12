using Azure.Messaging.ServiceBus;
using Employee.Requests;
using System.Text.Json;

namespace Employee.Service.NonActionMethod
{
    public class Department_NonAction : INonAction
    {
        private IConfiguration _configuration;

        public Department_NonAction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DepartServiceBus(List<DepartmentRequest> result)
        {
            var sessionId = Guid.NewGuid().ToString();
            var serializedData = JsonSerializer.Serialize(result);
            var client = new ServiceBusClient(_configuration.GetConnectionString("ServiceBus"));
            var sender = client.CreateSender(_configuration["QueueName:DepartmentQueue"]);
            var send = new ServiceBusMessage(serializedData);
            send.SessionId = sessionId;
            Console.WriteLine("Sending Message...");
            await sender.SendMessageAsync(send);
            await sender.CloseAsync();
            Console.WriteLine("Sent Message...");
        }

    }
}
