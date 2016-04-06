using System.Web;
using System.Web.Mvc;

namespace _2016_04_06_JSONP_Study
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
