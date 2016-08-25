using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace DotNetConsole
{
	public class DotNetConsole10
	{
		private const string CodeSnippet = @"
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class CodeClass
{
	public string PrintResult()
	{
		return (@return).ToString();
	}
}";

		public static void Main(string []args)
		{
			if (args.Length < 1)
			{
				args = new[] { "1 + 1" };
			}
			var code = CodeSnippet.Replace("@return", args[0]).Replace("'", "\"");
			Console.WriteLine("表达式: " + args[0]);
			Console.WriteLine("执行结果: " + Compiler(code));
		}

		/// <summary>
		/// 编译执行
		/// </summary>
		/// <returns>错误消息/执行结果</returns>
		public static string Compiler(string code)
		{
			var comParams = new CompilerParameters(new[] { "mscorlib.dll", "System.dll", "System.Core.dll" })
			{
				GenerateExecutable = false,
				GenerateInMemory = true
			};
			
			var comResult = new Microsoft.CSharp.CSharpCodeProvider().CompileAssemblyFromSource(comParams, code);    // TODO CODE
			if (comResult.Errors.HasErrors)
			{
				return comResult.Errors.Cast<object>().Aggregate(code, (c, i) => c += Environment.NewLine + i);
			}
			// 获取/设置 已经编译的程序集
			var assembly = comResult.CompiledAssembly;
			// 反射执行程序
			var obj = assembly.CreateInstance("CodeClass");
			var methodInfo = obj?.GetType().GetMethod("PrintResult");
			try
			{
				return methodInfo?.Invoke(obj, null).ToString().Trim();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
