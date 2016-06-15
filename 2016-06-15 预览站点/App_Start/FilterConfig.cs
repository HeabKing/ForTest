using System.Web;
using System.Web.Mvc;

namespace _2016_06_15_预览站点
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
