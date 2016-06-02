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
			string raw = @"		POST http://oa.zxxk.com/AsynAjax.ashx HTTP/1.1
		Host: oa.zxxk.com
		Connection: keep-alive
		Content-Length: 87
		Accept: */*
		Origin: http://oa.zxxk.com
		X-Requested-With: XMLHttpRequest
		User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
		Content-Type: application/x-www-form-urlencoded; charset=UTF-8
		Referer: http://oa.zxxk.com/
		Accept-Encoding: gzip, deflate
		Accept-Language: zh-CN,zh;q=0.8,en;q=0.6
		
		Type=LoginValidate&UserName=%E4%BD%95%E5%A3%AB%E9%9B%84&PassWord=he394899990&LimitDay=0

";
			var request = new HttpRequestMessage().CreateFromRaw(raw);
			var html = new HttpClient().SendAsync(request).Result.Content.ReadAsStringAsync().Result;
			Console.WriteLine(html);
		}
	}
}
