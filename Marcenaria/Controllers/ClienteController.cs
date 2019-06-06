using Marcenaria.Daos;
using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcenaria.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View(ClienteDAO.BuscarTodos());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View(ClienteDAO.BuscarPorId(id));
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nome = collection["nNome"];
                cliente.Telefone = collection["nTelefone"];
                cliente.Celular = collection["nCelular"];
                cliente.Tipo = Convert.ToBoolean(collection["nTipo"]);
                cliente.Cpf = collection["nCpf"];
                cliente.Cnpj = collection["nCnpj"];
                cliente.Estado = collection["nEstado"];
                cliente.Cidade = collection["nCidade"];
                cliente.Bairro = collection["nBairro"];
                cliente.Rua = collection["nRua"];
                cliente.Numero = Convert.ToInt32(collection["nNumero"]);

                if (!ClienteDAO.Persistir(cliente))
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

        // GET: Instituicao/Edit/5
        public ActionResult Edit(int id)
        {
            Cliente cliente = ClienteDAO.BuscarPorId(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Instituicao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = id;
                cliente.Nome = collection["nNome"];
                cliente.Telefone = collection["nTelefone"];
                cliente.Celular = collection["nCelular"];
                cliente.Cpf = collection["nCpf"];
                cliente.Cnpj = collection["nCnpj"];
                cliente.Estado = collection["nEstado"];
                cliente.Cidade = collection["nCidade"];
                cliente.Bairro = collection["nBairro"];
                cliente.Rua = collection["nRua"];
                cliente.Numero = Convert.ToInt32(collection["nNumero"]);

                if (!string.IsNullOrEmpty(collection["nTipo"]))
                {
                    cliente.Tipo = Convert.ToBoolean(collection["nTipo"].Contains("true"));
                }
                else
                {
                    cliente.Tipo = true;
                }


                if (!ClienteDAO.Persistir(cliente))
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

        // GET: Instituicao/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ClienteDAO.BuscarPorId(id));
        }

        // POST: Instituicao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (ClienteDAO.Excluir(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ClienteDAO.BuscarPorId(id));

                }
            }
            catch
            {
                return View(ClienteDAO.BuscarPorId(id));
            }
        }
    }
}