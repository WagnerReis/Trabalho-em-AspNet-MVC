using Marcenaria.Daos;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class ItemVendaController : Controller
    {
        // GET: ItemVenda
        public ActionResult Index()
        {
            return View(ItemVendaDAO.BuscarTodos());
        }

        // GET: ItemVenda/Details/5
        public ActionResult Details(int id)
        {
            return View(ItemVendaDAO.BuscarPorId(id));
        }

        // GET: ItemVenda/Create
        public ActionResult Create()
        {
            ViewBag.Produtos = ProdutoDAO.BuscarTodos();
            ViewBag.Vendas = VendaDAO.BuscarTodos();
            return View();
        }

        // POST: ItemVenda/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ItemVenda itemVenda = new ItemVenda();
                itemVenda.Quantidade = Convert.ToInt32(collection["Quantidade"]);
                itemVenda.Valor = Convert.ToDecimal(collection["Valor"]);
                itemVenda.Produto = ProdutoDAO.BuscarPorId(Convert.ToInt32(collection["Produto.Id"]));
                itemVenda.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(collection["Venda.Id"]));

                if (!ItemVendaDAO.Persistir(itemVenda))
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

        // GET: ItemVenda/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Produtos = ProdutoDAO.BuscarTodos();
            ViewBag.Vendas = VendaDAO.BuscarTodos();
            return View(ItemVendaDAO.BuscarPorId(id));
        }

        // POST: ItemVenda/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ItemVenda itemVenda = new ItemVenda();
                itemVenda.Id = id;
                itemVenda.Quantidade = Convert.ToInt32(collection["nQuantidade"]);
                itemVenda.Valor = Convert.ToDecimal(collection["nValor"]);
                itemVenda.Produto = ProdutoDAO.BuscarPorId(Convert.ToInt32(collection["Produto.Id"]));
                itemVenda.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(collection["Venda.Id"]));

                if (!ItemVendaDAO.Persistir(itemVenda))
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

        // GET: ItemVenda/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ItemVendaDAO.BuscarPorId(id));
        }


        // POST: ItemVenda/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (ItemVendaDAO.Excluir(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ItemVendaDAO.BuscarPorId(id));

                }
            }
            catch
            {
                return View(ItemVendaDAO.BuscarPorId(id));
            }
        }
    }
}