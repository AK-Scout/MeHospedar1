using System.Web.Mvc;

namespace Projeto01.Areas.Seguranca
{
    public class SegurancaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Seguranca";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               name: "Seguranca",
               url: "Seguranca/{controller}/{action}/{id}",
              defaults:  new { controller = "Seguranca", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}