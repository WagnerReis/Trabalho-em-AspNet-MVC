using Marcenaria.Models;
using scaweb.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Marcenaria.Daos
{
    public static class ItemVendaDAO
    {
        private static ItemVenda getItemVenda(object[] dados)
        {
            ItemVenda itemVenda = new ItemVenda();
            itemVenda.Id = Convert.ToInt32(dados.GetValue(0));
            itemVenda.Quantidade = Convert.ToInt16(dados.GetValue(1));
            itemVenda.Valor = Convert.ToDecimal(dados.GetValue(2));
            itemVenda.Produto = ProdutoDAO.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));
            itemVenda.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(dados.GetValue(4)));

            return itemVenda;
        }

        public static List<ItemVenda> BuscarTodos()
        {
            List<ItemVenda> itemVendas = new List<ItemVenda>();
            try
            {
                SqlCommand cmd = new SqlCommand("select iv.id, iv.quantidade, iv.valor, iv.id_produto, iv.id_venda from item_venda as iv order by iv.id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    itemVendas.Add(getItemVenda(dt.Rows[i].ItemArray));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return itemVendas;
        }

        public static long getLastId()
        {
            return DBUtil.getLastId("item_venda");
        }

        public static Boolean Persistir(ItemVenda itemVenda)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (itemVenda.Id > 0)//update
                {
                    cmd = new SqlCommand("update item_venda set quantidade=@quantidade, valor=@valor, id_produto=@id_produto, id_venda=@id_venda where id=@id", conexao);
                }
                else //insert
                {
                    itemVenda.Id = DBUtil.getNextId("item_venda");

                    cmd = new SqlCommand("insert into item_venda(id,quantidade,valor,id_produto,id_venda) values (@id,@quantidade,@valor,@id_produto,@id_venda)", conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", itemVenda.Id);
                cmd.Parameters.AddWithValue("@quantidade", itemVenda.Quantidade);
                cmd.Parameters.AddWithValue("@valor", itemVenda.Valor);
                cmd.Parameters.AddWithValue("@id_produto", itemVenda.Produto.Id);
                cmd.Parameters.AddWithValue("@id_venda", itemVenda.Venda.Id);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Persistencia - Erro ao conectar ao banco de dados" + e.Message);
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }

        public static ItemVenda BuscarPorId(long id)
        {
            ItemVenda itemVenda = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select iv.id, iv.quantidade, iv.valor, iv.id_produto, iv.id_venda from item_venda as iv where iv.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                itemVenda = getItemVenda(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do item venda" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return itemVenda;
        }

        public static Boolean Excluir(ItemVenda itemVenda)
        {
            return Excluir(itemVenda.Id);
        }

        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from item_venda where id=@id", conexao);
                conexao.Open();
                cmd.Parameters.AddWithValue("@id", id);
                total = cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exclusão - Erro ao conectar ao banco de dados" + e.Message);
            }
            finally
            {
                conexao.Close();
            }

            return total > 0;
        }

        public static List<ItemVenda> buscar(String Parametro)
        {
            List<ItemVenda> itemVendas = new List<ItemVenda>();

            long Id = 0;
            int Quantidade = 0;
            try
            {
                Id = long.Parse(Parametro);
                Quantidade = int.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select iv.id, iv.quantidade, " +
                    " iv.valor, iv.id_produto, iv.id_venda from item_venda AS iv " +
                    " WHERE iv.id = @id " +
                    " OR iv.quantidade = @quantidade " +
                    " ORDER BY iv.id ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@quantidade", Quantidade);
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    itemVendas.Add(getItemVenda(ds.Tables[0].Rows[i].ItemArray));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
            finally
            {
                DBUtil.closeConnection();
            }
            return itemVendas;
        }
    }
}