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
			var target = @"C:\Users\HeabKing\Desktop\ZXXKCOM2016032315494967528\".ConvertToHtmls(null, OfficeAndPdfConvertToHtmlExtension.ConvertModel.DirAndSubDir);
			Process.Start("explorer.exe", target);
		}
	}
}
