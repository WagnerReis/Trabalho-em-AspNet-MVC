using System;
using System.Collections.Generic;
using Marcenaria.Daos;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Marcenaria.Tests.Daos
{
    [TestClass]
    public class ProdutoDAOTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Produto> produtos = ProdutoDAO.BuscarTodos();
            Assert.IsTrue(produtos.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            Produto produto = new Produto();
            produto.Nome = "Novo produto!";
            produto.Valor = 1500;
            produto.Descricao = "Um novo produto adicionado!";

            Assert.IsTrue(ProdutoDAO.Persistir(produto));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Produto p = ProdutoDAO.BuscarPorId(ProdutoDAO.getLastId());
            p.Nome = "Produto atualizado!";
            p.Valor = 2000;
            p.Descricao = "Produto atualizado com sucesso!";

            Assert.IsTrue(ProdutoDAO.Persistir(p));
        }

        [TestMethod]
        public void BuscarID()
        {
            Produto p = ProdutoDAO.BuscarPorId(ProdutoDAO.getLastId());
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void Deletar()
        {
            Produto p = ProdutoDAO.BuscarPorId(ProdutoDAO.getLastId());

            Assert.IsTrue(ProdutoDAO.Excluir(ProdutoDAO.getLastId()));
        }

        [TestMethod]
        public void Buscar()
        {
            List<Produto> produtos = ProdutoDAO.Buscar("aa");
            Assert.IsTrue(produtos.Count > 0);
        }
    }
}
