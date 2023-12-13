using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BonTech.Product.Application.Consumers;

public class RabbitMqConsumer
{
    public void ReadMessage()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("students", exclusive: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Message received: {message}");
        };

        channel.BasicConsume(queue: "students", autoAck: true, consumer: consumer);
    }
}