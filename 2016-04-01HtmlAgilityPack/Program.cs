using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using HtmlAgilityPack;


namespace _2016_04_01HtmlAgilityPack
{
	class Program
	{

		public static void Main()
		{
			//<catalog>
			//	<cd country="USA">
			//		<title>Empire Burlesque</title>
			//		<artist>Bob Dylan</artist>
			//		<price>10.90</price>
			//	</cd>
			//    <cd country="UK">
			//		<title>James Bond</title>
			//		<artist>Hello World</artist>
			//		<price>123456</price>
			//	</cd>
			//</catalog>

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.Load("xml.xml");
			var node = doc.DocumentNode.SelectSingleNode("/catalog/cd/@country");   // TODO : 选择的是cd元素而不是country属性
			Console.WriteLine(node.GetType());
			Console.WriteLine(node);


			HtmlAgilityPack.HtmlNodeNavigator docx = new HtmlAgilityPack.HtmlNodeNavigator("xml.xml");
			var nodex = docx.SelectSingleNode("/catalog/cd/@country");	// TODO 不能使用SelectNodes
			Console.WriteLine(nodex.GetType());
			Console.WriteLine(nodex.Value);     // 这个解决了属性选择的问题

			
			var r = docx.Select("/catalog/cd/@country").Cast<HtmlNodeNavigator>();

			// http://stackoverflow.com/questions/26744559/htmlagilitypack-xpath-and-regex TODO 关于 SelectNodes 的解决方案

			var nodes = docx.SelectNodes("/catalog/cd/@country");

			//XmlDocument doc = new XmlDocument();
			//doc.Load("xml.xml");
			//var r = doc.DocumentElement.SelectSingleNode("//@country");	// !! XmlDocodument就支持
		}

		static void Main1(string[] args)
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
