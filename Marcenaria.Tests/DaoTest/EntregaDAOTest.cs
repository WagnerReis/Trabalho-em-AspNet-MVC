using System;
using System.Collections.Generic;
using Marcenaria.Daos;
using Marcenaria.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marcenaria.Tests.Daos
{
    [TestClass]
    public class EntregaDAOTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Entrega> entregas = EntregaDAO.BuscarTodos();
            Assert.IsTrue(entregas.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            Entrega e = new Entrega();
            e.Data = Convert.ToDateTime("25/05/2019");
            e.Estado = "Rio de Janeiro";
            e.Cidade = "Rio de Janeiro";
            e.Rua = "Nova rua";
            e.Numero = 187;
            e.Bairro = "Novo bairro";
            e.Referencia = "Nova referencia";
            e.Venda = VendaDAO.BuscarPorId(VendaDAO.getLastId());

            Assert.IsTrue(EntregaDAO.Persistir(e));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Entrega e = EntregaDAO.BuscarPorId(EntregaDAO.getLastId());
            e.Data = DateTime.Now;
            e.Estado = "Minas Gerais";
            e.Cidade = "Juiz de Fora";
            e.Rua = "Rua atualizada";
            e.Numero = 781;
            e.Bairro = "Bairro atualizado";
            e.Referencia = "Referencia atualizada";
            e.Venda = VendaDAO.BuscarPorId(2);

            Assert.IsTrue(EntregaDAO.Persistir(e));
        }

        [TestMethod]
        public void BuscarID()
        {
            Entrega e = EntregaDAO.BuscarPorId(EntregaDAO.getLastId());
            Assert.IsNotNull(e);
        }



        [TestMethod]
        public void Deletar()
        {
            Entrega e = EntregaDAO.BuscarPorId(EntregaDAO.getLastId());

            Assert.IsTrue(EntregaDAO.Excluir(EntregaDAO.getLastId()));
            
        }


        [TestMethod]
        public void Buscar()
        {
            List<Entrega> entregas = EntregaDAO.buscar("aa");
            
            Assert.IsTrue(entregas.Count > 0);
        }
    }
}
