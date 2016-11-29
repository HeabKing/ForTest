using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sinx.Utility.Extension;
using Xunit;

namespace _2016_11_10_Zxxk
{
	/// <summary>
	/// 学科网公共Api测试
	/// </summary>
	/// <remarks>
	///		1. 
	/// </remarks>
	public class ZxxkApiCommonTest
	{
		/// <summary>
		/// 我的工作QQ登录的学科网 - UserID
		/// </summary>
		private static int UserId = 24620567;

		private static string XkPassport = "8FC593F3DA5D068E4D4268D6F222102EAB7B7E474CAEBDB5123C17530DAE00A119EED77A310BE5577A6176DC16E4150B54B04709D19B2540ED678AC7A7E5F61FFD266B8CEA8201ECB8AAD364CA33A897208FF360E18D30DCE65D65A69B7B19EDBFA37B478DC5226AE1C9BB3B7211FEAD28BEEF7F3FEE1270BCE84E4C958DFC049698A09E99103E634304E08394359C1E9695EE8E82213F063EA8908465118B5C1F6D2682E6D2D1DA0FBE39CCB2D95CC5AD4E4F00211A1334BC176B953020CE95AD0786E16982FEAD5914B8344A8919D447F8CB0BF4809EEFDE3127B52B785DDA176EAE29052FE55BBA0144A9EFF2AD0841D8E1AE302A601CC17B5DF5F130207B39733C4D34AE74E893FA26E6C11ABBB7DDBD49FB1C039F8709C5A45DA1E4D743CD38AF61AA4AEA5C3A5DB838E10EE7EB4AC8AC472E19D21194D494FCEDB61C38B0CF955E75F63A7FD532E36A794CF402AEA9F2A4554EA0C5F60963688A9F70C5265209495CEF5E4CBC87CE6B27D586908CFEE79F19D858C5512CF576717BDDB03EBFD156";
		private static string Aspoo = "UserID=24623600&UserPassword=4297f44b13955235245b2497399d7a93&UserGroupID=4&UserName=13671202774";
		private static string Ip = "36.110.49.98";
		private static HttpClient Client = new HttpClient();
		/// <summary>
		/// 2016-11-10 收藏测试
		/// </summary>
		/// <remarks>
		///		1. 没有使用登录相关的Cookie, 收藏成功
		/// </remarks>
		[Fact]
		public void CollectionTest()
		{
			using (var client = new HttpClient())
			{
				var raw = $@"
					GET http://114.215.236.123:8093/Favorite/AddFavorite?userId=24620567&title=[%E4%B8%AD%E5%AD%A6%E8%81%94%E7%9B%9F]%E5%B9%BF%E4%B8%9C%E7%9C%81%E5%8F%B0%E5%B1%B1%E5%B8%82%E5%8D%8E%E4%BE%A8%E4%B8%AD%E5%AD%A62015-2016%E5%AD%A6%E5%B9%B4%E9%AB%98%E4%B8%80%E4%B8%8A%E5%AD%A6%E6%9C%9F%E8%AF%AD%E6%96%87%E5%B0%8F%E6%B5%8B7%E8%AF%95%E9%A2%98&url=http://yw.zxxk.com/soft/5756942.html&remark=&isPublic=1&callback=jQuery1102003077012020114389_1478767581665&_=1478767581670 HTTP/1.1
					Host: 114.215.236.123:8093
					Connection: keep-alive
					Accept: */*
					User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36
					Referer: http://yw.zxxk.com/jc/class-694.html
					Accept-Encoding: gzip, deflate, sdch
					Accept-Language: zh-CN,zh;q=0.8,en;q=0.6,de;q=0.4,zh-TW;q=0.2";
				var request = HttpRequestMessageEx.CreateFromRaw(raw);
				var response = client.SendAsync(request).Result;
				var content = response.Content.ReadAsStringAsync().Result;
				Assert.True(content == "jQuery1102003077012020114389_1478767581665({\"msg\":\"True\"})");
			}
		}
		[Fact]
		public void Collection2Test()
		{
			var passportCookie = $"xk.passport={XkPassport}";
			var aspooCookie = $"Aspoo={Aspoo}";
			var raw = $@"
				POST http://user.zxxk.com/MainFUNC/MyFavorite.aspx?callback=jQuery1640457490921760064_1478856633524&action=api&Title=%u5927%u5174%u5927%u5174%u53CD%u53CD%u590D%u590D%u53CD%u53CD%u590D%u590D%u51E4%u98DE%u98DE&Url=http%3A//hx.zxxk.com/soft/5392332.html&Remark=&IsPublic=1&t=0.4649681864209534&_=1478856731199 HTTP/1.1
				Host: user.zxxk.com
				Connection: keep-alive
				Pragma: no-cache
				Cache-Control: no-cache
				User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36
				Accept: */*
				Referer: http://localhost:1386/search1.aspx?keyword=&channelid=10&SelectTypeID=3
				Accept-Encoding: gzip, deflate, sdch
				Accept-Language: zh-CN,zh;q=0.8
				Cookie: $cookie
				Content-Length: 0
				";
			var response = Client.SendAsync(HttpRequestMessageEx.CreateFromRaw(raw.Replace("$cookie", aspooCookie))).Result;
			var content = response.Content.ReadAsStringAsync().Result;
			// Aspoo
			Assert.True(content.Contains("Login.aspx"));
			var res = Client.SendAsync(HttpRequestMessageEx.CreateFromRaw(raw.Replace("$cookie", passportCookie))).Result;
			var stream = res.Content.ReadAsStreamAsync().Result;
			var buffer = new byte[stream.Length];
			stream.Read(buffer, 0, buffer.Length);
			//var con = Encoding.GetEncoding("gb2312").GetString(buffer, 0, buffer.Length);
			//// xk.passport
			//Assert.True(con.Contains("ok") || con.Contains("收藏"));
		}

		/// <summary>
		/// 2016-11-10 下载测试
		/// </summary>
		[Fact]
		public void DownloadTest()
		{
			using (var client = new HttpClient())
			{
				var raw = @"
GET http://baidu.com HTTP/1.1
Content-Type: application/x-www-form-urlencoded;charset=utf-8
";
				var request = HttpRequestMessageEx.CreateFromRaw(raw);
			}
		}

		/// <summary>
		/// 2016-11-10 根据用户xk.passport cookie获取用户Id
		/// </summary>
		[Fact]
		public void GetUserIdFromCookie()
		{
			using (var client = new HttpClient())
			{
				var raw = $@"
					GET http://user.gw.zxxk.com:8086/api/v1/user/useraccount/verifylogin?Passport=8FC593F3DA5D068E4D4268D6F222102EAB7B7E474CAEBDB5123C17530DAE00A119EED77A310BE5577A6176DC16E4150B54B04709D19B2540ED678AC7A7E5F61FFD266B8CEA8201ECB8AAD364CA33A897208FF360E18D30DCE65D65A69B7B19EDBFA37B478DC5226AE1C9BB3B7211FEAD28BEEF7F3FEE1270BCE84E4C958DFC049698A09E99103E634304E08394359C1E9695EE8E82213F063EA8908465118B5C1F6D2682E6D2D1DA0FBE39CCB2D95CC5AD4E4F00211A1334BC176B953020CE95AD0786E16982FEAD5914B8344A8919D447F8CB0BF4809EEFDE3127B52B785DDA176EAE29052FE55BBA0144A9EFF2AD0841D8E1AE302A601CC17B5DF5F130207B39733C4D34AE74E893FA26E6C11ABBB7DDBD49FB1C039F8709C5A45DA1E4D743CD38AF61AA4AEA5C3A5DB838E10EE7EB4AC8AC472E19D21194D494FCEDB61C38B0CF955E75F63A7FD532E36A794CF402AEA9F2A4554EA0C5F60963688A9F70C5265209495CEF5E4CBC87CE6B27D586908CFEE79F19D858C5512CF576717BDDB03EBFD156&UserAddress=36.110.49.98&method=api%2Fv1%2Fuser%2Fuseraccount%2Fverifylogin&v=2.0&app_key=1a05d061a954384f&format=json&partner_id=top-sdk-net-20150212&timestamp=2016-11-10%2017%3A21%3A09&sign=13FB2009408D5921AD3507AB78FDF7A7 HTTP/1.1
					User-Agent: Top4Net
					Content-Type: application/x-www-form-urlencoded;charset=utf-8
					Host: user.gw.zxxk.com:8086
					Connection: Keep-Alive
					";
				var request = HttpRequestMessageEx.CreateFromRaw(raw);
				var response = client.SendAsync(request).Result;
				var content = response.Content.ReadAsStringAsync().Result;
				Assert.True(Regex.IsMatch(content, "\"UserId\":24623600"));
			}
		}
		/// <summary>
		/// 根据用户Id获取用户信息
		/// </summary>
		[Fact]
		public void GetUserInfoByUserId()
		{
			var raw = $@"
				GET http://user.gw.zxxk.com:8086/api/v1/user/userasset/getuserassetbyuserid?UserID=24623600&method=api%2Fv1%2Fuser%2Fuserasset%2Fgetuserassetbyuserid&v=2.0&format=json&partner_id=top-sdk-net-20150212&timestamp=2016-11-11%2009%3A01%3A38&sign=6C0E128D9DF63B2A93F99CEAB6D825AC HTTP/1.1
				User-Agent: Top4Net
				Content-Type: application/x-www-form-urlencoded;charset=utf-8
				Host: user.gw.zxxk.com:8086
				Connection: Keep-Alive
				";
			var req = HttpRequestMessageEx.CreateFromRaw(raw);
			var res = Client.SendAsync(req).Result;
			var content = res.Content.ReadAsStringAsync().Result;
			Assert.True(Regex.IsMatch(content, @"""ErrMsg"":"""""));
			Assert.True(Regex.IsMatch(content, "\"Content\":\".+?\""));
		}
	}

	//public static class HttpRequestMessageEx
	//{
	//	public static HttpRequestMessage CreateFromRaw(this HttpRequestMessage request, string reqRaw)
	//	{
	//		List<string> list = (List<string>)Enumerable.ToList<string>((IEnumerable<string>)((IEnumerable<string>)Regex.Split(reqRaw.Trim(), Environment.NewLine)).Select<string, string>((Func<string, string>)(m => m.Trim())));
	//		string[] strArray1 = Regex.Split(list.FirstOrDefault<string>() ?? "", "\\s");
	//		if (((IEnumerable<string>)strArray1).Count<string>() != 3 || !RegexEx.IsUrl(strArray1[1]) || !Regex.IsMatch(strArray1[2].ToLower(), "http/\\d+\\.\\d+"))
	//			throw new ArgumentException("请求行解析出错");
	//		string method = strArray1[0].Trim();
	//		string uriString = strArray1[1].Trim();
	//		strArray1[2].Trim();
	//		request.Method = new HttpMethod(method);
	//		request.RequestUri = new Uri(uriString);
	//		list.Remove(list.First<string>());
	//		if (method.ToLower() != "get")
	//		{
	//			string str = list.FirstOrDefault<string>(new Func<string, bool>(string.IsNullOrWhiteSpace));
	//			int index1 = list.IndexOf(str);
	//			string content = "";
	//			if (str != null)
	//			{
	//				for (int index2 = index1; index2 < list.Count; ++index2)
	//					content += list[index2];
	//				list.RemoveRange(index1, list.Count - index1);
	//			}
	//			request.Content = (HttpContent)new StringContent(content);
	//			list = (List<string>)Enumerable.ToList<string>((IEnumerable<string>)list.Where<string>((Func<string, bool>)(m => !Regex.IsMatch(m, "Content-Length", RegexOptions.IgnoreCase))));
	//		}
	//		foreach (string str1 in list)
	//		{
	//			string[] strArray2 = str1.Split(":".ToCharArray(), 2);
	//			string str2 = ((IEnumerable<string>)strArray2).FirstOrDefault<string>();
	//			string name = str2 != null ? str2.Trim() : (string)null;
	//			string str3 = ((IEnumerable<string>)strArray2).LastOrDefault<string>();
	//			string str4 = str3 != null ? str3.Trim() : (string)null;
	//			if (name == null)
	//				throw new ArgumentException("请求头解析出错");
	//			bool flag1 = false;
	//			if (name == "Content-Type")
	//			{
	//				request.Content.Headers.ContentType = new MediaTypeHeaderValue(str4?.Split(';')[0].Trim()) {CharSet = str4?.Split(';')[1].Trim().Split('=')[1]};
	//				continue;
	//			}
	//			try
	//			{
	//				request.Headers.Remove(name);
	//				flag1 = true;
	//			}
	//			catch (Exception ex)
	//			{
	//			}
	//			if (!flag1 || !request.Headers.TryAddWithoutValidation(name, str4))
	//			{
	//				bool flag2 = false;
	//				try
	//				{
	//					request.Content.Headers.Remove(name);
	//					flag2 = true;
	//				}
	//				catch (Exception ex)
	//				{
	//				}
	//				if (!flag2 || !request.Content.Headers.TryAddWithoutValidation(name, str4))
	//					throw new ArgumentException("key: " + name);	// TODO
	//			}
	//		}
	//		return request;
	//	}

			//	public static HttpRequestMessage CreateFromRaw(string reqRaw)
			//	{
			//		return new HttpRequestMessage().CreateFromRaw(reqRaw);
			//	}

			//	public static async Task<HttpRequestMessage> CloneAsync(this HttpRequestMessage req)
			//	{
			//		HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);
			//		MemoryStream ms = new MemoryStream();
			//		if (req.Content != null)
			//		{
			//			await req.Content.CopyToAsync((Stream)ms).ConfigureAwait(false);
			//			ms.Position = 0L;
			//			clone.Content = (HttpContent)new StreamContent((Stream)ms);
			//			if (req.Content.Headers != null)
			//			{
			//				foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders)req.Content.Headers)
			//					clone.Content.Headers.Add(header.Key, header.Value);
			//			}
			//		}
			//		clone.Version = req.Version;
			//		foreach (KeyValuePair<string, object> property in (IEnumerable<KeyValuePair<string, object>>)req.Properties)
			//			clone.Properties.Add(property);
			//		foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders)req.Headers)
			//			clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
			//		return clone;
			//	}
			//}
		}

