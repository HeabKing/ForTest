using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_03_15DIStudy.Console
{
	/// <summary>
	/// 用户注册 - 非DI版本
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{

		}

		public void Login(string userId, string password)
		{
			var authService = new AuthenticationService();
			if (authService.TwoFactorLogin(userId, password))
			{
				if (authService.VerifyToken(" 使用者输入的验证码"))
				{
					// 登录成功。
				}
			}
			// 登录失败。
		}
	}

	class AuthenticationService
	{
		private readonly EmailService _msgService;
		public AuthenticationService()
		{
			_msgService = new EmailService(); // 建立用来发送验证码的对象
		}
		public bool TwoFactorLogin(string userId, string pwd)
		{
			// 检查账号密码，若正确，则返回一个包含用户信息的对象。
			User user = CheckPassword(userId, pwd);
			if (user != null)
			{
				// 接着发送验证码给使用者，假设随机产生的验证码为 "123456"。
				this._msgService.Send(user, " 您的登录验证码为 123456");
				return true;
			}
			return false;
		}

		private User CheckPassword(string userId, string pwd)
		{
			throw new NotImplementedException();
		}

		public bool VerifyToken(string 使用者输入的验证码)
		{
			throw new NotImplementedException();
		}
	}

	public class EmailService
	{
		public void Send(User user, string 您的登录验证码为)
		{
			throw new NotImplementedException();
		}
	}

	public class User
	{
	}


}
