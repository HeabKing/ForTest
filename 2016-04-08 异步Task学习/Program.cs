using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_04_08_异步Task学习
{
	class Program
	{
		static void Main(string[] args)
		{
			// 异步死锁 - 这里不会导致死锁, 因为当前上下文可以运行多个线程
			DeadBlock();


			var t = WhenAll();
			Task.WhenAll(t);
		}

		#region 异步死锁
		/// <summary>
		/// 只有在UI线程和ASP.NET线程(同时只能运行一个线程)才会导致死锁
		/// </summary>
		public static void DeadBlock()
		{
			Func<Task> taskfun = async () =>
			{
				await Task.Delay(10000);
				Debug.WriteLine(12);
			};
			var t = taskfun();
			t.Wait();
		}

		#endregion

		#region 2016-04-08 Task.WhenAll()
		public static async Task WhenAll()
		{
			int[] nums = {1, 2, 3, 4};
			IEnumerable<Task<int>> tasks = nums.Select(Task.FromResult);
			Task<int[]> task = Task.WhenAll(tasks); // 当所有任务完成的时候, task立即完成
			int[] result = await task;

			result.ToList().ForEach(i => Debug.WriteLine(i));
		}
		// 如果有一个任务
		#endregion
	}
}
