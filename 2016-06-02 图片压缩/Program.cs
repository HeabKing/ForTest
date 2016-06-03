using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesinx.Utility;

namespace _2016_06_02_图片压缩
{
	public class Program
	{
		static void Main(string[] args)
		{


			// 测试 390 个图片 282 M 11秒钟
			Stopwatch sopwatch = Stopwatch.StartNew();
			foreach (var item in Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"))
			{
				var t = VaryImgQuality.VaryAsync(new[] { item }, @"C:\Users\HeabKing\Desktop\单线程\");
				t.Wait();
			}
			//var t = VaryImgQuality.VaryAsync(Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"), @"C:\Users\HeabKing\Desktop\456\");
			//var t = VaryImgQuality.VaryAsync(@"C:\Users\HeabKing\Desktop\ZXXKCOM20160602142011752481.jpg");
			Console.WriteLine(sopwatch.Elapsed);

			Stopwatch sopwatch2 = Stopwatch.StartNew();
			var tt = VaryImgQuality.VaryAsync(Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"), @"C:\Users\HeabKing\Desktop\多线程\");
			tt.Wait();
			Console.WriteLine(sopwatch2.Elapsed);

			Console.WriteLine("Done!");
			Console.ReadKey();
		}


	}
}
