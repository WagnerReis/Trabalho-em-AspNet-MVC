using Marcenaria.Dao;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class ContaPagarController : Controller
    {
        // GET: ContaPagar
        public ActionResult Index()
        {
            return View(ContaPagarDao.BuscarTodos());
        }

        // GET: ContaPagar/Details/5
        public ActionResult Details(int id)
        {
            ContaPagar i = ContaPagarDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // GET: ContaPagar/Create
        public ActionResult Create()
        {
            ViewBag.Compras = CompraDao.BuscarTodos();
            return View();
        }

        // POST: ContaPagar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ContaPagar i = new ContaPagar();
                i.Valor = Convert.ToDecimal(collection["Valor"]);
                i.Data = Convert.ToDateTime(collection["Data"]);
                i.Compra = CompraDao.BuscarPorId(Convert.ToInt32(collection["Compra.Id"]));

                ContaPagarDao.Persistir(i);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContaPagar/Edit/5
        public ActionResult Edit(int id)
        {
            ContaPagar i = ContaPagarDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: ContaPagar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ContaPagar i = new ContaPagar();
                i.Valor = Convert.ToDecimal(collection["Valor"]);
                i.Data = Convert.ToDateTime(collection["Data"]);
                i.Compra = CompraDao.BuscarPorId(Convert.ToInt32(collection["Compra.Id"]));

                if (!ContaPagarDao.Persistir(i))
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

        // GET: ContaPagar/Delete/5
        public ActionResult Delete(int id)
        {
            ContaPagar i = ContaPagarDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: ContaPagar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (ContaPagarDao.Excluir(id))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ContaPagarDao.BuscarPorId(id));
                }
            }
            catch
            {
                return View(ContaPagarDao.BuscarPorId(id));
            }
        }
    }
}
