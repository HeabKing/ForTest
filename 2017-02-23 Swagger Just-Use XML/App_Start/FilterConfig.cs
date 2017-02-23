using System.Web;
using System.Web.Mvc;

namespace _2017_02_23_Swagger_Just_Use_XML
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
