using System.Web;
using System.Web.Mvc;

namespace _2016_03_15DIStudy
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
