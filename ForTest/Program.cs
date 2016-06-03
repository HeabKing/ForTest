using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using Common.MessageService;

namespace ForTest
{
	public class Program
	{
		public static void Main()
		{
			string f1 = @"c:\fdsa\432";
			string f2 = @"c:\fdsa\432\";
			string f3 = @"c:\fdsa\432\a";
			string f4 = @"c:\fdsa\432\a.text";
			System.Console.WriteLine(Path.GetDirectoryName(f1));
			System.Console.WriteLine(Path.GetDirectoryName(f2));
			System.Console.WriteLine(Path.GetDirectoryName(f3));
			System.Console.WriteLine(Path.GetDirectoryName(f4));
		}

		public static void Main1()
		{
			var arrInt = new []
			{
				40,
				41,
				60,
				62,
				64,
				44,
				59,
				58,
				92,
				34,
				47,
				91,
				93,
				63,
				61,
				123,
				125,
			};
			for (int i = 0; i <= 33; i++)
			{
				Debug.WriteLine((char)i);
			}
			arrInt.ToList().ForEach(s=>Debug.WriteLine((char)s));
			
			bool[] a = new bool[2];
			System.Console.WriteLine(a[0]);
		}
	}

	#region 2016-03-16 DI

	//public class Program
	//{
	//	interface IOutput
	//	{
	//		void Write(string content);
	//	}

	//	class ConsoleOutput : IOutput
	//	{
	//		public void Write(string content)
	//		{
	//			Console.WriteLine(content);
	//		}
	//	}

	//	interface IShowToday
	//	{
	//		void Show();
	//	}

	//	class ShowToday : IShowToday
	//	{
	//		public IOutput Output { get; set; }
	//		public ShowToday(IOutput output)
	//		{
	//			Output = output;
	//		}
	//		public ShowToday()
	//		{

	//		}
	//		public void Show()
	//		{
	//			Output.Write(DateTime.Now.ToString());
	//		}
	//	}

	//	public static void Main()
	//	{
	//		var builder = new ContainerBuilder();
	//		builder.RegisterType<ConsoleOutput>().As<IOutput>();
	//		// 使用lambda表达式注册 - 属性注入
	//		//builder.Register(c => new ShowToday { Output = c.Resolve<IOutput>()}).As<IShowToday>();
	//		//builder.Register(c => new ShowToday()).OnActivated(e => e.Instance.Output = e.Context.Resolve<IOutput>()).As<IShowToday>(); // For 循环依赖
	//		// 反射组件注册 - 属性注入
	//		//builder.RegisterType<ShowToday>().PropertiesAutowired().As<IShowToday>();	// 使用PropertiesAutowired()实现自动连接 条件: 在反射组件注册使用
	//		builder.RegisterType<ShowToday>().WithProperty("OutPut", new ConsoleOutput()).As<IShowToday>();
	//		var container = builder.Build();

	//		var today = container.Resolve<IShowToday>();
	//		today.Show();
	//	}

	//} 

	#endregion

	#region 2016-03-15 Dapper

	//public class Program
	//{
	//	public static void Main()
	//	{
	//		using (SqlConnection conn = new SqlConnection("Data Source=qds150599512.my3w.com;Initial Catalog=qds150599512_db;User Id=qds150599512;Password=he394899990"))
	//		{
	//			var guid = Guid.NewGuid();
	//			var result = conn.QueryMultiple(@"SELECT 1 A,2 B;SELECT 3 C,4 D,5 E");
	//			var r1 = result.Read();
	//			var r2 = result.Read();
	//			//var r3 = result.Read();	// 异常
	//		}
	//	}
	//} 

	#endregion

	#region 2016-03-15 DI

	//public class Program
	//{
	//	public interface IOutPut
	//	{
	//		void Write(string content);
	//	};

	//	public class ConsoleOutput : IOutPut
	//	{
	//		public void Write(string content)
	//		{
	//			Console.WriteLine(content);
	//		}
	//	}

	//	public static IContainer StartUp()
	//	{
	//		// 容器创建器
	//		var builder = new ContainerBuilder();

	//		// 将自己公开
	//		builder.RegisterType<ConsoleOutput>();

	//		return builder.Build();
	//	}

	//	// 通过IContainer创建的是整个应用程序声明周期的组件
	//	private static IContainer Container { get; set; }

	//	static void Main()
	//	{
	//		Container = StartUp();
	//		ConsoleOutput console = Container.Resolve<ConsoleOutput>();
	//		console.Write("Hello World");
	//	}
	//} 

	#endregion
}