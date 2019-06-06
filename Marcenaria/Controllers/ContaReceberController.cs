using Marcenaria.Daos;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class ContaReceberController : Controller
    {
        // GET: ContaReceber
        public ActionResult Index()
        {
            return View(ContaReceberDAO.BuscarTodos());
        }

        // GET: ContaReceber/Details/5
        public ActionResult Details(int id)
        {
            return View(ContaReceberDAO.BuscarPorId(id));
        }

        // GET: ContaReceber/Create
        public ActionResult Create()
        {
            ViewBag.Vendas = VendaDAO.BuscarTodos();
            return View();
        }

        // POST: ContaReceber/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ContaReceber cr  = new ContaReceber();
                cr.Valor = Convert.ToDecimal(collection["Valor"]);
                cr.Data = Convert.ToDateTime(collection["Data"]);
                cr.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(collection["Venda.Id"]));

                if (!ContaReceberDAO.Persistir(cr))
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

        // GET: ContaReceber/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Vendas = VendaDAO.BuscarTodos();
            ContaReceber cr = ContaReceberDAO.BuscarPorId(id);
            if(cr == null)
            {
                return HttpNotFound();
            }
            return View(cr);
        }

        // POST: ContaReceber/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ContaReceber cr = new ContaReceber();
                cr.Id = id;
                cr.Valor = Convert.ToDecimal(collection["nValor"]);
                cr.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(collection["Venda.Id"]));

                if (!string.IsNullOrEmpty(collection["nData"]))
                {
                    cr.Data = Convert.ToDateTime(collection["nData"]);
                }
                else
                {
                    cr.Data = DateTime.Now;
                }


                if (!ContaReceberDAO.Persistir(cr))
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

        // GET: ContaReceber/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ContaReceberDAO.BuscarPorId(id));
        }


        // POST: ContaReceber/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (ContaReceberDAO.Excluir(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ContaReceberDAO.BuscarPorId(id));

                }
            }
            catch
            {
                return View(ContaReceberDAO.BuscarPorId(id));
            }
        }
    }
}