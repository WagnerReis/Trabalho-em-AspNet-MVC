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
    public class VendaController : Controller
    {
        // GET: Venda
        public ActionResult Index()
        {
            return View(VendaDAO.BuscarTodos());
        }

        // GET: Venda/Details/5
        public ActionResult Details(int id)
        {
            return View(VendaDAO.BuscarPorId(id));
        }

        // GET: Venda/Create
        public ActionResult Create()
        {
            ViewBag.Clientes = ClienteDAO.BuscarTodos();
            ViewBag.Funcionarios = FuncionarioDao.BuscarTodos();
            return View();
        }

        // POST: Venda/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Venda v = new Venda();
                v.Data = Convert.ToDateTime(collection["Data"]);
                v.Cliente = ClienteDAO.BuscarPorId(Convert.ToInt32(collection["Cliente.Id"]));
                v.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(collection["Funcionario.Id"]));

                if (!VendaDAO.Persistir(v))
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

        // GET: Venda/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Clientes = ClienteDAO.BuscarTodos();
            ViewBag.Funcionarios = FuncionarioDao.BuscarTodos();
            Venda v = VendaDAO.BuscarPorId(id);
            if(v == null)
            {
                return HttpNotFound();
            }
            return View(v);
        }

        // POST: Venda/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Venda v = new Venda();
                v.Id = id;
                v.Cliente = ClienteDAO.BuscarPorId(Convert.ToInt32(collection["Cliente.Id"]));
                v.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(collection["Funcionario.Id"]));
                
                if (!string.IsNullOrEmpty(collection["nData"]))
                {
                    v.Data = Convert.ToDateTime(collection["nData"]);
                }
                else
                {
                    v.Data = DateTime.Now;
                }


                if (!VendaDAO.Persistir(v))
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

        // GET: Venda/Delete/5
        public ActionResult Delete(int id)
        {
            return View(VendaDAO.BuscarPorId(id));
        }


        // POST: Venda/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (VendaDAO.Excluir(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(VendaDAO.BuscarPorId(id));

                }
            }
            catch
            {
                return View(VendaDAO.BuscarPorId(id));
            }
        }
    }
}