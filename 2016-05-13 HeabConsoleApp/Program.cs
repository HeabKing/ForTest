using System;
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
			string sessionId;
			StringContent stringContent = new StringContent("Type=LoginValidate&UserName=%E4%BD%95%E5%A3%AB%E9%9B%84&PassWord=he394899990&LimitDay=0");
			using (HttpClientHandler handler = new HttpClientHandler { UseCookies = false })
			using (HttpClient client = new HttpClient(handler))
			{
				stringContent.Headers.Remove("Content-Type");
				stringContent.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
				var response = client.PostAsync("http://oa.zxxk.com/AsynAjax.ashx", stringContent).Result;
				sessionId = response.Headers.GetValues("Set-Cookie").FirstOrDefault();
			}
			// 爬取
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/Default.aspx");
			request.Headers.Add("Cookie", sessionId);
			using (var handler = new HttpClientHandler { UseCookies = false })
			using (var client = new HttpClient(handler) { BaseAddress = new Uri("http://oa.zxxk.com") })
			{
				// 获得原始HTML
				var response = client.SendAsync(request).Result;
				response.EnsureSuccessStatusCode();
				var html = response.Content.ReadAsStringAsync().Result;
				// 获得待点击URL信息列表
				IEnumerable<dynamic> listLi = (from Match m in Regex.Matches(html, "<li>.+?</li>")
											  where Regex.IsMatch(m.Value, "<img")
											  let title = Regex.Match(m.Value, "title=\"(?<title>.*?)\"")
											  let url = Regex.Match(m.Value, "href=(\"|')(?<url>.*?)(\"|')")
											  let date = Regex.Match(m.Value, @"\[(?<date>.*?)\]")
											  select new
											  {
												  Title = title.Success ? title.Result("${title}") : "",
												  Url = url.Success ? url.Result("${url}") : "",
												  Date = date.Success ? date.Result("${date}") : "",
											  }).ToList();

				var taskList = new List<Task<HttpResponseMessage>>();
				var listTemp = new Dictionary<int, int>();
				// 进行自动点击
				foreach (var item in listLi)
				{
					request.RequestUri = new Uri(client.BaseAddress, item.Url);
					var r = client.SendAsync(CloneHttpRequestMessageAsync(request).Result);
					taskList.Add(r);
					listTemp[listLi.ToList().IndexOf(item)] = taskList.IndexOf(r);
				}
				Task.WhenAll(taskList);
				string emailStr = taskList.Where(m => !m.IsFaulted && m.Result.IsSuccessStatusCode).Aggregate("", (c, i) => c += listLi.ToList()[listTemp[taskList.IndexOf(i)]].Title + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Date + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Url + "    \r\n");
				emailStr += $" \r\n 总共 {taskList.Count(m => !m.IsFaulted && m.Result.IsSuccessStatusCode)*5} 积分到手...";
				IMessageService emailService = new EmailServiceFromQq("heabking@qq.com", "pwuomsefcevacbeg");
				emailService.SendMessage(new EmailMessage
				{
					 Msg = emailStr,
					  Subject = DateTime.Now + " OA 积分获得情况"
				}, "394899990@qq.com");
			}
		}
		/// <summary>
		/// http://stackoverflow.com/questions/25044166/how-to-clone-a-httprequestmessage-when-the-original-request-has-content?noredirect=1#comment38953745_25044166
		/// http://stackoverflow.com/questions/18000583/re-send-httprequestmessage-exception/18014515#18014515
		/// http://stackoverflow.com/questions/25047311/the-request-message-was-already-sent-cannot-send-the-same-request-message-multi
		/// http://stackoverflow.com/questions/25047311/the-request-message-was-already-sent-cannot-send-the-same-request-message-multi
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage req)
		{
			HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

			// Copy the request's content (via a MemoryStream) into the cloned object
			var ms = new MemoryStream();
			if (req.Content != null)
			{
				await req.Content.CopyToAsync(ms).ConfigureAwait(false);
				ms.Position = 0;
				clone.Content = new StreamContent(ms);

				// Copy the content headers
				if (req.Content.Headers != null)
					foreach (var h in req.Content.Headers)
						clone.Content.Headers.Add(h.Key, h.Value);
			}


			clone.Version = req.Version;

			foreach (KeyValuePair<string, object> prop in req.Properties)
				clone.Properties.Add(prop);

			foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
				clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

			return clone;
		}
	}
}
