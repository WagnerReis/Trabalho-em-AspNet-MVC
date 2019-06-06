using Marcenaria.Daos;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class EntregaController : Controller
    {
        // GET: Entrega
        public ActionResult Index()
        {
            return View(EntregaDAO.BuscarTodos());
        }

        // GET: Entrega/Details/5
        public ActionResult Details(int id)
        {
            return View(EntregaDAO.BuscarPorId(id));
        }

        // GET: Entrega/Create
        public ActionResult Create()
        {
            ViewBag.Vendas = VendaDAO.BuscarTodos();
            return View();
        }

        // POST: Entrega/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Entrega e = new Entrega();
                e.Data = DateTime.Now;
                e.Estado = collection["nEstado"];
                e.Cidade = collection["nCidade"];
                e.Rua = collection["nRua"];
                e.Numero = Convert.ToInt32(collection["nNumero"]);
                e.Referencia = collection["nReferencia"];
                e.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(collection["Venda.Id"]));

                if (!EntregaDAO.Persistir(e))
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

        // GET: Entrega/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Vendas = VendaDAO.BuscarTodos();
            Entrega e = EntregaDAO.BuscarPorId(id);
            if(e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        // POST: Entrega/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Entrega e = new Entrega();
                e.Id = id;
                e.Estado = collection["nEstado"];
                e.Cidade = collection["nCidade"];
                e.Rua = collection["nRua"];
                e.Numero = Convert.ToInt32(collection["nNumero"]);
                e.Referencia = collection["nReferencia"];
                e.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(collection["Venda.Id"]));

                if (!string.IsNullOrEmpty(collection["nData"]))
                {
                    e.Data = Convert.ToDateTime(collection["nData"]);
                }
                else
                {
                    e.Data = DateTime.Now;
                }


                if (!EntregaDAO.Persistir(e))
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

        // GET: Entrega/Delete/5
        public ActionResult Delete(int id)
        {
            return View(EntregaDAO.BuscarPorId(id));
        }


        // POST: Entrega/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (EntregaDAO.Excluir(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(EntregaDAO.BuscarPorId(id));

                }
            }
            catch
            {
                return View(EntregaDAO.BuscarPorId(id));
            }
        }
    }
}