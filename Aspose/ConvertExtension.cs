using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aspose
{
	public static class OfficeAndPdfConvertToHtmlExtension
	{
		public enum ConvertModel
		{
			SingleDir,
			DirAndSubDir
		}

		/// <summary>
		/// 将Office格式, PDF格式转换成HTML格式
		/// </summary>
		/// <param name="pathSrc"></param>
		/// <param name="pathDst"></param>
		/// <returns></returns>
		public static string ConvertToHtml(this string pathSrc, string pathDst = null)
		{
			if (!System.IO.File.Exists(pathSrc))
			{
				throw new Exception($"指定文件 {pathSrc} 不存在!");
			}

			var extfilename = System.IO.Path.GetExtension(pathSrc);
			if (string.IsNullOrWhiteSpace(pathDst))
			{
				pathDst = System.IO.Path.GetDirectoryName(pathSrc) + @"\Formated\" + System.IO.Path.GetFileName(pathSrc) + ".html";
			}
			
			switch (extfilename)
			{
				case ".doc":
				case ".docx":
					var docSrc = new Words.Document(pathSrc);
					docSrc.Save(pathDst, Words.SaveFormat.Html);
					break;
				case ".xls":
				case ".xlsx":
					Cells.Workbook wb = new Cells.Workbook(pathSrc);
					wb.Save(pathDst, new Cells.HtmlSaveOptions(Cells.SaveFormat.Html));
					break;
				case ".ppt":
				case ".pptx":
					using (var pres = new Slides.Presentation(pathSrc))
					{
						var htmlOpt = new Slides.Export.HtmlOptions
						{
							HtmlFormatter = Slides.Export.HtmlFormatter.CreateDocumentFormatter("", false)
						};
						pres.Save(pathDst, Slides.Export.SaveFormat.Html, htmlOpt);
					}
					break;
				case ".pdf":
					var pdfSrc = new Pdf.Document(pathSrc);
					pdfSrc.Save(pathDst, Pdf.SaveFormat.Html);
					break;
				default:
					throw new Exception("不支持的格式");
			}
			var html = System.IO.File.ReadAllText(pathDst);
			html = Regex.Replace(html, "(Evaluation Only\\. Created with Aspose\\.(.+?)\\. Copyright \\d+-\\d+ Aspose Pty Ltd\\.)|(This document was truncated here because it was created in the Evaluation Mode\\.)", "");
			System.IO.File.WriteAllText(pathDst, html);
			return pathDst;
		}

		/// <summary>
		/// 将指定目录中的所有Office格式和PDF格式的文件转换为HTML格式
		/// </summary>
		/// <param name="dirSrc"></param>
		/// <param name="dirDst"></param>
		/// <param name="model">SingleDir : 转换指定目录的所有文件    DirAndSubDir : 转换指定目录以及子目录的所有文件</param>
		/// <returns></returns>
		public static string ConvertToHtmls(this string dirSrc, string dirDst = null, ConvertModel model = ConvertModel.SingleDir)
		{
			if (!System.IO.Directory.Exists(dirSrc))
			{
				throw new Exception("指定目录不存在");
			}
			var files = model == ConvertModel.SingleDir ? System.IO.Directory.GetFiles(dirSrc).ToList() : GetAllFiles(dirSrc);
			Parallel.ForEach(files, file =>
			{
				try
				{
					dirDst = file.ConvertToHtml();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(file + ex.Message);
				}
			});
			return model == ConvertModel.SingleDir ? System.IO.Path.GetDirectoryName(dirDst) : dirSrc;
		}

		/// <summary>
		/// 获取指定的目录以及子目录中的所有文件（不包括目录）
		/// </summary>
		/// <param name="directory">指定目录</param>
		/// <returns>文件列表</returns>
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
			return list;
		}
	}
}
