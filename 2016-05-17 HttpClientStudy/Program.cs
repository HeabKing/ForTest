using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
}
