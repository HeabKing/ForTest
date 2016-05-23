using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2016_05_17_HttpClientStudy
{
	class Program
	{
		// 1. 給请求加上Cookie
		static void Main(string[] args)
		{


			//Console.WriteLine(result.Result);

			//string url = "http://oa.zxxk.com";
			//HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/");
			////request.Headers.Add("Cookie", "UserInfo=UserName=Rt/3QlF1TvUY11TTWNeMVg==&PassWord=rbPKEaE4fxMi5yI1HA0ZSA==; Hm_lvt_0e522924b4bbb2ce3f663e505b2f1f9c=1463022044,1463362054; ASP.NET_SessionId=fwmbrkmlscvrdvcilprbw3v4");
			//var requestHandler = new HttpClientHandler() { UseCookies = false };
			//HttpClient client = new HttpClient(requestHandler) { BaseAddress = new Uri(url) };

			//var response = client.SendAsync(request).Result;
			//Console.WriteLine(response.Content.ReadAsStringAsync().Result);
		}
	}

	// ReSharper disable once InconsistentNaming
	public static class System_Net_Http_Extension
	{
		/// <summary>
		/// url验证的正则 https://mathiasbynens.be/demo/url-regex
		/// TODO : 归纳为System.Text.RegularExpressions;
		/// </summary>
		public static string RegexUrlPattern { get; } =
			@"_^(?:(?:https?|ftp)://)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\x{00a1}-\x{ffff}0-9]+-?)*[a-z\x{00a1}-\x{ffff}0-9]+)(?:\.(?:[a-z\x{00a1}-\x{ffff}0-9]+-?)*[a-z\x{00a1}-\x{ffff}0-9]+)*(?:\.(?:[a-z\x{00a1}-\x{ffff}]{2,})))(?::\d{2,5})?(?:/[^\s]*)?$_iuS";

		public static void RequestRawAsync(this HttpMessageInvoker invoker, string reqRaw)
		{
			// 解析reqRaw
			var splitLine = Regex.Split(reqRaw.Trim(), Environment.NewLine);
			// 1. 解析请求行
			var requestLine = Regex.Split(splitLine.FirstOrDefault() ?? "", "\\s");
			if (requestLine.Count() != 3 || 
				!Regex.IsMatch(requestLine[1], RegexUrlPattern)||
				!Regex.IsMatch(requestLine[2].ToLower(), @"http/\d+\.\d+"))
			{
				throw new ArgumentException("请求行解析出错");
			}
			var httpMethod = requestLine[0];
			var httpUrl = requestLine[1];
			var httpVersion = requestLine[2];
			// 2. 解析请求

			HttpRequestMessage req = new HttpRequestMessage();

		}
	}
}
