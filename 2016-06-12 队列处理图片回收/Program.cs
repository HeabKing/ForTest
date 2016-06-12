using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace _2016_06_12_队列处理图片回收
{
	class Program
	{
		private static readonly Queue<KeyValuePair<string, int>> Queue = new Queue<KeyValuePair<string, int>>();
		static void Main(string[] args)
		{
			var files = Directory.GetFiles(@"C:\Users\HeabKing\Desktop\单线程\");
			File.Open(files.LastOrDefault(), FileMode.Open);
			ImgProcessQueue queue = new ImgProcessQueue();
			var t = queue.StartAsync(new TimeSpan(0, 0, 2));
			foreach (var item in files)
			{
				queue.Add(item);
			}
			foreach (var item in files)
			{
				queue.Add(item);
			}
			t.Wait();
		}
	}

	public class ImgProcessQueue
	{
		private readonly Queue<KeyValuePair<string, int>> _queue = new Queue<KeyValuePair<string, int>>();
		private ILog _logger;

		/// <summary>
		/// 开始处理
		/// </summary>
		/// <param name="timeSpan">轮询处理的时间间隔</param>
		/// <param name="action">可选, 对每个图片的处理, 默认为删除操作</param>
		/// <returns></returns>
		public async Task StartAsync(TimeSpan timeSpan, Action<string> action = null)
		{
			// 默认为删除操作
			if (action == null)
			{
				action = File.Delete;
			}
			if (_logger == null)
			{
				log4net.Config.XmlConfigurator.Configure(); // 通过xml注册
				_logger = LogManager.GetLogger("testApp.Logging");
				_logger.Info("开始处理图片...");
			}
			while (true)
			{
				var queuecount = _queue.Count;	// 值传递
				for (int i = 0; i < queuecount; i++)
				{
					var kv = _queue.Dequeue();
					if (!File.Exists(kv.Key))
					{
						_logger.Warn($"{kv.Key} 文件不存在, 无法删除.");
						continue;
					}
					try
					{
						action(kv.Key);
					}
					// 如果文件正在占用, 从新放入队列, 并计数
					catch (System.IO.IOException e)
					{
						int count = 100;
						if (kv.Value > count)	// 删除指定次数依然不行, 放弃了
						{
							_logger.Error($"{kv.Key} 文件删除了{count}次都失败了! 有进程一直占着不放. 已经从队列中排除, 放弃删除了. {e}");
						}
						else
						{
							// 如果文件有进程占用, 将文件重新放入队尾之前先看看队列中是否已经存在同样路径的文件 - 未开发, 因为影响效率且没有必要

							_logger.Warn($"{e} [已经尝试了 {kv.Value} 次!]");
							var newKv = new KeyValuePair<string, int>(kv.Key, kv.Value + 1);
							_queue.Enqueue(newKv);
						}
					}
					catch (Exception e)
					{
						_logger.Error(e);
					}
				}
				await Task.Delay(timeSpan).ConfigureAwait(false);
			}
		}

		/// <summary>
		/// 添加图片路径
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool Add(string path)
		{
			if (!File.Exists(path))
			{
				return false;
			}
			_queue.Enqueue(new KeyValuePair<string, int>(path, 0));
			return true;
		}
	}
}
