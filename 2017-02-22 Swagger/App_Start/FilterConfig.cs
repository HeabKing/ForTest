using System.Web;
using System.Web.Mvc;

namespace _2017_02_22_Swagger
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
