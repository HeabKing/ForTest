using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hesinx.UnitTest
{
	[TestClass]
	public class SimpleTest
	{
		[TestMethod]
		public void Test()
		{
			var tempaddress = Regex.Match("{%uploaddir%}/2016-6/8/zxxkcom2016060817032530520.doc", "{%.+?%}(?<address>.+?)$").Result("${address}");
			Assert.AreEqual("/2016-6/8/zxxkcom2016060817032530520.doc", tempaddress);
		}
	}
}
