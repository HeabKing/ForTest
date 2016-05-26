using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hesinx.UnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			string email1 = "heabking@163.com";
			string email2 = "39489990@vip.qq.com";
			string email3 = "fdas.32@qq.com";
			string email4 = "fda@qq";
			string email5 = "fadfasfsqq.com";

			string pattern = ((Regex) null).GetEmailPattern();
			Assert.IsTrue(Regex.IsMatch(email1, pattern));
			Assert.IsTrue(Regex.IsMatch(email2, pattern));
			Assert.IsTrue(Regex.IsMatch(email3, pattern));
			Assert.IsFalse(Regex.IsMatch(email4, pattern));
			Assert.IsFalse(Regex.IsMatch(email5, pattern));
		}
	}
}
