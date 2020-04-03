using Modelo.Cadastros;
using Servicos.Cadastros;
using Servicos.Tabelas;
using System.Net;
using System.Web.Mvc;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = produtoServico.ObterProdutoPorId((long)id)
;
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        //Get  DETAILS
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        //Get  DELETE
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }



        /*A	 action	 	Edit		 precisa	 de	 uma	 mudança	 a	 mais,	 antes	 de	 a trazermos	para	cá.	Veja	em	seu	código,	ainda	não	modificado,	que
        existe	o	fato	de	passar	as	categorias	e	fabricantes	para	a		ViewBag	, que	popularão	os		DropDownLists		da	visão.	Esta	mesma	situação
        ocorre	para	a	action		GET	Create	.	Sendo	assim,	vamos	criar	um método	 privado	 que	 resolverá	 este	 problema        */
        /*Veja	 que,	na	assinatura	 do	método,	 o	 parâmetro		produto		 é opcional.	E	quando	ele	não	existir,	é	atribuído		null		a	ele.	Isso	foi
        adotado	para	podermos	diferenciar	quando	o	quarto	parâmetro	do SelectList()		será	informado.         */
        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        //Get  EDIT
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }

        //Get  CREATE
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);
                return View(produto);

            }
        }


        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            return GravarProduto(produto);
        }        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id)
;
                TempData["Message"] = "Produto	" + produto.Nome.ToUpper()+ "	foi	removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
/*
       private EFContext context = new EFContext();

       // GET: Produtos
       public ActionResult Index()
       {
           var produtos = context.Produtos.Include(c => c.Categoria).Include(f => f.Fabricante).OrderBy(n => n.Nome); //O método Include() permite a   inclusão de  associações nos objetos que serão retornados  do DbSet em  questão.
           return View(produtos);
       }

       // GET: Produtos/Create
       public ActionResult Create()
       {

           ViewBag.CategoriaId = new SelectList(context.Categorias.OrderBy(b => b.Nome), "CategoriaId", "Nome"); //utilizado a ViewBag, agora para armazenar objetos de categorias  e de fabricantes
           ViewBag.FabricanteId = new SelectList(context.Fabricantes.OrderBy(b => b.Nome), "FabricanteId", "Nome");
           /*
             O	método	SelectList() representa	 uma lista de	itens da qual o usuário	 pode	 selecionar	 um	 item.	 Em	 nosso	 caso,	 os
               itens	 serão	 expostos	 em	 um	 DropDownList. O	método possui	diversos	construtores,	mas	o	usado	no	código	anterior
               recebe:
               1.	 A	coleção	de	itens	que	popularão	o		DropDownList	;
               2.	 A	 propriedade	 que	 representa	 o	 valor	 que	 será armazenado	no	controle;
               3.	 A	 propriedade	 que	 possui	 o	 valor	 a	 ser	 exibido	 pelo controle.
             */
        /*
                    return View();
                }

                // POST: Produtos/Create
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create(Produto produto)
                {
                    try
                    {
                        context.Produtos.Add(produto);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View(produto);
                    }
                }

                //	GET:	Produtos/Edit/5
                public ActionResult Edit(long? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Produto produto = context.Produtos.Find(id);
                    if (produto == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.CategoriaId = new SelectList(context.Categorias.OrderBy(b => b.Nome), "CategoriaId", "Nome", produto.CategoriaId);
                    ViewBag.FabricanteId = new SelectList(context.Fabricantes.OrderBy(b => b.Nome), "FabricanteId", "Nome", produto.FabricanteId);
                    return View(produto);
                }


                // POST: Produtos/Edit/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit(int id, Produto produto)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            context.Entry(produto).State = EntityState.Modified;
                            context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return View(produto);
                    }
                    catch
                    {
                        return View(produto);
                    }
                }

                // GET: Produtos/Details/5
                public ActionResult Details(long? id)   
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Produto produto = context.Produtos.Where(p => p.ProdutoId == id).Include(c => c.Categoria).Include(f => f.Fabricante).First();
                    if (produto == null)
                    {
                        return HttpNotFound();
                    }
                    return View(produto);
                }

                // GET: Produtos/Delete/5
                public ActionResult Delete(long? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Produto produto = context.Produtos.Where(p => p.ProdutoId ==id).Include(c => c.Categoria).Include(f => f.Fabricante).First();
                    if (produto == null)
                    {
                        return HttpNotFound();
                    }
                    return View(produto);
                }

                //	POST:	Produtos/Delete/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Delete(long id)
                {
                    try
                    {
                        Produto produto = context.Produtos.Find(id);
                        context.Produtos.Remove(produto);
                        context.SaveChanges();
                        TempData["Message"] = "Produto	" + produto.Nome.ToUpper()+ "	foi	removido";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }

            }
        }

            */
