using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_2016_03_16答疑系统.Startup))]
namespace _2016_03_16答疑系统
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

	        var builder = new ContainerBuilder();

			// 注册数据库连接服务
	        string connString =
				"Data Source=.;Initial Catalog=DaYiXiTong;Integrated Security=true;";
			builder.Register(c => new SqlConnection(connString)).As<DbConnection>();

			// Register your MVC controllers. 可以直接在构造函数中进行注入
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
