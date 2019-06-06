using Marcenaria.Dao;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            return View(CompraDao.BuscarTodos());
        }

        // GET: Compra/Details/5
        public ActionResult Details(int id)
        {
            Compra i = CompraDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // GET: Compra/Create
        public ActionResult Create()
        {
            ViewBag.fornecedores = FornecedorDao.BuscarTodos();
            ViewBag.funcionarios = FuncionarioDao.BuscarTodos();
            return View();
        }

        // POST: Compra/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Compra i = new Compra();
                i.Data = Convert.ToDateTime(collection["Data"]);
                i.Fornecedor = FornecedorDao.BuscarPorId(Convert.ToInt32(collection["fornecedor.Id"])); ;
                i.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(collection["Funcionario.Id"]));
                // i.ContasPagar =
                // i.ItensCompra = 


                CompraDao.Persistir(i);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Compra/Edit/5
        public ActionResult Edit(int id)
        {
            Compra i = CompraDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
           
        }

        // POST: Compra/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Compra i = new Compra();
                i.Data = Convert.ToDateTime(collection["Data"]);
                i.Fornecedor = FornecedorDao.BuscarPorId(Convert.ToInt32(collection["Fornecedor.Id"])); ;
                i.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(collection["Funcionario.Id"]));
                // i.ContasPagar =
                // i.ItensCompra = 

                if (!CompraDao.Persistir(i))
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

        // GET: Compra/Delete/5
        public ActionResult Delete(int id)
        {
            Compra i = CompraDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: Compra/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (CompraDao.Excluir(id))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(CompraDao.BuscarPorId(id));
                }
            }
            catch
            {
                return View(CompraDao.BuscarPorId(id));
            }
        }
    }
}
