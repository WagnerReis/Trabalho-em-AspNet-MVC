using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Daos;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.Daos
{
    [TestClass]
    public class VendaDAOTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Venda> vendas = VendaDAO.BuscarTodos();
            Assert.IsTrue(vendas.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            Venda v = new Venda();
            
            v.Data = Convert.ToDateTime("05/05/2019");
            v.Cliente = ClienteDAO.BuscarPorId(ClienteDAO.getLastId());
            v.Funcionario = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId());

            Assert.IsTrue(VendaDAO.Persistir(v));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Venda v = VendaDAO.BuscarPorId(VendaDAO.getLastId());
            v.Data = DateTime.Now;
            v.Cliente = ClienteDAO.BuscarPorId(2);
            v.Funcionario = FuncionarioDao.BuscarPorId(1);

            Assert.IsTrue(VendaDAO.Persistir(v));
        }

        [TestMethod]
        public void BuscarID()
        {
            Venda venda = VendaDAO.BuscarPorId(VendaDAO.getLastId());
            Assert.IsNotNull(venda);
        }



        [TestMethod]
        public void Deletar()
        {
            Venda v = VendaDAO.BuscarPorId(VendaDAO.getLastId());

            Assert.IsTrue(VendaDAO.Excluir(VendaDAO.getLastId()));
            
        }


        [TestMethod]
        public void Buscar()
        {
            List<Venda> instituicoes = VendaDAO.buscar("1");
            
            Assert.IsTrue(instituicoes.Count > 0);
        }
    }
}
