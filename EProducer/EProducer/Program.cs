using RabbitMQ.Client;
using System;
using System.Text;

namespace EProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "minhaSegundaFila",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int count = 0;

                while (true)
                {
                    string message = $"{count ++} Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "minhaNovaExchange",
                                         routingKey: "minhaSegundaKey",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    System.Threading.Thread.Sleep(200);

                }
             
            }

           
        }
    }
}
