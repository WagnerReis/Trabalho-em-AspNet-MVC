using Marcenaria.Daos;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View(ProdutoDAO.BuscarTodos());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View(ProdutoDAO.BuscarPorId(id));
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Produto p = new Produto();
                p.Nome = collection["nNome"];
                p.Valor = Convert.ToDecimal(collection["nValor"]);
                p.Descricao = collection["nDescricao"];

                if (!ProdutoDAO.Persistir(p))
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

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            Produto p = ProdutoDAO.BuscarPorId(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Produto p = new Produto();
                p.Id = id;
                p.Nome = collection["nNome"];
                p.Valor = Convert.ToDecimal(collection["nValor"]);
                p.Descricao = collection["nDescricao"];


                if (!ProdutoDAO.Persistir(p))
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

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ProdutoDAO.BuscarPorId(id));
        }


        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (ProdutoDAO.Excluir(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ProdutoDAO.BuscarPorId(id));

                }
            }
            catch
            {
                return View(ProdutoDAO.BuscarPorId(id));
            }
        }
    }
}