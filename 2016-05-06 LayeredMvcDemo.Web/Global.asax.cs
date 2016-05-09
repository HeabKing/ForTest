using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using _2016_05_06_LayeredMvcDemo.Web.App_Start;

namespace _2016_05_06_LayeredMvcDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			// 把MVC框架的默认controller factory换掉
	        var ctrlFactory = new MyControllerFactory();
			// MVC构造函数的注入
	        ControllerBuilder.Current.SetControllerFactory(ctrlFactory);
        }
    }
}
