using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using _2016_05_06_LayeredMvcDemo.DataAccess;
using _2016_05_06_LayeredMvcDemo.Web.App_Start;

namespace _2016_05_06_LayeredMvcDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			//// 把MVC框架的默认controller factory换掉
	  //      var ctrlFactory = new MyControllerFactory();
			//// MVC构造函数的注入
	  //      ControllerBuilder.Current.SetControllerFactory(ctrlFactory);

			// 设定Mvc引用程序的全局dependency resolver对象
	        var myResolver = new MyDependencyResolver();
	        DependencyResolver.SetResolver(myResolver);
        }
		/// <summary>
		/// 在每一次Http请求开始时建立一个新的SouthwindContext对象, 并保存到当前HttpContext对象的Items集合属性中
		/// </summary>
		protected void Application_BeginRequest()
	    {
		    HttpContext.Current.Items["DbContext"] = new SouthwindContext();
	    }
		/// <summary>
		/// 每当一个Http请求结束的时候, 将原先保存的CouthwindContext对象清除
		/// </summary>
	    protected void Application_EndRequest()
	    {
			var db = HttpContext.Current.Items["DbContext"] as SouthwindContext;
		    db?.Dispose();
	    }
    }
}
