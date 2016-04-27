using System.Web.Mvc;

namespace UI.Areas.InStock
{
    public class InStockAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InStock";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InStock_default",
                "InStock/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}