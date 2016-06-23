using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace _2016_06_23_RabbitMQ生产者
{
	public class _02_工作队列
	{
		public static void Main()
		{
			Console.WriteLine("Create Factory...");
			var factory = new ConnectionFactory {HostName = "localhost"};
			Console.WriteLine("Create Connection...");
			using (var connection = factory.CreateConnection())
			{
				Console.WriteLine("Create Channel...");
				using (var channel = connection.CreateModel())
				{
					Console.WriteLine("Declare 'workqueue' Queue...");
					channel.QueueDeclare("workqueue", false, false, false, null);
					string message = "hello workqueue...";
					// 基于基础内容类创建一个空的内容头
					var properties = channel.CreateBasicProperties();
					properties.Persistent = true;

					var body = Encoding.UTF8.GetBytes(message);
					channel.BasicPublish(exchange: "", routingKey:"workqueue", basicProperties: properties, body: body);
					Console.WriteLine(" set {0}", message);
					Console.WriteLine(" Any Key Quit...");	// 服务端根据"."的个数来Sleep对应的秒数
				}
			}
			Console.ReadKey();
		}
	}
}
