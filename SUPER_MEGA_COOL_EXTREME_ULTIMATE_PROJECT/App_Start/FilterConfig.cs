using System.Web;
using System.Web.Mvc;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
