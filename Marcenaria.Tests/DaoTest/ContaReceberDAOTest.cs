using System;
using System.Collections.Generic;
using Marcenaria.Daos;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.Daos
{
    [TestClass]
    public class ContaReceberDAOTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<ContaReceber> contaRecebers = ContaReceberDAO.BuscarTodos();
            Assert.IsTrue(contaRecebers.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            //Venda venda = VendaDAO.BuscarPorId(VendaDAO.getLastId());
            ContaReceber cr = new ContaReceber();
            cr.Valor = 500.0M;
            cr.Data = Convert.ToDateTime("20/10/2019");
            cr.Venda = VendaDAO.BuscarPorId(VendaDAO.getLastId());

            Assert.IsTrue(ContaReceberDAO.Persistir(cr));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            ContaReceber cr = ContaReceberDAO.BuscarPorId(ContaReceberDAO.getLastId());
            cr.Valor = 1500.00M;
            cr.Data = Convert.ToDateTime("25/10/2019");
            cr.Venda = VendaDAO.BuscarPorId(1);

            Assert.IsTrue(ContaReceberDAO.Persistir(cr));
        }

        [TestMethod]
        public void BuscarID()
        {
            ContaReceber cr = ContaReceberDAO.BuscarPorId(ContaReceberDAO.getLastId());
            Assert.IsNotNull(cr);
        }



        [TestMethod]
        public void Deletar()
        {
            ContaReceber cr = ContaReceberDAO.BuscarPorId(ContaReceberDAO.getLastId());

            Assert.IsTrue(ContaReceberDAO.Excluir(ContaReceberDAO.getLastId()));
            
        }


        [TestMethod]
        public void Buscar()
        {
            List<ContaReceber> cr = ContaReceberDAO.buscar("1");
            
            Assert.IsTrue(cr.Count > 0);
        }
    }
}
