using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dapper;
using _2016_03_16答疑系统.ViewModel;

namespace _2016_03_16答疑系统.Controllers
{
	[Authorize]
	public class OnlineController : Controller
	{
		private readonly DbConnection _conn;
		public OnlineController(DbConnection conn)
		{
			_conn = conn;
		}
		public ActionResult ChoicePerson()
		{
			// 从数据库拿出所有用户 - 除掉本人
			var emails = _conn.Query("SELECT Email FROM dbo.Z_Users").Where(m => m.Email != HttpContext.User.Identity.Name).ToList();
			List<string> emaillist = emails.Select(m => (string)m.Email).ToList();

			return View("ChoiceOnlinePerson", emaillist);
		}

		public async Task<ActionResult> HasChat()
		{
			var result = await _conn.QueryAsync(@"
				-- 拿23小时之内的数据
				SELECT * 
				FROM dbo.Z_Content 
				WHERE (GETDATE() - CreateTime) 
					< '1900-01-01 23:01:00'
					AND IsOnline=1");
			var enumerable = result as dynamic[] ?? result.ToArray();
			if (!enumerable.Any()) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			var task = _conn.ExecuteAsync(@"
					UPDATE [dbo].[Z_Content]
						SET [CreateTime] = '2000-01-01 00:00:00'--(GETDATE() - '00:01:01')
					WHERE Id = @Id", new { enumerable.First().Id });
			return Json(enumerable.FirstOrDefault());
		}

		[HttpPost]
		public ActionResult GetChatWindow(string to)
		{
			return View("Online", new OnlineViewModel { Content = "", To = to, Id = 0 });
		}

		public ActionResult GetContent(int contentid)
		{
			// 从指定id中获取数据, 展示出来
			var result = _conn.Query<OnlineViewModel>("SELECT * FROM dbo.Z_Content WHERE Id = @Id", new { Id = contentid });
			return Json(new { content = result.FirstOrDefault()?.Content });
		}
	}
}