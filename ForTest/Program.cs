using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using Dapper;

namespace ForTest
{
	public class Program
	{
		public static void Main()
		{
			HttpClient client = new HttpClient();
			var resulttast = client.GetStringAsync("http://localhost:13573/api/xk.mobile.user.get?userid=34568");
			string result = resulttast.Result;
			
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

