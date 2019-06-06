using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Marcenaria.Daos;

namespace Marcenaria.Tests.DaoTest
{
    [TestClass]
    public class ItemCompraDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<ItemCompra> itenscompra = ItemCompraDao.BuscarTodos();
            Assert.IsTrue(itenscompra.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Buscar()
        {
            List<ItemCompra> itenscompra = ItemCompraDao.buscar("6");

            //            for (int i=0; i < instituicoes.Count; i++)
            //            {
            //                Debug.WriteLine(instituicoes[i].Nome);
            //            }
            Assert.IsTrue(itenscompra.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void BuscarID()
        {
            ItemCompra i = ItemCompraDao.BuscarPorId(ItemCompraDao.getLastId());
            Assert.IsNotNull(i);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirInserir()
        {
            ItemCompra i = new ItemCompra();
            i.Quantidade = 11;
            i.Valor = 11;
            i.Produto = ProdutoDAO.BuscarPorId(ProdutoDAO.getLastId());
            i.Compra = CompraDao.BuscarPorId(CompraDao.getLastId());

            Assert.IsTrue(ItemCompraDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirAtualizar()
        {
            ItemCompra i = ItemCompraDao.BuscarPorId(ItemCompraDao.getLastId());
            i.Quantidade = 22;
            i.Valor = 22;
            i.Produto = ProdutoDAO.BuscarPorId(ProdutoDAO.getLastId());
            i.Compra = CompraDao.BuscarPorId(CompraDao.getLastId());

            Assert.IsTrue(ItemCompraDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Deletar()
        {
            ItemCompra i = ItemCompraDao.BuscarPorId(ItemCompraDao.getLastId());

            Assert.IsTrue(ItemCompraDao.Excluir(ItemCompraDao.getLastId()));

            // Assert.IsTrue(InstituicaoDAO.Excluir(i));
        }
        //--------------------------------------------------------


    }
}