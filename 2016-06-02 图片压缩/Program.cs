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
			var t = VaryImgQuality.VaryAsync(Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"), @"C:\Users\HeabKing\Desktop\456\");
			//var t = new VaryImgQuality().VaryAsync(@"C:\Users\HeabKing\Desktop\20120812103725 - 副本 (10) - 副本.jpg50.jpg");
			t.Wait();
			Console.WriteLine(sopwatch.Elapsed);
			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		
	}
}
