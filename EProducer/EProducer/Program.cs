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
                /*channel.QueueDeclare(queue: "minhaSegundaFila",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);*/

                int count1 = 0;
                int count2 = 0;

                while (true)
                {
                    string message = $"{count1++} Hello World! na minhaNovaFila";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "minhaNovaExchange",
                                         routingKey: "minhaRoutingKey", 
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    System.Threading.Thread.Sleep(200);


                    string message2 = $"{count2++} Hello World! na minhaSegundaFila";
                    var body2 = Encoding.UTF8.GetBytes(message2);

                    channel.BasicPublish(exchange: "minhaNovaExchange",
                                            routingKey: "minhaSegundaKey",
                                            basicProperties: null,
                                            body: body2);
                    Console.WriteLine(" [x] Sent {0}", message2);

                    System.Threading.Thread.Sleep(500);




                }

               

            }

           
        }
    }
}
