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
		/// Extension SelectNodes For XPath Expression
		/// </summary>
		/// <remarks>Hesinx 2016-05-26</remarks>
		/// <param name="navigator"></param>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static IEnumerable<HtmlNodeNavigator> SelectNodes(this HtmlNodeNavigator navigator, string xpath)
		{
			return navigator.Select(xpath).Cast<HtmlNodeNavigator>();
			//return from HtmlNodeNavigator m in navigator.Select(xpath) select m;
		}
	}
}
