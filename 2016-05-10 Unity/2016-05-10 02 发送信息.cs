using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_05_10_Unity
{
	public interface IMessageService
	{
		void SendMessage(string to, string msg);
	}

	public class EmailService : IMessageService
	{
		public void SendMessage(string to, string msg)
		{
			Console.WriteLine($"通过EmailService发送邮件給{to}");
		}
	}

	public class SmsService : IMessageService
	{
		public void SendMessage(string to, string msg)
		{
			Console.WriteLine($"通过SmsService发送短信給{to}");
		}
	}

	public interface INotificationManager
	{
		void Notify(string to, string msg);
	}

	public class NotificationManager : INotificationManager
	{
		private readonly IMessageService _msgService = null;
		// 从构造函数注入讯息服务对象
		public NotificationManager(IMessageService svc)
		{
			_msgService = svc;
		}
		// 利用讯息服务来发送讯息給指定对象
		public void Notify(string to, string msg)
		{
			_msgService.SendMessage(to, msg);
		}
	}
}
