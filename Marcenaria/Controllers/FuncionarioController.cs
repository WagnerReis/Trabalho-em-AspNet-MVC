using Marcenaria.Dao;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            return View(FuncionarioDao.BuscarTodos());
        }

        // GET: Funcionario/Details/5
        public ActionResult Details(int id)
        {
            Funcionario i = FuncionarioDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // GET: Funcionario/Create
        public ActionResult Create()
        {
            ViewBag.Usuarios = UsuarioDao.BuscarTodos();

            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                Funcionario i = new Funcionario();
                i.Nome = collection["Nome"];
                i.Tipo = Convert.ToBoolean(collection["Tipo"]);
                i.Cnpj = collection["Cnpj"];
                i.Cpf = collection["Cpf"];
                i.Telefone = Convert.ToString(collection["Telefone"]);
                i.Celular = Convert.ToString(collection["Celular"]);
                i.Estado = collection["Estado"];
                i.Cidade = collection["Cidade"];
                i.Bairro = collection["Bairro"];
                i.Rua = collection["Rua"];
                i.Numero = Convert.ToInt32(collection["Numero"]);
                i.Usuario = UsuarioDao.BuscarPorId(Convert.ToInt32(collection["Usuario.Id"]));

                FuncionarioDao.Persistir(i);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionario/Edit/5
        public ActionResult Edit(int id)
        {
            Funcionario i = FuncionarioDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Funcionario i = new Funcionario();
                i.Nome = collection["Nome"];
                i.Tipo = Convert.ToBoolean(collection["Tipo"]);
                i.Cnpj = collection["Cnpj"];
                i.Cpf = collection["Cpf"];
                i.Telefone = Convert.ToString(collection["Telefone"]);
                i.Celular = Convert.ToString(collection["Celular"]);
                i.Estado = collection["Estado"];
                i.Cidade = collection["Cidade"];
                i.Bairro = collection["Bairro"];
                i.Rua = collection["Rua"];
                i.Numero = Convert.ToInt32(collection["Numero"]);
                i.Usuario = UsuarioDao.BuscarPorId(Convert.ToInt32(collection["Usuario.Id"]));

                if (!FuncionarioDao.Persistir(i))
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

        // GET: Funcionario/Delete/5
        public ActionResult Delete(int id)
        {
            Funcionario i = FuncionarioDao.BuscarPorId(id);
            if (i == null)
            {
                return HttpNotFound();
            }

            return View(i);
        }

        // POST: Funcionario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (FuncionarioDao.Excluir(id))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(FuncionarioDao.BuscarPorId(id));
                }
            }
            catch
            {
                return View(FuncionarioDao.BuscarPorId(id));
            }
        }
    }
}
