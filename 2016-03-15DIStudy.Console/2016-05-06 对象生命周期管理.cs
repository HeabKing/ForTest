using System.Diagnostics;
using Autofac;

namespace _2016_03_15DIStudy.Console
{
	/// <summary>
	///     针对的是实现了IDispose的Autofac不允许被垃圾回收期回收的对象
	/// </summary>
	internal class _2016_05_06_对象生命周期管理
	{
		public static void Main()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<Foo>().As<IFoo>(); // 注册
			var container = builder.Build(); // 建立容器

			while (true)
			{
				var obj = container.Resolve<IFoo>(); // 解析对象(建立对象)

				// 显示程序目前占用了多少内存
				var pro = Process.GetCurrentProcess();
				System.Console.WriteLine($"已经配置了: {pro.PrivateMemorySize64/(1024*1024)} MB");
			}
				// 本来每次循环结束以后, 分配的内存是要释放掉的, 这里不释放的原因是: 默认情况下, Autofac会追踪任何由它建立的disposable objects, 而且这个追踪动作是深层的, 是连锁反应的(即: 深层解析), 如果在解析IFoo的过程中发现实现类型Foo的构造函数需要注入IBar对象, Autofac就会先解析IBar并建立Bar对象, 同理, 在解析IBar时, 如果他的实现类型的构造函数又需要IThud对象, 则又会先解析IThud, 依此类推, 在这一路联动解析中所创建的依赖对象, 只要他们有实现IDisposable接口, Autofac就会加以追踪, 亦即Autofac容器内部会有变量一直参考这那些对象, 这就是内存不被释放的原因, 其他容器如Unity则不会
		}

		private interface IFoo
		{
		}

		/// <summary>
		///     单纯用来消耗内存, 每次建立消耗1M
		/// </summary>
		private class Foo : IFoo /*, IDisposable*/ // 注释掉就不会导致内存不足, IDisposable如同C++的析构函数
		{
			private byte[] _buf = new byte[1024*1024];

			public void Dispose()
			{
				System.Console.WriteLine("Entering Foo.Dispose");
			}
		}
	}
}