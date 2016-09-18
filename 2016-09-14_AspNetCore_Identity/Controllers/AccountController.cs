using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _2016_09_14_AspNetCore_Identity.Model;
using _2016_09_14_AspNetCore_Identity.Model.AccountViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace _2016_09_14_AspNetCore_Identity.Controllers
{
	public class AccountController : Controller
	{
		private static readonly IList<User> Accounts = new List<User>();
		private static readonly string CookieAuthenticationScheme = "MyCookieMiddlewareInstance";
		public IActionResult Index()
		{
			var userName = HttpContext.Authentication.GetAuthenticateInfoAsync(CookieAuthenticationScheme)
				.Result.Principal.Claims.FirstOrDefault(m=> m.Type== "UserName")?
				.Value ?? "游客";
			ViewBag.UserName = userName;
			return View();
		}

		public async Task<IActionResult> Register(RegisterViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var user = new Controllers.User
				{
					Id = Accounts.Count > 0 ? Accounts.Max(m => m.Id) + 1 : 0,
					UserName = vm.UserName,
					Password = vm.Password
				};
				Accounts.Add(user);
				await HttpContext.Authentication.SignInAsync(CookieAuthenticationScheme, CreateClaimsPrincipalAsync(user));   // TODO 总结 ClaimsPrincipal 实例的创建
				return Redirect("/Account/Index");
			}
			return View();
		}

		public async Task<IActionResult> Login(LoginViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var b = await Task.FromResult(Accounts.Count(m => vm.UserName == m.UserName && m.Password == vm.Password) > 0);
				await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", User);
				return Redirect("/Account/Index");
			}
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
			return Content("ok");
		}

		private ClaimsPrincipal CreateClaimsPrincipalAsync(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}
			ClaimsIdentity id = new ClaimsIdentity();
			id.AddClaim(new Claim(nameof(user.UserName), user.UserName, "just a string"));
			return new ClaimsPrincipal(id);
		}
	}

	class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
