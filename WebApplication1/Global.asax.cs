using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.MessageService;
using _2016_05_13_HeabConsoleApp;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			
        }

	    public static async Task OaSpiderAsync()
	    {
			while (true)
			{
				await Task.Delay(new TimeSpan(0, 30, 0)).ConfigureAwait(false);
				if (DateTime.Now.Hour == 19 && DateTime.Now.Minute < 30)
				{
					try
					{
						Program.OaRun();
					}
					catch (Exception e)
					{
						new EmailServiceFromQq("heabking@qq.com", "pwuomsefcevacbeg").SendMessage(new EmailMessage { Msg = "OA程序出现异常<br/>" + e, Subject = "OA程序异常" }, "394899990@qq.com");
					}
				}
			}
		}
    }
}
