using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aspose;

namespace Aspose
{
	class Program
	{
		static void Main(string[] args)
		{

			//args = new[] { @"C:\Users\HeabK\Source\Repos\ForTest\Aspose\bin\Debug\插值.docx" };
			var target = args[0].ConvertToHtmls(null, OfficeAndPdfConvertToHtmlExtension.ConvertModel.SingleFile);
			//Process.Start("explorer.exe", target);
			//GetAllFiles(target).ForEach(m => Process.Start("chrome.EXE", m));
			//Console.WriteLine("任意键关闭");
			//Console.ReadKey();
		}
		private static List<string> GetAllFiles(string directory)
		{
			// 递归获取下一级目录中的所有文件 - 委托
			Func<string, List<string>> getAllSubFiles = dir =>
			{
				string[] directorys = System.IO.Directory.GetDirectories(directory);
				return directorys.Aggregate(new List<string>(), (current, t) =>
					current.Concat(GetAllFiles(t)).ToList());
			};

			// 获取当前目录所有本目录文件
			var list = System.IO.Directory.GetFiles(directory).ToList();
			// 递归获取下一级目录中的所有文件, 并跟上边的合并
			list = list.Concat(getAllSubFiles(directory)).ToList();

			return list.Where(m => System.IO.Path.GetExtension(m) == ".html").ToList();
		}
	}
}
