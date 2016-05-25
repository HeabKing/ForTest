using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.Text.RegularExpressions
{
	/// <summary>
	/// 关于正则的拓展
	/// </summary>
	public static class HesinxExtension
	{
		/// <summary>
		/// Extension - Regex
		/// </summary>
		/// <param name="regex"></param>
		public static void Extension(this Regex regex){}

		/// <summary>
		/// 关于网址验证的正则 https://mathiasbynens.be/demo/url-regex
		/// </summary>
		/// <param name="regex"></param>
		/// <returns></returns>
		public static string GetUrlPattern(this Regex regex)
		{
			return @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
		}
	}
}
