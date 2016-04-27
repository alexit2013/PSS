using System.Web.Mvc;

namespace UI.Areas.InOut
{
    public class InOutAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InOut";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InOut_default",
                "InOut/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}