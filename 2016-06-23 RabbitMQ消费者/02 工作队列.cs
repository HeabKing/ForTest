using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace _2016_06_23_RabbitMQ消费者
{
	public class _02_工作队列
	{
		public static void Main()
		{
			var factory = new ConnectionFactory {HostName = "localhost"};
			Console.WriteLine("创建连接...");
			using (var connection = factory.CreateConnection())
			{
				Console.WriteLine("创建消息通道...");
				using (var channel = connection.CreateModel())
				{
					Console.WriteLine("声明队列...");
					channel.QueueDeclare("workqueue", false, false, false, null);
					var consumer = new EventingBasicConsumer(channel);
					
					consumer.Received += (model, ea) =>
					{
						var body = ea.Body;
						var message = Encoding.UTF8.GetString(body);
						Console.WriteLine(" [x] Received {0}", message);

						int dots = message.Split('.').Length - 1;
						Thread.Sleep(dots * 1000);

						Console.WriteLine(" [x] Done");
					};
					channel.BasicConsume(queue: "workqueue", noAck: true/*消息响应*/, consumer: consumer);
					Console.WriteLine("基于事件后台监听, 按任意键退出前台线程...");
					Console.ReadKey();	// 注意: 这里的ReadKey应该在channel等变量没有被销毁的地方, 不然后台无法进行监听

				}
			}

		}
	}
}
