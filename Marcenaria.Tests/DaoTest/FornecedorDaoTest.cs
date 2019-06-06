using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.DaoTest
{
    [TestClass]
    public class FornecedorDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Fornecedor> fornecedores = FornecedorDao.BuscarTodos();
            Assert.IsTrue(fornecedores.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Buscar()
        {
            List<Fornecedor> fornecedores = FornecedorDao.buscar("a");

            //            for (int i=0; i < instituicoes.Count; i++)
            //            {
            //                Debug.WriteLine(instituicoes[i].Nome);
            //            }
            Assert.IsTrue(fornecedores.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void BuscarID()
        {
            Fornecedor i = FornecedorDao.BuscarPorId(FornecedorDao.getLastId());
            Assert.IsNotNull(i);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirInserir()
        {
            Fornecedor i = new Fornecedor();
            i.RazaoSocial = "AAAA";
            i.Telefone = "1111";
            i.Celular = "111";
            i.Cnpj = "aa";
            i.Estado = "aa";
            i.Cidade = "aa";
            i.Bairro = "aa";
            i.Rua = "aa";
            i.Numero = 1;

            Assert.IsTrue(FornecedorDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirAtualizar()
        {
            Fornecedor i = FornecedorDao.BuscarPorId(FornecedorDao.getLastId());
            i.RazaoSocial = "BBB";
            i.Telefone = "222";
            i.Celular = "222";
            i.Cnpj = "bbb";
            i.Estado = "bbb";
            i.Cidade = "bb";
            i.Bairro = "bb";
            i.Rua = "bb";
            i.Numero = 2;

            Assert.IsTrue(FornecedorDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Deletar()
        {
            Fornecedor i = FornecedorDao.BuscarPorId(FornecedorDao.getLastId());

            Assert.IsTrue(FornecedorDao.Excluir(FornecedorDao.getLastId()));

            // Assert.IsTrue(InstituicaoDAO.Excluir(i));
        }
        //--------------------------------------------------------
    }
}