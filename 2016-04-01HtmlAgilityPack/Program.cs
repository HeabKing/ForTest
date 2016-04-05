using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace _2016_04_01HtmlAgilityPack
{
	class Program
	{
		static void Main(string[] args)
		{
			HttpClient client = new HttpClient();
			var htmlTask = client.GetStringAsync("http://www.baidu.com");
			var html = htmlTask.Result;

			html = $"<div id='justForGetText'>{html}</div>";

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(html);
			var result = doc.GetElementbyId("justForGetText").InnerText;
			//Console.WriteLine(result);

			string str = @"毕达哥拉斯，一个神奇的学派。他与他们的传说，贯穿于我们基础数论、几何的研究当中。现在就让我们随着教徒希帕索斯的脚步，走近这个学（hei）派（bang）……




/ a12.html>";
			str = Regex.Replace(str, @"\s+", " ");
			Console.WriteLine(str);
		}
	}
}
