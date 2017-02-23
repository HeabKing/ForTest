using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_02_23_Swagger_Just_Use_XML.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}
	}
}
