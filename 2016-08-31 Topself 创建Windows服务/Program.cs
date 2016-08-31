using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using Common.MessageService;
using HtmlAgilityPack;
using Topshelf;

namespace _2016_08_31_Topself_创建Windows服务
{
	public class Program
	{
		public static void Main()
		{
#if DEBUG
			var spider = new OaSpider();
			spider.RunAndEmail();
#else
			RunService();
#endif
		}

		private static void RunService()
		{
			// 使用 HostFactory 配置一个托管
			HostFactory.Run(x => // 使用 x 暴漏所有配置信息
			{
				// 告诉 Topself 这里有一个 TownCrier 类型的服务
				x.Service<OaSpider>(s =>
				{
					// 如何创建服务实例
					s.ConstructUsing(name => new OaSpider());
					// 开始服务
					s.WhenStarted(tc => tc.Start());
					// 关闭服务
					s.WhenStopped(tc => tc.Stop());
				});
				x.RunAsLocalSystem();
				// 设置服务的描述
				x.SetDescription("OA爬虫, 自动获取积分并发送邮件");
				x.SetDisplayName("OaSpider");
				x.SetServiceName("OaSpider");

				x.SetStartTimeout(TimeSpan.FromSeconds(10));
				x.SetStopTimeout(TimeSpan.FromSeconds(10));

				x.EnableServiceRecovery(c =>
				{
					//c.RunProgram(1, "notepad.exe"); // run a program
					c.RestartService(1); //1分钟后尝试重启
					c.OnCrashOnly(); //服务崩溃时才会重启	
				});
			});
		}
	}

	public class OaSpider
	{
		private readonly Timer _timer;

		public OaSpider()
		{
			_timer = new Timer(TimeSpan.FromDays(1).TotalMilliseconds) { AutoReset = true };
			_timer.Elapsed += (sender, eventArgs) => RunAndEmail();
		}

		public void Start()
		{
			_timer.Start();
		}

		public void Stop()
		{
			_timer.Stop();
		}

		public void RunAndEmail()
		{
			string emailContent;
			try
			{
				emailContent = Run();
			}
			catch (Exception ex)
			{
				emailContent = ex.ToString();
			}
			IMessageService emailService = new EmailServiceFromQq("heabking@qq.com", "pwuomsefcevacbeg");
			emailService.SendMessage(new EmailMessage
			{
				Msg = emailContent,
				Subject = DateTime.Now + " OA 积分获得情况"
			}, "394899990@qq.com");
		}

		private static string Run()
		{
			// 登录
			using (var client = new HttpClient())
			{
				string requestRaw = $@"
					POST http://oa.zxxk.com/AsynAjax.ashx HTTP/1.1
					Host: oa.zxxk.com
					Connection: keep-alive
					Content-Length: 87
					Pragma: no-cache
					Cache-Control: no-cache
					Accept: */*
					Origin: http://oa.zxxk.com
					X-Requested-With: XMLHttpRequest
					User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
					Content-Type: application/x-www-form-urlencoded; charset=UTF-8
					Referer: http://oa.zxxk.com/Login.html
					Accept-Encoding: gzip, deflate
					Accept-Language: zh-CN,zh;q=0.8,en;q=0.6

					Type=LoginValidate&UserName=%E4%BD%95%E5%A3%AB%E9%9B%84&PassWord=he394899990&LimitDay=0";
				Console.WriteLine(requestRaw);
				IEnumerable<string> sessionId;
				var response = client.SendAsync(new HttpRequestMessage().CreateFromRaw(requestRaw)).Result;
				response.EnsureSuccessStatusCode();
				response.Headers.TryGetValues("Set-Cookie", out sessionId);

				if (sessionId == null)
				{
					throw new HttpRequestException("未返回 Set-Cookie, 登录失败");
				}

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
				response = client.SendAsync(new HttpRequestMessage().CreateFromRaw(requestRaw)).Result;
				response.EnsureSuccessStatusCode();
				var html = response.Content.ReadAsStringAsync().Result;

				// 获得待点击URL信息列表
				IEnumerable<dynamic> listLi = from m in (null as HtmlNodeNavigator)
						.Create(html)
						.SelectSetAsRoot("//li[img]")
					let title = m.SelectSingle("//@title").Value
					let url = m.SelectSingle("//@href").Value
					let date = m.SelectSingle("//span/text()").Value
					where !Regex.IsMatch(url, "knowledge", RegexOptions.IgnoreCase)
					select new
					{
						Title = title,
						Url = url,
						Date = date
					};
				listLi = listLi.ToList();
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
				var emailStr = taskList.Where(m => !m.IsFaulted && m.Result.IsSuccessStatusCode)
					.Aggregate("",
						(c, i) =>
							c +=
								listLi.ToList()[listTemp[taskList.IndexOf(i)]].Title + "   " +
								listLi.ToList()[listTemp[taskList.IndexOf(i)]].Date + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Url +
								"    <br/>");
				emailStr += $" <br/> 总共 {taskList.Count(m => !m.IsFaulted && m.Result.IsSuccessStatusCode)*5} 积分到手...";
				return emailStr;
			}
		}
	}
}