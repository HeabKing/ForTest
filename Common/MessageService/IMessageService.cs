using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageService
{
	/// <summary>
	/// 消息类 - 设置成接口是防止误用Message实例导致转成EmailMessage等派生类的时候出错
	/// </summary>
	public interface IMessage
	{
		// 内容
		string Msg { get; set; }
	}

	/// <summary>
	/// 信息发送服务接口
	/// </summary>
	/// <remarks>何士雄 2016-05-13</remarks>
	public interface IMessageService
	{
		/// <summary>
		/// 发送信息的接口
		/// </summary>
		/// <remarks>何士雄 2016-05-13</remarks>
		/// <param name="to">向谁发送(一个/几个)</param>
		/// <param name="msg">发送的内容</param>
		/// <returns></returns>
		bool SendMessage(IMessage msg, params string[] to);
	}
}
