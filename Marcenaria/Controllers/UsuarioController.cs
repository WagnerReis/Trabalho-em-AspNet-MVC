using Marcenaria.Dao;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View(UsuarioDao.BuscarTodos());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            Usuario i = UsuarioDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Usuario i = new Usuario();
                i.Login = collection["Login"];
                i.Senha = collection["Senha"];

                UsuarioDao.Persistir(i);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            Usuario i = UsuarioDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }
            return View(i);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Usuario i = new Usuario();
                i.Login = collection["Login"];
                i.Senha = collection["Senha"];

                if (!UsuarioDao.Persistir(i))
                {
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            Usuario i = UsuarioDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (UsuarioDao.Excluir(id))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(UsuarioDao.BuscarPorId(id));
                }
            }
            catch
            {
                return View(UsuarioDao.BuscarPorId(id));
            }
        }
    }
}
