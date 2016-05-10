using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace _2016_05_10_Unity
{
	class Program
	{
		static void Main1(string[] args)
		{
			// 建立Unity容器
			IUnityContainer container = new UnityContainer();
			// 向Unity容器注册类型
			container.RegisterType<ISayHello, SayHelloInEnglist>();
			// 在程序某处要求解析类型, 已取得组件的实例
			ISayHello hello = container.Resolve<ISayHello>();
			// 调用组件的方法
			hello.Run();

			Console.ReadKey();
		}

		interface ISayHello
		{
			void Run();
		}

		class SayHelloInEnglist : ISayHello
		{
			public void Run()
			{
				Console.WriteLine("Hello Unity!");
			}
		}

		class SayHelloInChinese : ISayHello
		{
			public void Run()
			{
				Console.WriteLine("哈罗, Unity!");
			}
		}
	}
}
