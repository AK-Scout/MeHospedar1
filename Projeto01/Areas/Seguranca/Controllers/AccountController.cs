﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Projeto01.Areas.Seguranca.Models;
using Projeto01.Infraestrutura;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
namespace Projeto01.Areas.Seguranca.Controllers
{
    public class AccountController : Controller
    {
        //GET Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Usuario user = UserManager.Find(details.Usuario, details.Senha);
                if (user == null)
                {
                    ModelState.AddModelError("", "Usuário ou senha inválido(s).");
}
                else
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(user,DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties{ IsPersistent = false }, ident);
                    if (returnUrl == null)
                        returnUrl = "/Home";
                    return Redirect(returnUrl);
                }
            }
            return View(details);
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario user = new Usuario
                {
                    UserName = model.Nome,
                    Email = model.Email
                };
                    return RedirectToAction("Index");

            }
            return View(model);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}