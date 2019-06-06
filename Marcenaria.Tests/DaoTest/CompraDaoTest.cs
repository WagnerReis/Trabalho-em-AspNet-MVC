using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.DaoTest
{
    [TestClass]
    public class CompraDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Compra> compras = CompraDao.BuscarTodos();
            Assert.IsTrue(compras.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Buscar()
        {
            List<Compra> compras = CompraDao.buscar("1");

            //            for (int i=0; i < instituicoes.Count; i++)
            //            {
            //                Debug.WriteLine(instituicoes[i].Nome);
            //            }
            Assert.IsTrue(compras.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void BuscarID()
        {
            Compra i = CompraDao.BuscarPorId(CompraDao.getLastId());
            Assert.IsNotNull(i);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirInserir()
        {
            Compra i = new Compra();
            i.Data = Convert.ToDateTime("2001/05/20");
            i.Fornecedor = FornecedorDao.BuscarPorId(FornecedorDao.getLastId());
            i.Funcionario = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId());

            Assert.IsTrue(CompraDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirAtualizar()
        {
            Compra i = CompraDao.BuscarPorId(CompraDao.getLastId());
            i.Data = DateTime.Now;
            i.Fornecedor = FornecedorDao.BuscarPorId(FornecedorDao.getLastId());
            i.Funcionario = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId());

            Assert.IsTrue(CompraDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Deletar()
        {
            Compra i = CompraDao.BuscarPorId(CompraDao.getLastId());

            Assert.IsTrue(CompraDao.Excluir(CompraDao.getLastId()));

            // Assert.IsTrue(InstituicaoDAO.Excluir(i));
        }

        //--------------------------------------------------------


    }
}