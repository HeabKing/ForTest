using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace _2016_06_15_预览站点.Controllers
{
	/// <summary>
	/// 预览处理 - 针对单文件转换为多个jpg图片
	/// </summary>
	/// <remarks>何士雄 2016-06-15</remarks>
	public class PreViewController : ApiController
	{
		/// <summary>
		/// 用户跟OSS进行交互
		/// </summary>
		private readonly HttpClient _client = new HttpClient();

		/// <summary>
		/// 预览处理
		/// </summary>
		/// <param name="softData"></param>
		/// <returns></returns>
		public IHttpActionResult Get(SoftData softData)
		{
			PreViewResult preViewResult;

			// 从本地缓存获取
			if (!GetPreViewDataFromLocal(softData, out preViewResult))
			{
				// 从OSS上获取文件放到本地缓存
				if (GetPreViewDataFromOss(softData.SoftAddress))
				{
					// 从本地缓存获取
					if (!GetPreViewDataFromLocal(softData, out preViewResult))
					{
						// TODO 打日志 - 从oss获取文件成功, 但是本地找不到
					}
				}
				else
				{
					// todo 打日志 - 从oss获取文件失败
				}
			}
			return Json(preViewResult);
		}

		/// <summary>
		/// 从本地获取预览文件
		/// </summary>
		/// <param name="softData"></param>
		/// <param name="previewDataResult"></param>
		/// <returns></returns>
		private bool GetPreViewDataFromLocal(SoftData softData, out PreViewResult previewDataResult)
		{
			previewDataResult = new PreViewResult { PreViewAddresses = new List<string>() };
			var localDir = ConvertToLocalDir(softData);
			if (Directory.Exists(localDir))
			{
				var files = Directory.GetFiles(localDir);
				previewDataResult.PreViewAddresses = files; // TODO 将物理路径转换为虚拟路径
			}
			return previewDataResult.PreViewAddresses.Count > 0;
		}

		/// <summary>
		/// 从OSS获取文件
		/// </summary>
		/// <returns></returns>
		private bool GetPreViewDataFromOss(string softAddress)
		{
			var ossAddress = ConvertToOssAddress(softAddress);
			return HasPreViewData(ossAddress) && SaveOssPreViewData(ossAddress);
		}

		/// <summary>
		/// 判断Oss上是否存在转换后的文件
		/// </summary>
		/// <param name="ossAddress"></param>
		/// <returns></returns>
		private bool HasPreViewData(string ossAddress)
		{
			var str = _client.GetStringAsync(ossAddress);
			throw new NotImplementedException();
		}

		/// <summary>
		/// 根据地址从OSS拉取文件并保存到本地
		/// </summary>
		/// <param name="ossAddress"></param>
		/// <returns></returns>
		private bool SaveOssPreViewData(string ossAddress)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 将数据库地址转为OSS地址
		/// </summary>
		/// <param name="softAddress">资料的数据库地址</param>
		/// <returns></returns>
		private string ConvertToOssAddress(string softAddress)
		{
			var ossHost = ""; // todo
			throw new NotImplementedException();
		}

		/// <summary>
		/// 将数据库地址转换为本地地址
		/// </summary>
		/// <param name="softAddress">数据库地址</param>
		/// <remarks>何士雄 2016-06-15</remarks>
		/// <returns></returns>
		private string ConvertToLocalDir(SoftData softData)
		{
			var match = Regex.Match(softData.SoftAddress, "{%.+?%}(?<address>.+?)\\..+?$");
			return match.Success ? HttpContext.Current.Server.MapPath(match.Result("${address}") + "/") : "";
		}
	}

	/// <summary>
	/// 请求预览的资料的数据
	/// </summary>
	public class SoftData
	{
		public int SoftId { get; set; }
		public string SoftAddress { get; set; }
	}

	/// <summary>
	/// 用于转换成Json作为请求结果
	/// </summary>
	/// <remarks>何士雄 2016-06-15</remarks>
	public class PreViewResult
	{
		/// <summary>
		/// 预览地址
		/// </summary>
		public IList<string> PreViewAddresses { get; set; }
	}
}
