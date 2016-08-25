using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;

namespace _2016_08_23_DotNetConsole
{
	/// <summary>
	/// C#代码执行器，提供动态执行C#代码
	/// </summary>
	public class Evaluator
	{
		private static readonly CSharpCodeProvider CCodeProder;
		private static CompilerParameters _compPars;
		private static CompilerResults _compResult;
		private static readonly Regex RexLastCode = new Regex(@"(print:).+;\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase); //用于搜索最后一条代码的位置

		static Evaluator()
		{
			CCodeProder = new CSharpCodeProvider();
		}

		/// <summary>
		/// 执行指定的代码
		/// </summary>
		/// <param name="strCodes"></param>
		/// <param name="strErrText"></param>
		/// <returns></returns>
		public static string Eval(string strCodes, out string strErrText)
		{
			#region 编译代码
			strErrText = InitCompile(ref strCodes);
			if (strErrText != null) return null;
			try
			{
				_compResult = CCodeProder.CompileAssemblyFromSource(_compPars, new string[] { strCodes });
			}
			catch (NotImplementedException nie)
			{
				strErrText = nie.Message;
				return null;
			}

			if (_compResult.Errors.HasErrors)
			{
				var sbErrs = new StringBuilder(strCodes + System.Environment.NewLine);
				sbErrs.Append("您所提供的C#代码中存在语法错误！" + System.Environment.NewLine);
				foreach (CompilerError err in _compResult.Errors)
				{
					sbErrs.AppendFormat("{0}，{1}" + System.Environment.NewLine, err.ErrorNumber, err.ErrorText);
				}
				strErrText = sbErrs.ToString();
				return null;
			}
			#endregion

			var assembly = _compResult.CompiledAssembly;
			object prgInsl = assembly.CreateInstance("System._ClassEvaluatorCompiler");
			MethodInfo medInfo = prgInsl.GetType().GetMethod("PrintResult");
			try
			{
				var strRetn = medInfo.Invoke(prgInsl, null).ToString();
				return strRetn;
			}
			catch (Exception exMsg)
			{
				strErrText = exMsg.Message;
				return null;
			}
		}

		/// <summary>
		/// 预编译代码
		/// </summary>
		/// <param name="strCodes">待编译的源代码</param>
		/// <returns>如果错误则返回错误消息</returns>
		private static string InitCompile(ref string strCodes)
		{
			List<string> lstRefs = new List<string>();//代码字符串中的include引用程序集------未使用

			List<string> lstUsings = new List<string>();//代码字符串中的using引用命名空间


			#region 分离引用的程序集与命名空间
			int point = 0;
			char[] cCodes = strCodes.ToCharArray();
			for (int i = 0; i < cCodes.Length; ++i)
			{
				if (cCodes[i] == '\n' || (cCodes[i] == '\r' && cCodes[i + 1] == '\n'))
				{
					var strTemp = strCodes.Substring(point, i - point);
					if (strTemp.TrimStart(new char[] { ' ' }).StartsWith("using "))
					{
						strTemp = strTemp.Substring(6).Trim();
						if (!lstUsings.Contains(strTemp))
						{
							lstUsings.Add(strTemp);
						}
						else
						{
							return "预编译失败，代码中不允许包含重复命名空间导入。" + System.Environment.NewLine + "using " + strTemp;
						}
						point = cCodes[i] == '\n' ? i + 1 : i + 2;
						++i;
					}
					else if (strTemp.TrimStart(new char[] { ' ' }).StartsWith("include "))
					{
						strTemp = strTemp.Substring(8).Trim().ToLower();
						if (!lstRefs.Contains(strTemp))
						{
							lstUsings.Add(strTemp);
						}
						else
						{
							return "预编译失败，代码中不允许包含重复的程序集引用。" + System.Environment.NewLine + "include " + strTemp;
						}
						point = cCodes[i] == '\n' ? i + 1 : i + 2;
						++i;
					}
					else
					{
						break;
					}
				}
			}
			strCodes = strCodes.Substring(point);
			#endregion

			#region 初始化编译参数
			if (_compPars == null)
			{
				_compPars = new CompilerParameters
				{
					GenerateExecutable = false,
					GenerateInMemory = true
				};
			}
			//string workDir = System.Web.HttpContext.Current.Server.MapPath("~") + "Bin\\";
			//string workDir = System.Environment.CurrentDirectory;
			_compPars.ReferencedAssemblies.Clear();
			_compPars.ReferencedAssemblies.Add("system.dll");
			_compPars.ReferencedAssemblies.Add("system.data.dll");
			_compPars.ReferencedAssemblies.Add("system.xml.dll");

			//compPars.ReferencedAssemblies.Add(workDir + "BLL.dll");
			//compPars.ReferencedAssemblies.Add(workDir + "Component.dll");
			//compPars.ReferencedAssemblies.Add(workDir + "Model.dll");
			//compPars.ReferencedAssemblies.Add(workDir + "Utility.dll");

			foreach (string str in lstRefs)
			{
				_compPars.ReferencedAssemblies.Add(str);
			}
			#endregion

			StringBuilder sbRetn = new StringBuilder();

			#region 生成代码模板
			//*为代码添加return 语句*/
			Match match = RexLastCode.Match(strCodes);
			if (match.Success)
			{
				strCodes =
					$"{strCodes.Substring(0, match.Groups[1].Index)}\r\nreturn {strCodes.Substring(match.Groups[1].Index + match.Groups[1].Length)}";
			}
			else
			{
				strCodes = "return " + strCodes.Trim();//把要运行的代码字符串作为返回值---作为输出 - 显示运行的字符串源码
			}

			/*拼接代码*/
			foreach (string str in lstUsings)
			{
				sbRetn.AppendLine("using " + str);
			}
			sbRetn.AppendLine("using System.Text.RegularExpressions;");
			sbRetn.AppendLine("namespace System{");
			sbRetn.AppendLine("public class _ClassEvaluatorCompiler{");
			sbRetn.AppendLine("public static object PrintResult(){");
			sbRetn.AppendLine(strCodes);
			sbRetn.AppendLine("}}}");
			#endregion
			strCodes = sbRetn.ToString();
			return null;
		}


	}


	/// <summary>
	/// 简单版 只支持一行表达式的执行
	/// </summary>
	public class ProgramTest
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				args = new[] {"1 + 1"};
			}

			Console.WriteLine($"表达式: {args.Aggregate("", (c, i) => c += i)}");
			string error = string.Empty;
			string res = string.Empty;
			string code = args.Aggregate("", (c, i) => c += i).Replace('\'', '\"') + ";";

			//c# EVAL 动态 执行 代码 .net 执行 字符串 代码
			res = Evaluator.Eval(code, out error);


			if (!string.IsNullOrEmpty(error))
			{
				Console.WriteLine(error);
			}
			else
			{
				Console.WriteLine("执行结果为：" + res);
			}
			Console.ReadKey();
		}
	}


}