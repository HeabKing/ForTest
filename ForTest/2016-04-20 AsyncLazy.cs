using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTest
{
	[TestClass]
	public class _2016_04_20_AsyncLazy
	{
		[TestMethod]
		public void GetDateTime()
		{

		}

		public AsyncLazy<int> lazy = new AsyncLazy<int>(()=> Task.FromResult(DateTime.Now));
	}
}
