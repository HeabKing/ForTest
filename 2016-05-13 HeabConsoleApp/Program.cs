using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.MessageService;
using System.Xml;

namespace _2016_05_13_HeabConsoleApp
{
	/// <summary>
	/// HeabConsoleApp 是我的电脑上一个开机运行的程序, 可以负责进行爬虫启用, 电脑控制等功能
	/// </summary>
	public class Program
	{
		static void Main(string[] args)
		{
			// 登录
			using (var client = new HttpClient())
			{
				string requestRaw = @"POST http://oa.zxxk.com/AsynAjax.ashx HTTP/1.1
					Accept: */*
					Origin: //oa.zxxk.com
					X-Requested-With: XMLHttpRequest
					User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
					Referer: //oa.zxxk.com/
					Accept-Encoding: gzip, deflate
					Accept-Language: zh-CN,zh;q=0.8,en;q=0.6
					Content-Type: application/x-www-form-urlencoded; charset=utf-8
					Host: oa.zxxk.com
					Content-Length: 87
					Expect: 100-continue
					Connection: Keep-Alive

					Type=LoginValidate&UserName=%E4%BD%95%E5%A3%AB%E9%9B%84&PassWord=he394899990&LimitDay=0";

				IEnumerable<string> sessionId;
				client.SendAsync(new HttpRequestMessage().CreateFromRaw(requestRaw)).Result.Headers.TryGetValues("Set-Cookie", out sessionId);
				var sessionIdList = sessionId as IList<string> ?? sessionId.ToList();

				requestRaw = $@"GET http://oa.zxxk.com/Default.aspx HTTP/1.1
					Host: oa.zxxk.com
					Connection: keep-alive
					Pragma: no-cache
					Cache-Control: no-cache
					Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
					Upgrade-Insecure-Requests: 1
					User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
					Referer: http://oa.zxxk.com/Function/Index.html
					Accept-Encoding: gzip, deflate, sdch
					Accept-Language: zh-CN,zh;q=0.8,en;q=0.6
					Cookie: {sessionIdList.FirstOrDefault()}";

				var html = client.SendAsync(new HttpRequestMessage().CreateFromRaw(requestRaw)).Result.Content.ReadAsStringAsync().Result;

				// 获得待点击URL信息列表
				//IEnumerable<dynamic> listLi = (from Match m in Regex.Matches(html, "<li>.+?</li>")
				//							   where Regex.IsMatch(m.Value, "<img")
				//							   let title = Regex.Match(m.Value, "title=\"(?<title>.*?)\"")
				//							   let url = Regex.Match(m.Value, "href=(\"|')(?<url>.*?)(\"|')")
				//							   let date = Regex.Match(m.Value, @"\[(?<date>.*?)\]")
				//							   select new
				//							   {
				//								   Title = title.Success ? title.Result("${title}") : "",
				//								   Url = url.Success ? url.Result("${url}") : "",
				//								   Date = date.Success ? date.Result("${date}") : "",
				//							   }).ToList();

				// 获得待点击URL信息列表 - 升级
				var htmlDoc = new HtmlAgilityPack.HtmlDocument();
				htmlDoc.LoadHtml(html);
				var doc = htmlDoc.CreateNavigator() as HtmlAgilityPack.HtmlNodeNavigator;
				
				IEnumerable<dynamic> listLi = from m in htmlDoc.DocumentNode.SelectNodes("//li[img]")
											  select new
											  {
												  Url = m.SelectSingleNode("//@href"),  // TODO 选择的是href所在的元素而不是href属性本身
												  Title = m.SelectSingleNode("//@title"),
												  Date = m.SelectSingleNode("//span[text()]")
											  };

				var taskList = new List<Task<HttpResponseMessage>>();
				var listTemp = new Dictionary<int, int>();

				// 进行自动点击
				var request = new HttpRequestMessage();
				foreach (var item in listLi)
				{
					request.RequestUri = new Uri(new Uri("http://oa.zxxk.com"), item.Url);
					var r = client.SendAsync(request.Clone().Result);
					taskList.Add(r);
					listTemp[listLi.ToList().IndexOf(item)] = taskList.IndexOf(r);
				}
				Task.WhenAll(taskList);
				string emailStr = taskList.Where(m => !m.IsFaulted && m.Result.IsSuccessStatusCode).Aggregate("", (c, i) => c += listLi.ToList()[listTemp[taskList.IndexOf(i)]].Title + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Date + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Url + "    \r\n");
				emailStr += $" \r\n 总共 {taskList.Count(m => !m.IsFaulted && m.Result.IsSuccessStatusCode) * 5} 积分到手...";
				IMessageService emailService = new EmailServiceFromQq("heabking@qq.com", "pwuomsefcevacbeg");
				emailService.SendMessage(new EmailMessage
				{
					Msg = emailStr,
					Subject = DateTime.Now + " OA 积分获得情况"
				}, "394899990@qq.com");
			}
		}
	}
}
