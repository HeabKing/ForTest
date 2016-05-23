using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageService
{
	/// <summary>
	/// 消息类
	/// </summary>
	public class EmailMessage : IMessage
	{
		/// <summary>
		/// 主题
		/// </summary>
		public string Subject { get; set; }
		/// <summary>
		/// 消息内容
		/// </summary>
		public string Msg { get; set; }
	}

	/// <summary>
	/// 使用邮件服务发送消息
	/// </summary>
	/// <remarks>何士雄 2016-05-13</remarks>
	public class EmailService : IMessageService
	{
		/// <summary>
		/// 发送邮件的客户端
		/// </summary>
		private readonly SmtpClient _smtpClient = new SmtpClient();
		/// <summary>
		/// 代表一个邮件
		/// </summary>
		private readonly MailMessage _mailMessage = new MailMessage();

		/// <summary>
		/// 对客户端进行初始化
		/// </summary>
		/// <param name="userName">用户名, QQ邮箱不用加@qq.com</param>
		/// <param name="pwd">密码: 使用授权码</param>
		/// <param name="from">发件人邮箱</param>
		/// <param name="host">发件人邮箱服务器地址</param>
		/// <param name="emableSsl"></param>
		/// <param name="port">发件人邮箱服务器端口号</param>
		public EmailService(string userName, string pwd, string from, string host, bool emableSsl, int port)
		{
			// 证书 - 用户名 + 密码
			_smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);
			// 邮件服务器地址
			_smtpClient.Host = host;
			// 是否使用SSL加密
			_smtpClient.EnableSsl = emableSsl;
			// 使用的端口号
			_smtpClient.Port = port;

			// 发件人邮箱
			_mailMessage.From = new MailAddress(from);
			// 主题内容使用的编码
			_mailMessage.SubjectEncoding = Encoding.UTF8;
			// 正文使用的编码
			_mailMessage.BodyEncoding = Encoding.Default;
			// 优先级
			_mailMessage.Priority = MailPriority.High;
			// 邮件正文是否为Html格式
			_mailMessage.IsBodyHtml = true;
			// 附件
			//_mailMessage.Attachments.Add(new Attachment("fullpath"));
		}

		public bool SendMessage(IMessage msg, params string[] to)
		{
			//向收件人地址集合添加邮件地址
			foreach (var m in to)
			{
				_mailMessage.To.Add(m);
			}
			EmailMessage emailMessage = msg as EmailMessage;
			// 添加主题
			_mailMessage.Subject = emailMessage?.Subject;
			// 添加正文
			_mailMessage.Body = emailMessage?.Msg ?? "";

			try
			{
				//将邮件发送到SMTP邮件服务器
				_smtpClient.Send(_mailMessage);
				Debug.WriteLine("发送邮件成功");
				return true;
			}
			catch (System.Net.Mail.SmtpException ex)
			{
				Debug.WriteLine(ex);
				return false;
			}

		}
	}

	public class EmailServiceFromQq : EmailService
	{
		public EmailServiceFromQq(string userName, string pwd) : base(userName.Split('@')[0], pwd, userName, "smtp.qq.com", true, 587)
		{

		}
	}

	/// <summary>
	/// 网易邮箱
	/// </summary>
	public class EmailServiceFrom163 : EmailService
	{
		public EmailServiceFrom163(string userName, string pwd) : base(userName, pwd, userName, "smtp.163.com", false, 25){}
	}
}
