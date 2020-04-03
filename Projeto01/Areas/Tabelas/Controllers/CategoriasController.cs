using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Persistencia.Contexts;
using System.Net;
using System.Data.Entity;
using Modelo.Tabelas;

namespace Projeto01.Areas.Tabelas.Controllers

{
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();
        //	GET:	Categorias
        public ActionResult Index()
        {
            return View(context.Categorias.OrderBy(c => c.Nome));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            context.Categorias.Add(categoria);
            context.SaveChanges(); //salva as alterações no banco
            return RedirectToAction("Index");
        }

        //	GET:	Categorias/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //	POST:	Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified; //seta informação de que o objeto recebido sofreu uma alteração desde sua recuperação
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        //	GET:	Testes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           // Categoria categoria = context.Categorias.Find(id);
            Categoria categoria = context.Categorias.Where(f => f.CategoriaId == id).Include("Produtos.Fabricante").First();

            if (categoria== null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //	GET:	Categorias/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }


        //	POST:	Categorias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = context.Categorias.Find(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            TempData["Message"] = "Categoria	" + categoria.Nome.ToUpper() + "	foi	removida";
            return RedirectToAction("Index");
        }

    }
}


