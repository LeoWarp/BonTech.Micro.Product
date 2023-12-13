using System.Text;
using BonTech.Product.Producer.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BonTech.Product.Producer;

public class ProductProducer : IMessageProducer
{
    public void SendMessage<T>(T message, string routingKey, string exchange = default)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var json = JsonConvert.SerializeObject(message, Formatting.Indented, 
            new JsonSerializerSettings() 
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange, routingKey, body: body);
    }
}