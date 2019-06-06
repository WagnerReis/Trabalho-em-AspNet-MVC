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
    public class ContaPagarDao
    {
        private static ContaPagar GetContaPagar(object[] dados)
        {
            ContaPagar contaspagar = new ContaPagar();
            contaspagar.Id = Convert.ToInt32(dados.GetValue(0));
            contaspagar.Valor = Convert.ToDecimal(dados.GetValue(1));
            contaspagar.Data = System.DBNull.Value.Equals(dados.GetValue(2)) ? DateTime.Now : Convert.ToDateTime(dados.GetValue(2));
            contaspagar.Compra = CompraDao.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));      
            


            return contaspagar;
        }
        //--------------------------------------------------------
        public static long getLastId()
        {
            return DBUtil.getLastId("conta_pagar");
        }
        //--------------------------------------------------------
        public static List<ContaPagar> BuscarTodos()
        {
            List<ContaPagar> contaspagar = new List<ContaPagar>();
            try
            {
                SqlCommand cmd = new SqlCommand("select cp.id, cp.valor, cp.data, cp.id_compra from conta_pagar as cp ", DBUtil.getConnection());

                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    contaspagar.Add(GetContaPagar(dt.Rows[i].ItemArray));
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
            return contaspagar;
        }
        //--------------------------------------------------------
        public static Boolean Persistir(ContaPagar contaspagar)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (contaspagar.Id > 0)
                {
                    cmd = new SqlCommand("update conta_pagar set valor=@valor, data=@data, id_compra=@id_compra where id=@id", conexao);
                }                                                                                  
                else 
                {

                    contaspagar.Id = DBUtil.getNextId("conta_pagar");

                    cmd = new SqlCommand("insert into conta_pagar(id,valor,data, id_compra) values (@id,@valor,@data,@id_compra)", conexao);
                }                                                            

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", contaspagar.Id);
                cmd.Parameters.AddWithValue("@valor", contaspagar.Valor);
                cmd.Parameters.AddWithValue("@data", contaspagar.Data);
                cmd.Parameters.AddWithValue("@id_compra", contaspagar.Compra.Id);

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
        public static ContaPagar BuscarPorId(long id)
        {
            ContaPagar contaspagar = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select * from conta_pagar as cp where cp.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                contaspagar = GetContaPagar(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id da Conta a Pagar" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return contaspagar;
        }
        //--------------------------------------------------------
        public static Boolean Excluir(ContaPagar contaspagar)
        {
            return Excluir(contaspagar.Id);
        }
        //--------------------------------------------------------
        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from conta_pagar where id=@id", conexao);
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
        public static List<ContaPagar> buscar(String Parametro)
        {
            List<ContaPagar> contaspagar = new List<ContaPagar>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select cp.id, cp.valor, cp.data, cp.id_compra " +
                    " from conta_pagar AS cp " +
                    " WHERE cp.data LIKE @data " +
                    "  OR cp.id = @id " +
                    "  OR cp.valor = @valor " +
                    "  OR cp.id_compra = @id_compra " +
                    "  ORDER BY cp.data ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@valor", Parametro);
                cmd.Parameters.AddWithValue("@data", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id_compra", Parametro);
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    contaspagar.Add(GetContaPagar(ds.Tables[0].Rows[i].ItemArray));
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
            return contaspagar;
        }
    }
}
