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
	public static class RegexExtension
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

		/// <summary>
		/// 邮箱正则 https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
		/// </summary>
		/// <param name="regex"></param>
		/// <returns></returns>
		public static string GetEmailPattern(this Regex regex)
		{
			return @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
		}
	}
}
