using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using HtmlAgilityPack;

// ReSharper disable once CheckNamespace
namespace HtmlAgilityPack
{
	/// <summary>
	/// For XPath Expression Support
	/// </summary>
	/// <remarks>Hesinx 2016-05-26</remarks>
	public static class HtmlNodeNavigatorExtension
	{
		/// <summary>
		/// 根据 html 字符串创建
		/// </summary>
		/// <remarks>Hesinx 2016-05-26</remarks>
		/// <param name="navigator">(null as HtmlNodeNavigator)</param>
		/// <param name="html"></param>
		/// <returns></returns>
		public static HtmlNodeNavigator Create(this HtmlNodeNavigator navigator, string html)
		{
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(html);
			return doc.CreateNavigator() as HtmlNodeNavigator;
		}

		/// <summary>
		/// 根据 xpath 选择单个 node, 非 root node
		/// </summary>
		/// <param name="navigator"></param>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static HtmlNodeNavigator SelectSingle(this HtmlNodeNavigator navigator, string xpath)
		{
			
			return navigator.ToElement().SelectSingleNode(xpath) as HtmlNodeNavigator;
		}

		/// <summary>
		/// 根据 xpath 选择 node 集合, node 非 root node
		/// </summary>
		/// <remarks>Hesinx 2016-05-26</remarks>
		/// <param name="navigator"></param>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static IEnumerable<HtmlNodeNavigator> SelectSet(this HtmlNodeNavigator navigator, string xpath)
		{
			return navigator.ToElement().Select(xpath).Cast<HtmlNodeNavigator>();
			//return from HtmlNodeNavigator m in navigator.Select(xpath) select m;
		}

		/// <summary>
		/// 根据 xpath 选择首个 node, node 被转化为 root node
		/// </summary>
		/// <remarks>Hesinx 2016-05-26</remarks>
		/// <param name="navigator"></param>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static HtmlNodeNavigator SelectSingleAsRoot(this HtmlNodeNavigator navigator, string xpath)
		{
			return navigator.SelectSingle(xpath).ToRoot();
		}

		/// <summary>
		/// 根据 xpath 选择 node, 并将当前 node 转化为 root node
		/// </summary>
		/// <remarks>Hesinx 2016-05-26</remarks>
		/// <param name="navigator"></param>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static IEnumerable<HtmlNodeNavigator> SelectSetAsRoot(this HtmlNodeNavigator navigator, string xpath)
		{
			return navigator.SelectSet(xpath).Select(m => m.ToRoot());
		}

		/// <summary>
		/// 将当前 HtmlNodeNavigator 转化为 Root HtmlNodeNavigator
		/// </summary>
		/// <param name="navigator"></param>
		/// <remarks>Hesinx 2016-05-26</remarks>
		/// <returns></returns>
		public static HtmlNodeNavigator ToRoot(this HtmlNodeNavigator navigator)
		{
			string docHtml = navigator.CurrentNode.OuterHtml;
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(docHtml);
			return doc.CreateNavigator() as HtmlNodeNavigator;
		}

		/// <summary>
		/// 将 Element / Attribute 转换为 Element
		/// </summary>
		/// <param name="navigator"></param>
		/// <returns></returns>
		public static HtmlNodeNavigator ToElement(this HtmlNodeNavigator navigator)
		{
			// CurrentNode.CreateNavigator()? 使用的原因是因为 如果 navigator 是 Attribute, 那么SelectSingleNode()将无法寻找 Element, 如果转换成 node, 在根据 node 转换为 Element, 再进行查找的话就可以了
			// Element -> Element, Attribute
			// Attribute -> Attribute
			return navigator.CurrentNode.CreateNavigator() as HtmlNodeNavigator;
		}
	}
}
