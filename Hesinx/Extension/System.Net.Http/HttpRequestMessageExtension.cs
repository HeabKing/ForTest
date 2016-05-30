using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace System.Net.Http
{
	/// <summary>
	/// System.net.Http的拓展方法
	/// </summary>
	public static class HttpRequestMessageExtension
	{
		/// <summary>
		/// Extension - HttpRequestMessage
		/// </summary>
		/// <param name="request"></param>
		public static void Extension(this HttpRequestMessage request)
		{
		}

		/// <summary>
		/// 根据原始的Http请求字符生成HttpRequestMessage
		/// </summary>
		/// <param name="request">用于表示拓展方法的this</param>
		/// <param name="reqRaw">原始Http请求字符</param>
		/// <returns></returns>
		public static HttpRequestMessage CreateFromRaw(this HttpRequestMessage request, string reqRaw)
		{
			// 解析reqRaw
			var splitLine = Regex.Split(reqRaw.Trim(), Environment.NewLine).Select(m => m.Trim()).ToList();
			#region 1. 解析请求行
			// 1. 解析请求行
			var requestLine = Regex.Split(splitLine.FirstOrDefault() ?? "", "\\s");
			if (requestLine.Count() != 3 ||
				!Regex.IsMatch(requestLine[1], ((Regex)null).GetUrlPattern()) ||
				!Regex.IsMatch(requestLine[2].ToLower(), @"http/\d+\.\d+"))
			{
				throw new ArgumentException("请求行解析出错");
			}
			var httpMethod = requestLine[0].Trim();
			var httpUrl = requestLine[1].Trim();
			var httpVersion = requestLine[2].Trim();    // 这里先使用默认的, 不用reqRaw中的
			request.Method = new HttpMethod(httpMethod);
			request.RequestUri = new Uri(httpUrl);
			splitLine.Remove(splitLine.First());
			#endregion
			#region 2. 解析请求体
			// 2. 解析请求体
			if (httpMethod.ToLower() != "get")
			{
				var contentFlag = splitLine.FirstOrDefault(string.IsNullOrWhiteSpace);
				var indexFlag = splitLine.IndexOf(contentFlag);
				string content = "";
				if (contentFlag != null)
				{
					for (int i = indexFlag; i < splitLine.Count; i++)
					{
						content += splitLine[i];
					}
					splitLine.RemoveRange(indexFlag, splitLine.Count - indexFlag);
				}
				request.Content = new StringContent(content);
				// 我截取的Content内容可能不跟reqRaw中的完全长度一样, 所以还是依赖框架自己计算Content-Length吧
				splitLine = splitLine.Where(m => !Regex.IsMatch(m, "Content-Length", RegexOptions.IgnoreCase)).ToList();
			} 
			#endregion
			// 3. 解析请求头
			foreach (string keyvalue in splitLine)
			{
				var keyValue = keyvalue.Split(":".ToCharArray(), 2);
				var key = keyValue.FirstOrDefault()?.Trim();
				var value = keyValue.LastOrDefault()?.Trim();
				if (key == null)
				{
					throw new ArgumentException("请求头解析出错");
				}
				// 先尝试添加到HttpRequestHeaders中, 如果不行尝试添加到HttpContentHeaders中
				bool removeOk = false;
				try
				{
					request.Headers.Remove(key);
					removeOk = true;
				}
				catch (Exception)
				{
					// ignored
				}
				// 如果可以添加到HttpRequestHeaders中
				if (removeOk && request.Headers.TryAddWithoutValidation(key, value)) continue;
				// 尝试添加到HttpContentHeaders中
				removeOk = false;
				try
				{
					request.Content.Headers.Remove(key);
					removeOk = true;
				}
				catch (Exception)
				{
					// ignored
				}
				if (!removeOk || !request.Content.Headers.TryAddWithoutValidation(key, value))
				{
					throw new ArgumentException(nameof(key));
				}
			}
			return request;
		}

		/// <summary>
		/// http://stackoverflow.com/questions/25044166/how-to-clone-a-httprequestmessage-when-the-original-request-has-content?noredirect=1#comment38953745_25044166
		/// http://stackoverflow.com/questions/18000583/re-send-httprequestmessage-exception/18014515#18014515
		/// http://stackoverflow.com/questions/25047311/the-request-message-was-already-sent-cannot-send-the-same-request-message-multi
		/// http://stackoverflow.com/questions/25047311/the-request-message-was-already-sent-cannot-send-the-same-request-message-multi
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public static async Task<HttpRequestMessage> Clone(this HttpRequestMessage req)
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
