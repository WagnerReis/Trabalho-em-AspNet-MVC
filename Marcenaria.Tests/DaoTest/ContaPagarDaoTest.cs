using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.DaoTest
{
    [TestClass]
    public class ContaPagarDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<ContaPagar> contaspagar = ContaPagarDao.BuscarTodos();
            Assert.IsTrue(contaspagar.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Buscar()
        {
            List<ContaPagar> contaspagar = ContaPagarDao.buscar("0");

            //            for (int i=0; i < instituicoes.Count; i++)
            //            {
            //                Debug.WriteLine(instituicoes[i].Nome);
            //            }
            Assert.IsTrue(contaspagar.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void BuscarID()
        {
            ContaPagar i = ContaPagarDao.BuscarPorId(ContaPagarDao.getLastId());
            Assert.IsNotNull(i);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirInserir()
        {
            ContaPagar i = new ContaPagar();
            i.Valor = 11;
            i.Data = Convert.ToDateTime("2001/05/20");
            i.Compra = CompraDao.BuscarPorId(CompraDao.getLastId());

            Assert.IsTrue(ContaPagarDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirAtualizar()
        {
            ContaPagar i = ContaPagarDao.BuscarPorId(ContaPagarDao.getLastId());
            i.Valor = 22;
            i.Data = DateTime.Now;
            i.Compra = CompraDao.BuscarPorId(CompraDao.getLastId());

            Assert.IsTrue(ContaPagarDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Deletar()
        {
            ContaPagar i = ContaPagarDao.BuscarPorId(ContaPagarDao.getLastId());

            Assert.IsTrue(ContaPagarDao.Excluir(ContaPagarDao.getLastId()));

            // Assert.IsTrue(InstituicaoDAO.Excluir(i));
        }
        //--------------------------------------------------------


    }
}