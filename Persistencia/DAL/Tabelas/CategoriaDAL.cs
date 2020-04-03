using Modelo.Cadastros;
using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    public class CategoriaDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Categoria>ObterCategoriasClassificadasPorNome()
        {
            return context.Categorias.OrderBy(b => b.Nome);
        }
    }
}