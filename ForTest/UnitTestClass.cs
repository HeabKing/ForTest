using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForTest
{
	[TestClass]
	public class UnitTestClass
	{
		[TestMethod]
		public static void TestStringEqual()
		{
			string str1 = "1 2";
			string str2 = "12";
			var b = Regex.IsMatch(str1, str2, RegexOptions.IgnorePatternWhitespace);
			Assert.IsTrue(b);
		}
	}
}
