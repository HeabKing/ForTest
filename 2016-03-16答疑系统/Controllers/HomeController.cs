using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Dapper;
using _2016_03_16答疑系统.ViewModel;

namespace _2016_03_16答疑系统.Controllers
{
	public class HomeController : Controller
	{
		private readonly DbConnection _conn;
		public HomeController(DbConnection conn)
		{
			_conn = conn;
		}
		public ActionResult Index()
		{
			return View();
		}
		
		[HttpGet]
		public ActionResult Online(int? id, string to = "")
		{
			if (id == null || id == 0)
			{
				return View(new OnlineViewModel { Content = "", To = to, Id = 0 });
			}

			// 从指定id中获取数据, 展示出来
			var result = _conn.Query<OnlineViewModel>("SELECT * FROM dbo.Z_Content WHERE Id = @Id", new { Id = id });
			return View(result.FirstOrDefault());
		}

		[HttpPost]
		[Authorize]
		public ActionResult Online(string mycontent, int contentid = 0,string to = "")
		{
			ViewBag.Message = "在线答疑";

			// 如果是留言
			if (!string.IsNullOrWhiteSpace(to))
			{
				contentid = _conn.ExecuteScalar<int>(@"
					INSERT INTO dbo.Z_Content (Content)
					VALUES (@Content);
					SELECT @@IDENTITY", new { Content = $"" });
				_conn.Execute(@"
					INSERT INTO [dbo].[Z_LiuYan]
							   ([UserId]
							   ,[ContentId])
						 VALUES
							   (@UserId
							   ,@ContentId)", new {UserId = HttpContext.User.Identity.Name, ContentId = contentid});
				_conn.Execute(@"
					INSERT INTO [dbo].[Z_LiuYan]
							   ([UserId]
							   ,[ContentId])
						 VALUES
							   (@UserId
							   ,@ContentId)", new { UserId = to, ContentId = contentid });
			}

			// 获取用户名
			var username = HttpContext.User.Identity.Name;
			// 如果contentid为0, 创建一个新的数据
			if (contentid == 0)
			{
				contentid = _conn.ExecuteScalar<int>(@"
					INSERT INTO dbo.Z_Content (Content)
					VALUES (@Content);
					SELECT @@IDENTITY", new { Content = $"{username} : {mycontent} \r\n" });
			}
			else    // 如果指定id不为0, 则修改内容
			{
				_conn.Execute(@"
					UPDATE [dbo].[Z_Content]
					   SET [Content] = 
					   (
							SELECT Content
							FROM dbo.Z_Content
							WHERE Id = @Id
					   ) + @Content
					WHERE Id = @Id", new { Content = $"{username} : {mycontent} \r\n", Id = contentid });
			}

			// 从指定id中获取数据, 展示出来
			var result = _conn.Query<OnlineViewModel>("SELECT * FROM dbo.Z_Content WHERE Id = @Id", new {Id = contentid});

			return View(result.FirstOrDefault());
		}

		public ActionResult Contact()
		{
			var result = _conn.Query(@"
				SELECT * 
				FROM Z_LiuYan
				WHERE ContentId IN 
				(
					-- 查出所有給指定用户的留言
					SELECT DISTINCT ContentId
					FROM Z_LiuYan
					WHERE UserId = @User
				) AND UserId != @User", new {User = HttpContext.User.Identity.Name});
			
			return View(result);
		}

		public ActionResult LiuYan()
		{
			// 从数据库拿出所有用户 - 除掉笨人
			var emails = _conn.Query("SELECT Email FROM dbo.Z_Users").Where(m => m.Email != HttpContext.User.Identity.Name).ToList();
			List<string> emaillist = emails.Select(m => (string)m.Email).ToList();
			
			return View(emaillist);
		}

	}
}