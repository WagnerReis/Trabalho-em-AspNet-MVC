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
    public static class ContaReceberDAO
    {
        private static ContaReceber getContaReceber(object[] dados)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(dados.GetValue(0));
            contaReceber.Valor = Convert.ToDecimal(dados.GetValue(1));
            contaReceber.Data = System.DBNull.Value.Equals(dados.GetValue(2)) ? DateTime.Now : Convert.ToDateTime(dados.GetValue(2));
            contaReceber.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));

            return contaReceber;
        }

        public static List<ContaReceber> BuscarTodos()
        {
            List<ContaReceber> contaRecebers = new List<ContaReceber>();
            try
            {
                SqlCommand cmd = new SqlCommand("select cr.id, cr.valor, cr.data, cr.id_venda from conta_receber as cr order by cr.id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    contaRecebers.Add(getContaReceber(dt.Rows[i].ItemArray));
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
            return contaRecebers;
        }

        public static long getLastId()
        {
            return DBUtil.getLastId("conta_receber");
        }

        public static Boolean Persistir(ContaReceber contaReceber)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (contaReceber.Id > 0)
                {
                    cmd = new SqlCommand("update conta_receber set valor=@valor, data=@data, id_venda=@id_venda where id=@id", conexao);
                }
                else
                {
                    contaReceber.Id = DBUtil.getNextId("conta_receber");

                    cmd = new SqlCommand("insert into conta_receber(id,valor,data,id_venda) values (@id,@valor,@data,@id_venda)", conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", contaReceber.Id);
                cmd.Parameters.AddWithValue("@valor", contaReceber.Valor);
                cmd.Parameters.AddWithValue("@data", contaReceber.Data);
                cmd.Parameters.AddWithValue("@id_venda", contaReceber.Venda.Id);

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

        public static ContaReceber BuscarPorId(long id)
        {
            ContaReceber contaReceber = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select cr.id, cr.valor, cr.data, cr.id_venda from conta_receber as cr where cr.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                contaReceber = getContaReceber(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id da conta a receber" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return contaReceber;
        }



        public static Boolean Excluir(ContaReceber contaReceber)
        {
            return Excluir(contaReceber.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from conta_receber where id=@id", conexao);
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

        public static List<ContaReceber> buscar(String Parametro)
        {
            List<ContaReceber> contaRecebers = new List<ContaReceber>();

            long Id = 0;
            decimal Valor = 0;
            try
            {
                Id = long.Parse(Parametro);
                Valor = decimal.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select cr.id, cr.valor, cr.data, " +
                    " cr.id_venda from conta_receber AS cr " +
                    " WHERE cr.valor = @valor " +
                    " OR cr.id = @id " +
                    " ORDER BY cr.id ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@valor", Valor);
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    contaRecebers.Add(getContaReceber(ds.Tables[0].Rows[i].ItemArray));
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
            return contaRecebers;
        }
    }
}