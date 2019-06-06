using Marcenaria.Dao;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public ActionResult Index()
        {
            return View(FornecedorDao.BuscarTodos());
        }

        // GET: Fornecedor/Details/5
        public ActionResult Details(int id)
        {
            Fornecedor i = FornecedorDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Fornecedor i = new Fornecedor();
                i.Telefone = Convert.ToString(collection["Telefone"]);
                i.Celular = Convert.ToString(collection["Celular"]);
                i.RazaoSocial = collection["RazaoSocial"];
                i.Cnpj = collection["Cnpj"];
                i.Estado = collection["Estado"];
                i.Cidade = collection["Cidade"];
                i.Bairro = collection["Bairro"];
                i.Rua = collection["Rua"];
                i.Numero = Convert.ToInt32(collection["Numero"]);

                FornecedorDao.Persistir(i);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Fornecedor/Edit/5
        public ActionResult Edit(int id)
        {
            Fornecedor i = FornecedorDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }
            return View(i);
        }

        // POST: Fornecedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Fornecedor i = new Fornecedor();
                i.Telefone = Convert.ToString(collection["Telefone"]);
                i.Celular = Convert.ToString(collection["Celular"]);
                i.RazaoSocial = collection["RazaoSocial"];
                i.Cnpj = collection["Cnpj"];
                i.Estado = collection["Estado"];
                i.Cidade = collection["Cidade"];
                i.Bairro = collection["Bairro"];
                i.Rua = collection["Rua"];
                i.Numero = Convert.ToInt32(collection["Numero"]);

                if (!FornecedorDao.Persistir(i))
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

        // GET: Fornecedor/Delete/5
        public ActionResult Delete(int id)
        {
            Fornecedor i = FornecedorDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (FornecedorDao.Excluir(id))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(FornecedorDao.BuscarPorId(id));
                }
            }
            catch
            {
                return View(FornecedorDao.BuscarPorId(id));
            }
        }
    }
}
