using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose;

namespace Aspose
{
	class Program
	{
		static void Main(string[] args)
		{
			var target = @"C:\Users\HeabKing\Desktop\soft".ConvertToHtmls(null, OfficeAndPdfConvertToHtmlExtension.ConvertModel.SingleDir);
			Process.Start("explorer.exe", target);
			GetAllFiles(target).ForEach(m => System.Diagnostics.Process.Start("chrome.EXE", m));
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
