﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Sinx.Utility.Extension;

namespace _2016_09_22_任务执行服务.SinxWorkItems
{
    /// <summary>
    /// OA 爬虫
    /// </summary>
    public static class OaSpider
    {
        public static void Begin(string username, string password, string emailaddress)
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

					Type=LoginValidate&UserName={username}&PassWord={password}&LimitDay=0";
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
                    var r = client.SendAsync(request.CloneAsync().Result);
                    taskList.Add(r);
                    listTemp[listLi.ToList().IndexOf(item)] = taskList.IndexOf(r);
                }
                Task.WhenAll(taskList);
                string emailStr = taskList.Where(m => !m.IsFaulted && m.Result.IsSuccessStatusCode).Aggregate("", (c, i) => c += listLi.ToList()[listTemp[taskList.IndexOf(i)]].Title + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Date + "   " + listLi.ToList()[listTemp[taskList.IndexOf(i)]].Url + "    <br/>");
                emailStr += $" <br/> 总共 {taskList.Count(m => !m.IsFaulted && m.Result.IsSuccessStatusCode) * 5} 积分到手...";
                // 发送邮件
                IMessageService emailService = SmtpClientEx.Create("heabking@qq.com", "pwuomsefcevacbeg");
                emailService.SendMessage(new EmailMessage(emailStr, DateTime.Now + " OA 积分获得情况"), emailaddress);
            }
        }
    }
}
