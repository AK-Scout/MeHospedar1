﻿using Persistencia.Contexts;
using System.Linq;
using System.Web.Mvc;
using Modelo.Cadastros;
using System.Net;
using System.Data.Entity;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class FabricantesController : Controller
    {
        private EFContext context = new EFContext();
        //	GET:	Fabricantes
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(c => c.Nome));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            context.Fabricantes.Add(fabricante);
            context.SaveChanges(); //salva as alterações no banco
            return RedirectToAction("Index");
        }

        //	GET:	Fabricantes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = context.Fabricantes.Find(id);

            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        //	POST:	Fabricantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                context.Entry(fabricante).State = EntityState.Modified; //seta informação de que o objeto recebido sofreu uma alteração desde sua recuperação
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        //	GET:	Testes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           // Fabricante fabricante = context.Fabricantes.Find(id);
            Fabricante fabricante = context.Fabricantes.Where(f =>f.FabricanteId ==id).Include("Produtos.Categoria").First();
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        //	GET:	Fabricantes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Find(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }


        //	POST:	Fabricantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = context.Fabricantes.Find(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            TempData["Message"] = "Fabricante	" + fabricante.Nome.ToUpper() + "	foi	removido";
            return RedirectToAction("Index");
        }




    }
}