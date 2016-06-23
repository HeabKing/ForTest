using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace _2016_06_23_RabbitMQ生产者
{
	class Send
	{
		static void Main(string[] args)
		{
			// 创建对服务器的连接
			var factory = new ConnectionFactory { HostName = "localhost" };
			//factory.UserName = "";	// 使用默认的用户名 
			//factory.Password = "";
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					// 在创建队列的时候, 只有RabbitMQ上该队列不存在才会去创建
					channel.QueueDeclare(queue: "hello", durable: false/*长期*/, exclusive: false/*独占*/, autoDelete: false, arguments: null);
					string message = "Hello World!";
					var body = Encoding.UTF8.GetBytes(message);	// 消息是以二进制传输的
					channel.BasicPublish(exchange:"", routingKey: "hello", basicProperties: null, body: body);
					Console.WriteLine(" [x] Send {0}", message);
				}
				Console.WriteLine(" Press [Enter] to exit.");
				Console.ReadKey();	// 注意: 这里的ReadKey应该在channel等变量没有被销毁的地方, 不然后台无法进行监听 
			}
		}
	}
}
