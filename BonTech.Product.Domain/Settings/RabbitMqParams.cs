namespace BonTech.Product.Domain.Settings;

public class RabbitMqParams
{
    public string QueueName { get; set; }
    
    public string RoutingKey { get; set; }
    
    public string ExchangeName { get; set; }
}