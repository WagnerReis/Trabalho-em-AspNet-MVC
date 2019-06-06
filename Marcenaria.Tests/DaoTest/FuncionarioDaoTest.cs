using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.DaoTest
{
    [TestClass]
    public class FuncionarioDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Funcionario> funcionarios = FuncionarioDao.BuscarTodos();
            Assert.IsTrue(funcionarios.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Buscar()
        {
            List<Funcionario> instituicoes = FuncionarioDao.buscar("N");

            //            for (int i=0; i < instituicoes.Count; i++)
            //            {
            //                Debug.WriteLine(instituicoes[i].Nome);
            //            }
            Assert.IsTrue(instituicoes.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void BuscarID()
        {
            Funcionario i = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId());
            Assert.IsNotNull(i);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirInserir()
        {
            Funcionario i = new Funcionario();
            i.Nome = "NNNN";
            i.Tipo = true;
            i.Cpf = "NNNN";
            i.Cnpj = "NNNN";
            i.Telefone = "111";
            i.Celular = "111";
            i.Estado = "NNNN";
            i.Cidade = "NNNN";
            i.Bairro = "NNNN";
            i.Rua = "NNNN";
            i.Numero = 1;
            i.Usuario = UsuarioDao.BuscarPorId(UsuarioDao.getLastId());

            Assert.IsTrue(FuncionarioDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirAtualizar()
        {
            Funcionario i = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId());
            i.Nome = "NNNN";
            i.Tipo = true;
            i.Cpf = "NNNN";
            i.Cnpj = "NNNN";
            i.Telefone = "111";
            i.Celular = "111";
            i.Estado = "NNNN";
            i.Cidade = "NNNN";
            i.Bairro = "NNNN";
            i.Rua = "NNNN";
            i.Numero = 1;
            i.Usuario = UsuarioDao.BuscarPorId(UsuarioDao.getLastId());

            Assert.IsTrue(FuncionarioDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Deletar()
        {
            Funcionario i = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId());

            Assert.IsTrue(FuncionarioDao.Excluir(FuncionarioDao.getLastId()));

            // Assert.IsTrue(InstituicaoDAO.Excluir(i));
        }
        //--------------------------------------------------------


    }
}