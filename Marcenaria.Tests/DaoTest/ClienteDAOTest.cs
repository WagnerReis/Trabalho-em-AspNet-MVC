using System;
using System.Collections.Generic;
using Marcenaria.Daos;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.Daos
{
    [TestClass]
    public class ClienteDAOTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Cliente> clientes = ClienteDAO.BuscarTodos();
            Assert.IsTrue(clientes.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            Cliente c = new Cliente();
            c.Nome = "Novo cliente";
            c.Telefone = "3255552222";
            c.Celular = "32999997777";
            c.Tipo = true;
            c.Cpf = "xxxxxxxxxxx";
            c.Cnpj = "15523847521369";
            c.Estado = "Minas Gerais";
            c.Cidade = "Juiz de Fora";
            c.Bairro = "Novo bairro";
            c.Rua = "Nova rua";
            c.Numero = 123;

            Assert.IsTrue(ClienteDAO.Persistir(c));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Cliente c = ClienteDAO.BuscarPorId(ClienteDAO.getLastId());
            c.Nome = "Cliente atualizado";
            c.Telefone = "3244445555";
            c.Celular = "32977779999";
            c.Tipo = false;
            c.Cpf = "54831439721";
            c.Cnpj = "xxxxxxxxxxxxxx";
            c.Estado = "São Paulo";
            c.Cidade = "São Paulo";
            c.Bairro = "Bairro atualizado";
            c.Rua = "Rua atualizada";
            c.Numero = 321;

            Assert.IsTrue(ClienteDAO.Persistir(c));
        }

        [TestMethod]
        public void BuscarID()
        {
            Cliente c = ClienteDAO.BuscarPorId(ClienteDAO.getLastId());
            Assert.IsNotNull(c);
        }



        [TestMethod]
        public void Deletar()
        {
            Cliente c = ClienteDAO.BuscarPorId(ClienteDAO.getLastId());

            Assert.IsTrue(ClienteDAO.Excluir(ClienteDAO.getLastId()));
            
        }


        [TestMethod]
        public void Buscar()
        {
            List<Cliente> clientes = ClienteDAO.buscar("Cliente");
            Assert.IsTrue(clientes.Count > 0);
        }
    }
}
