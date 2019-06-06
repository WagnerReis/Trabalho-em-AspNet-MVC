using System;
using System.Collections.Generic;
using Marcenaria.Daos;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.Daos
{
    [TestClass]
    public class ItemVendaDAOTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<ItemVenda> instituicoes = ItemVendaDAO.BuscarTodos();
            Assert.IsTrue(instituicoes.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            ItemVenda iv = new ItemVenda();
            iv.Quantidade = 10;
            iv.Valor = 50.0M;
            iv.Produto = ProdutoDAO.BuscarPorId(ProdutoDAO.getLastId());
            iv.Venda = VendaDAO.BuscarPorId(VendaDAO.getLastId());

            Assert.IsTrue(ItemVendaDAO.Persistir(iv));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            ItemVenda iv = ItemVendaDAO.BuscarPorId(ItemVendaDAO.getLastId());
            iv.Quantidade = 15;
            iv.Valor = 25500.50M;
            iv.Produto = ProdutoDAO.BuscarPorId(2);
            iv.Venda = VendaDAO.BuscarPorId(3);

            Assert.IsTrue(ItemVendaDAO.Persistir(iv));
        }

        [TestMethod]
        public void BuscarID()
        {
            ItemVenda iv = ItemVendaDAO.BuscarPorId(ItemVendaDAO.getLastId());
            Assert.IsNotNull(iv);
        }



        [TestMethod]
        public void Deletar()
        {
            ItemVenda iv = ItemVendaDAO.BuscarPorId(ItemVendaDAO.getLastId());

            Assert.IsTrue(ItemVendaDAO.Excluir(ItemVendaDAO.getLastId()));
            
        }


        [TestMethod]
        public void Buscar()
        {
            List<ItemVenda> itemVendas = ItemVendaDAO.buscar("1");
            
            Assert.IsTrue(itemVendas.Count > 0);
        }
    }
}
