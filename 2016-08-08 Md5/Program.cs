using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_08_08_Md5
{
	class Program
	{
		static void Main(string[] args)
		{
			// 32位 小写
			Func<string, string> md5 = input => System.Security.Cryptography.MD5.Create()
				.ComputeHash(Encoding.UTF8.GetBytes(input))
				.Aggregate(new StringBuilder(), (sb, index) => sb.Append(index.ToString("x2")))
				.ToString();
			Console.WriteLine(md5("123456"));
		}
	}
}
