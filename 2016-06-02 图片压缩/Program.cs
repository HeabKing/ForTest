using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesinx.Utility;

namespace _2016_06_02_图片压缩
{
	public class Program
	{
		public class Settings
		{
			public string SrcDir { get; set; }
			public string DesSrc { get; set; }
			public bool ParallelWithoutLog { get; set; }
		}

		static void Main(string[] args)
		{
			string logFile = DateTime.Now.ToString(CultureInfo.InvariantCulture) + Guid.NewGuid() + ".text";
			// 读取配置
			Settings settings = new Settings() { ParallelWithoutLog = true };
			string pathSettings = "settings.json";
			if (!File.Exists(pathSettings))
			{
				File.WriteAllText(pathSettings, Newtonsoft.Json.JsonConvert.SerializeObject(settings));
			}
			var settingsStr = File.ReadAllText("settings.json");
			settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(settingsStr);
			if (!Directory.Exists(settings.SrcDir) || !Directory.Exists(settings.DesSrc))
			{
				string temp = "配置文件中指定的路径不存在";
				Console.WriteLine(temp);
				File.WriteAllText(logFile, temp);
				Console.WriteLine("按任意键退出");
				Console.ReadKey();
				return;
			}
			//var t = VaryImgQuality.VaryAsync(Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"), @"C:\Users\HeabKing\Desktop\456\");
			//var t = VaryImgQuality.VaryAsync(@"C:\Users\HeabKing\Desktop\ZXXKCOM20160602142011752481.jpg");
			// 单线程处理, 有日志
			if (!settings.ParallelWithoutLog)
			{
				Stopwatch sopwatch = Stopwatch.StartNew();
				foreach (var item in Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"))
				{
					try
					{
						var t = VaryImgQuality.VaryAsync(new[] { item }, @"C:\Users\HeabKing\Desktop\单线程\");
						t.Wait();
					}
					catch (Exception e)
					{
						// TODO
						//File.WriteLine
					}
				}
				Console.WriteLine(sopwatch.Elapsed);
			}
			else
			{
				// 并行处理, 没有日志
				Stopwatch sopwatch2 = Stopwatch.StartNew();
				var tt = VaryImgQuality.VaryAsync(Directory.GetFiles(@"C:\Users\HeabKing\Desktop\123\"), @"C:\Users\HeabKing\Desktop\多线程\");
				tt.Wait();
				Console.WriteLine(sopwatch2.Elapsed);

				Console.WriteLine("Done!");
				Console.ReadKey();
			}
		}
	}
}
