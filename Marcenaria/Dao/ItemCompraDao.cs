using Marcenaria.Models;
using scaweb.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Marcenaria.Dao
{
    public class ItemCompraDao
    {
        private static ItemCompra GetItemCompra(object[] dados)
        {
            ItemCompra itenscompra = new ItemCompra();
            itenscompra.Id = Convert.ToInt32(dados.GetValue(0));    
            itenscompra.Quantidade = Convert.ToInt32(dados.GetValue(1));
            itenscompra.Valor = Convert.ToDecimal(dados.GetValue(2));
            //itenscompra.Produto = ProdutoDao.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));
            itenscompra.Compra = CompraDao.BuscarPorId(Convert.ToInt32(dados.GetValue(4)));     
            


            return itenscompra;
        }
        //--------------------------------------------------------
        public static long getLastId()
        {
            return DBUtil.getLastId("item_compra");
        }
        //--------------------------------------------------------
        public static List<ItemCompra> BuscarTodos()
        {
            List<ItemCompra> revisoes = new List<ItemCompra>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from item_compra as ic ", DBUtil.getConnection());

                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    revisoes.Add(GetItemCompra(dt.Rows[i].ItemArray));
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
            return revisoes;
        }
        //--------------------------------------------------------
        public static Boolean Persistir(ItemCompra itenscompra)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (itenscompra.Id > 0)//update
                {
                    cmd = new SqlCommand("update item_compra set quantidade=@quantidade, valor=@valor, id_produto=@id_produto, id_compra=@id_compra where id=@id", conexao);
                }                                                                                   
                else //insert
                {

                    itenscompra.Id = DBUtil.getNextId("item_compra");

                    cmd = new SqlCommand("insert into item_compra(id,quantidade,valor,id_produto,id_compra) values (@id,@quantidade,@valor,@id_produto,@id_compra)", conexao);
                }                                                              

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", itenscompra.Id);
                cmd.Parameters.AddWithValue("@quantidade", itenscompra.Quantidade);
                cmd.Parameters.AddWithValue("@valor", itenscompra.Valor);
                cmd.Parameters.AddWithValue("@id_produto", itenscompra.Produto.Id);
                cmd.Parameters.AddWithValue("@id_compra", itenscompra.Compra.Id);

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
        //--------------------------------------------------------
        public static ItemCompra BuscarPorId(long id)
        {
            ItemCompra itenscompra = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select * from item_compra as ic where ic.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                itenscompra = GetItemCompra(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do item compra" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return itenscompra;
        }
        //--------------------------------------------------------
        public static Boolean Excluir(ItemCompra itenscompra)
        {
            return Excluir(itenscompra.Id);
        }
        //--------------------------------------------------------
        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from item_compra where id=@id", conexao);
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
        //--------------------------------------------------------
        public static List<ItemCompra> buscar(String Parametro)
        {
            List<ItemCompra> itenscompra = new List<ItemCompra>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select ic.id, ic.quantidade," +
                    " ic.valor, ic.id_produto, ic.id_compra " +
                    " from item_compra AS ic " +
                    " WHERE ic.valor = @valor " +
                    "  OR ic.id = @id " +
                    "  OR ic.quantidade = @quantidade " +
                    "  OR ic.id_produto = @id_produto " +
                    "  OR ic.id_compra = @id_compra " +
                    "  ORDER BY ic.quantidade ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@quantidade", Parametro);
                cmd.Parameters.AddWithValue("@valor",  Parametro);
                cmd.Parameters.AddWithValue("@id_produto",  Parametro );
                cmd.Parameters.AddWithValue("@id_compra", Parametro);
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    itenscompra.Add(GetItemCompra(ds.Tables[0].Rows[i].ItemArray));
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
            return itenscompra;
        }
    }
}
