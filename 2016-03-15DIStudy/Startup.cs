using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_2016_03_15DIStudy.Startup))]
namespace _2016_03_15DIStudy
{
	public partial class Startup
	{
		private static int i = 0;
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			
		}
	}
}
