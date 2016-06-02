using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hesinx.UnitTest
{
	/// <summary>
	/// 测试在使用Cookie中的情况下能否重复发送Request
	/// </summary>
	[TestClass]
	public partial class Test
	{
		[TestMethod]
		public void TestHttpClient()
		{
			using (HttpClientHandler handler = new HttpClientHandler { CookieContainer = new System.Net.CookieContainer() })
			{
				handler.CookieContainer.Add(new Uri("https://www.baidu.com"), new System.Net.Cookie("user", "heab"));
				using (HttpClient client = new HttpClient(handler) { BaseAddress = new Uri("https://www.baidu.com") })
				{
					string html = client.GetStringAsync("").Result;
					Assert.IsTrue(html.Length > 100);
					html = client.GetStringAsync("").Result;
					Assert.IsTrue(html.Length > 100);
				}
			}
		}
	}
}
