using Microsoft.AspNet.Identity.EntityFramework;
using Projeto01.Areas.Seguranca.Models;
using System.Data.Entity;


namespace Projeto01.DAL
{
    public class IdentityDbContextAplicacao :IdentityDbContext<Usuario>
    {
        public IdentityDbContextAplicacao():base("IdentityDb")
        {
        }
        static IdentityDbContextAplicacao()
        {
            Database.SetInitializer<IdentityDbContextAplicacao>(new IdentityDbInit());
        }
        public static IdentityDbContextAplicacao Create()
        {
            return new IdentityDbContextAplicacao();
        }

        public System.Data.Entity.DbSet<Projeto01.Areas.Seguranca.Models.UsuarioViewModel> UsuarioViewModels { get; set; }
    }
    public class IdentityDbInit:DropCreateDatabaseIfModelChanges<IdentityDbContextAplicacao>
    {
    }
}