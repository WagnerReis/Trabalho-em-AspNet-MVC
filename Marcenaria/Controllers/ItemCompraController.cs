using Marcenaria.Dao;
using Marcenaria.Daos;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class ItemCompraController : Controller
    {
        // GET: ItemCompra
        public ActionResult Index()
        {
            return View(ItemCompraDao.BuscarTodos());
        }

        // GET: ItemCompra/Details/5
        public ActionResult Details(int id)
        {
            ItemCompra i = ItemCompraDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // GET: ItemCompra/Create
        public ActionResult Create()
        {
            ViewBag.Compras = CompraDao.BuscarTodos();
            ViewBag.Produtos = ProdutoDAO.BuscarTodos();

            return View();
        }

        // POST: ItemCompra/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ItemCompra i = new ItemCompra();
                i.Valor = Convert.ToDecimal(collection["Valor"]);
                i.Quantidade = Convert.ToInt16(collection["Quantidade"]);
                i.Compra = CompraDao.BuscarPorId(Convert.ToInt32(collection["Compra.Id"]));
                i.Produto = ProdutoDAO.BuscarPorId(Convert.ToInt32(collection["Produto.Id"]));

                ItemCompraDao.Persistir(i);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemCompra/Edit/5
        public ActionResult Edit(int id)
        {
            ItemCompra i = ItemCompraDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: ItemCompra/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ItemCompra i = new ItemCompra();
                i.Valor = Convert.ToDecimal(collection["Valor"]);
                i.Quantidade = Convert.ToInt16(collection["Quantidade"]);
                i.Compra = CompraDao.BuscarPorId(Convert.ToInt32(collection["Compra.Id"]));
                i.Produto = ProdutoDAO.BuscarPorId(Convert.ToInt32(collection["Produto.Id"]));

                if (!ItemCompraDao.Persistir(i))
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

        // GET: ItemCompra/Delete/5
        public ActionResult Delete(int id)
        {
            ItemCompra i = ItemCompraDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: ItemCompra/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (ItemCompraDao.Excluir(id))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ItemCompraDao.BuscarPorId(id));
                }
            }
            catch
            {
                return View(ItemCompraDao.BuscarPorId(id));
            }
        }
    }
}
