using System.Web.Mvc;

namespace Projeto01.Areas.Cadastros
{
    public class CadastrosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cadastros";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Cadastros",
                url: "Cadastros/{controller}/{action}/{id}",
                defaults: new { controller = "Cadastros", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}