using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
  public class MessageBusClient : IMessageBusClient
  {
    private readonly IConfiguration _config;
    private readonly IConnection _conn;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration config)
    {
      _config = config;
      var factory = new ConnectionFactory() { HostName = _config["RabbitMQHost"], 
                                              Port = int.Parse(_config["RabbitMQPort"])};

      try
      {
        _conn = factory.CreateConnection();
        _channel = _conn.CreateModel();

        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

        _conn.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

        Console.WriteLine("--> Connected to MessageBus");
        
      }
      catch(Exception ex)
      {
        Console.WriteLine($"--> Could not connect to the message bus. {ex.Message}");
      }
    }

    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
      var message = JsonSerializer.Serialize(platformPublishedDto);

      if (_conn.IsOpen)
      {
        Console.WriteLine("--> RabbitMQ Connection Open, sending message");

        //Todo send the message
        SendMessage(message);

      }
      else
      {
        Console.WriteLine("--> RabbitMQ connection is closed, not sending.");
      }
    }

    private void SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(exchange: "trigger", 
                            routingKey:"",
                            basicProperties: null,
                            body: body);

      Console.WriteLine($"--> We have sent {message}");
    }

    public void Dispose()
    {
      Console.WriteLine("MessageBus Disposed");
      if (_channel.IsOpen)
      {
        _channel.Close();
        _conn.Close();
      }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
    {
      Console.WriteLine("--> RabbitMQ Connection Shutdown.");
    }
  }
}