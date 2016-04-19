using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_04_08_异步Task学习
{
	public class _2016_04_19_不可变集合
	{
		public static void Main()
		{
			// 不可变集合每次都会返回一个新的集合
			var stack1 = ImmutableStack<int>.Empty;
			var stack2 = stack1.Push(1);	// 这里的stack1依旧为Empty
			var stack3 = stack2.Push(2);	// 这里的stack2依旧为{1}, 而stack3为{1, 2}, 但是他们共享1的内存
			foreach (var item in stack2)	
			{
				Debug.WriteLine(item);
			}
			Debug.WriteLine("--------------------");
			foreach (var item in stack3)
			{
				Debug.WriteLine(item);
			}
		}
	}
}
