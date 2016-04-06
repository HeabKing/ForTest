using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2016_04_06_JSONP_Study.Controllers
{
    public class JsonpController : Controller
    {
        // GET: Jsonp
        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult AjaxRequest()
	    {
		    var callback = Request["callback"];
		    return Content($"{callback}({{Hello:'World'}})");
        }
    }
}