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
    public class CompraDao
    {
        private static Compra GetCompra(object[] dados)
        {
            Compra compras = new Compra();
            compras.Id = Convert.ToInt32(dados.GetValue(0));    
            compras.Data = System.DBNull.Value.Equals(dados.GetValue(1)) ? DateTime.Now : Convert.ToDateTime(dados.GetValue(1));
            compras.Fornecedor = FornecedorDao.BuscarPorId(Convert.ToInt32(dados.GetValue(2)));
            compras.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));
            // lista  compra pagar
            // lista  item compra

            return compras;
        }
        //--------------------------------------------------------
        public static long getLastId()
        {
            return DBUtil.getLastId("compra");
        }
        //--------------------------------------------------------
        public static List<Compra> BuscarTodos()
        {
            List<Compra> compras = new List<Compra>();
            try
            {
                SqlCommand cmd = new SqlCommand("select co.id, co.data, co.id_fornecedor, co.id_funcionario from compra as co ", DBUtil.getConnection());

                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    compras.Add(GetCompra(dt.Rows[i].ItemArray));
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
            return compras;
        }
        //--------------------------------------------------------
        public static Boolean Persistir(Compra compras)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (compras.Id > 0)
                {
                    cmd = new SqlCommand("update compra set data=@data, id_fornecedor=@id_fornecedor, id_funcionario=@id_funcionario where id=@id", conexao);
                }                                                                                   
                else 
                {

                    compras.Id = DBUtil.getNextId("compra");

                    cmd = new SqlCommand("insert into compra(id,data,id_fornecedor,id_funcionario) values (@id,@data,@id_fornecedor,@id_funcionario)", conexao);
                }                                                              

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", compras.Id);
                cmd.Parameters.AddWithValue("@data", compras.Data);
                cmd.Parameters.AddWithValue("@id_fornecedor", compras.Fornecedor.Id);
                cmd.Parameters.AddWithValue("@id_funcionario", compras.Funcionario.Id);

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
        public static Compra BuscarPorId(long id)
        {
            Compra compras = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select * from compra as co where co.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                compras = GetCompra(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id da compra" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return compras;
        }
        //--------------------------------------------------------
        public static Boolean Excluir(Compra compras)
        {
            return Excluir(compras.Id);
        }
        //--------------------------------------------------------
        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from compra where id=@id", conexao);
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
        public static List<Compra> buscar(String Parametro)
        {
            List<Compra> compras = new List<Compra>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select co.id, co.data, co.id_fornecedor, co.id_funcionario " +
                    " from compra AS co " +
                    " WHERE co.data LIKE @data " +
                    "  OR co.id = @id " +
                    "  OR co.data LIKE @data " +
                    "  OR co.id_fornecedor = @id_fornecedor " +
                    "  OR co.id_funcionario = @id_funcionario " +
                    "  ORDER BY co.data ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@data", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id_fornecedor", Parametro);
                cmd.Parameters.AddWithValue("@id_funcionario", Parametro);
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    compras.Add(GetCompra(ds.Tables[0].Rows[i].ItemArray));
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
            return compras;
        }
    }
}
