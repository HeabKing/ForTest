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
		static void Main(string[] args)
		{
			var files = Directory.GetFiles(@"C:\Users\HeabKing\Desktop\单线程\");
			File.Open(files.LastOrDefault(), FileMode.Open);
			FileProcessQueue queue = new FileProcessQueue();
			var t = queue.StartAsync(new TimeSpan(0, 0, 2), 4, path => { File.Delete(path); return true; });
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

	/// <summary>
	/// 文件处理队列类
	/// </summary>
	/// <remarks>何士雄 2016-06-12</remarks>
	public class FileProcessQueue
	{
		/// <summary>
		/// 队列数据结构
		/// </summary>
		private readonly Queue<KeyValuePair<string, int>> _queue = new Queue<KeyValuePair<string, int>>();
		/// <summary>
		/// 日志组件
		/// </summary>
		private readonly ILog _logger;

		/// <summary>
		/// 初始化日志组件
		/// </summary>
		public FileProcessQueue()
		{
			log4net.Config.XmlConfigurator.Configure(); // 通过xml注册
			_logger = LogManager.GetLogger("testApp.Logging");
			_logger.Info("开始处理图片...");
		}

		/// <summary>
		/// 开始处理
		/// </summary>
		/// <param name="timeSpan">轮询处理的时间间隔</param>
		/// <param name="maxErrorCount">对一个文件进行的最大错误处理次数, 超过次数打上日志, 不再进行处理</param>
		/// <param name="predicate">对每个图片进行处理, 返回false或者抛出异常都会在指定次数内进行重试处理</param>
		/// <returns></returns>
		public async Task StartAsync(TimeSpan timeSpan, int maxErrorCount, Predicate<string> predicate)
		{
			// 添加到队尾的处理
			Action<KeyValuePair<string, int>, string, string> process = (kv, warnMsg, errorMsg) =>
			{
				if (kv.Value >= maxErrorCount)   // 删除指定次数依然不行, 放弃了
				{
					_logger.Error(errorMsg);
				}
				else
				{
					_logger.Warn(warnMsg);
					var newKv = new KeyValuePair<string, int>(kv.Key, kv.Value + 1);
					_queue.Enqueue(newKv);
				}
			};

			while (true)
			{
				var queuecount = _queue.Count;  // 值传递
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
						if (!predicate(kv.Key))
						{
							process(kv, $"文件处理已经尝试了{kv.Value + 1}次, 依旧失败!", $"文件处理已经失败了{kv.Value + 1}次, 已经将文件从队列中排除");
						}
					}
					// 如果文件正在占用, 从新放入队列, 并计数
					catch (Exception e/*IOException e*/)
					{
						process(kv, $"{e} [已经尝试了 {kv.Value + 1} 次!]", $"文件处理了{kv.Value}次都失败了! 有进程一直占着不放. 已经从队列中排除. {e}");
					}
					//catch (Exception e)
					//{
					//	_logger.Error(e);
					//}
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
				_logger.Warn($"尝试添加文件 {path} 失败, 文件不存在.");
				return false;
			}
			_queue.Enqueue(new KeyValuePair<string, int>(path, 0));
			return true;
		}
	}
}
