using System;
using System.Collections.Generic;
using Marcenaria.Dao;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.DaoTest
{
    [TestClass]
    public class UsuarioDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Usuario> usuarios = UsuarioDao.BuscarTodos();
            Assert.IsTrue(usuarios.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Buscar()
        {
            List<Usuario> usuarios = UsuarioDao.buscar("n");

            //            for (int i=0; i < instituicoes.Count; i++)
            //            {
            //                Debug.WriteLine(instituicoes[i].Nome);
            //            }
            Assert.IsTrue(usuarios.Count > 0);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void BuscarID()
        {
            Usuario i = UsuarioDao.BuscarPorId(UsuarioDao.getLastId());
            Assert.IsNotNull(i);
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirInserir()
        {
            Usuario i = new Usuario();
            i.Login = "a@a.com";
            i.Senha = "a";
            i.Funcionario = FuncionarioDao.BuscarPorId(FuncionarioDao.getLastId()); ;

            Assert.IsTrue(UsuarioDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void PersistirAtualizar()
        {
            Usuario i = UsuarioDao.BuscarPorId(UsuarioDao.getLastId());
            i.Login = "aaaa";
            i.Senha = "aaa";

            Assert.IsTrue(UsuarioDao.Persistir(i));
        }
        //--------------------------------------------------------
        [TestMethod]
        public void Deletar()
        {
            Usuario i = UsuarioDao.BuscarPorId(UsuarioDao.getLastId());

            Assert.IsTrue(UsuarioDao.Excluir(UsuarioDao.getLastId()));

            // Assert.IsTrue(InstituicaoDAO.Excluir(i));
        }
        //--------------------------------------------------------


    }
}