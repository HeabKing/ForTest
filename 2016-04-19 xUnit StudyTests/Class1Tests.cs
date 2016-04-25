using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2016_04_19_xUnit_Study;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_04_19_xUnit_Study.Tests
{
	[TestClass()]
	public class Class1Tests
	{
		[TestMethod()]
		public void PassingTestTest()
		{
			Assert.AreEqual(4, Add(2));
		}

		[TestMethod()]
		public void FailingTestTest()
		{
			Assert.AreEqual(5, Add(3));
		}

		int Add(int a)
		{
			return a + a;
		}
	}
}