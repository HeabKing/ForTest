using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_06_12_队列处理图片回收;

namespace _2016_06_14_预览类关系图_站点_清理
{
	/// <summary>
	/// 文件清理服务
	/// </summary>
	public interface IClearService
	{
		void ClearEnqueue(string path);
		Task StartClearAsync(TimeSpan timeSpan, int maxErrorCount, Predicate<string> predicate);
	}

	public class ClaerService:IClearService
	{
		public IProcessQueue Queue { get; set; }
		public void ClearEnqueue(string path)
		{
			throw new NotImplementedException();
		}

		public Task StartClearAsync(TimeSpan timeSpan, int maxErrorCount, Predicate<string> predicate)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// 预览服务
	/// </summary>
	public interface IProcessPreViewRequest
	{
		/// <summary>
		/// 获取文件
		/// </summary>
		/// <returns></returns>
		string GetFile();

		/// <summary>
		/// 获取模版
		/// </summary>
		/// <returns></returns>
		string GetTemplate();

		/// <summary>
		/// 统计
		/// </summary>
		void Statistics();

		/// <summary>
		/// 输出
		/// </summary>
		/// <returns></returns>
		string Output();
	}

	public class ProcessHtmlPreViewRequest: IProcessPreViewRequest
	{
		public string GetFile()
		{
			throw new NotImplementedException();
		}

		public string GetTemplate()
		{
			throw new NotImplementedException();
		}

		public void Statistics()
		{
			throw new NotImplementedException();
		}

		public string Output()
		{
			throw new NotImplementedException();
		}
	}

	public class ProcessJpgPreViewRequest : IProcessPreViewRequest
	{
		public string GetFile()
		{
			throw new NotImplementedException();
		}

		public string GetTemplate()
		{
			throw new NotImplementedException();
		}

		public void Statistics()
		{
			throw new NotImplementedException();
		}

		public string Output()
		{
			throw new NotImplementedException();
		}
	}
}
