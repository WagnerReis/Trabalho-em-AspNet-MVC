
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
    public static class EntregaDAO
    {
        private static Entrega getEntrega(object[] dados)
        {
            Entrega entrega = new Entrega();
            entrega.Id = Convert.ToInt32(dados.GetValue(0));
            entrega.Data = System.DBNull.Value.Equals(dados.GetValue(1)) ? DateTime.Now : Convert.ToDateTime(dados.GetValue(1));
            entrega.Estado = Convert.ToString(dados.GetValue(2));
            entrega.Cidade = Convert.ToString(dados.GetValue(3));
            entrega.Rua = Convert.ToString(dados.GetValue(4));
            entrega.Numero = Convert.ToInt16(dados.GetValue(5));
            entrega.Bairro = Convert.ToString(dados.GetValue(6));
            entrega.Referencia = Convert.ToString(dados.GetValue(7));
            entrega.Venda = VendaDAO.BuscarPorId(Convert.ToInt32(dados.GetValue(8)));

            return entrega;
        }

        public static List<Entrega> BuscarTodos()
        {
            List<Entrega> entregas = new List<Entrega>();
            try
            {
                SqlCommand cmd = new SqlCommand("select e.id, e.data, e.estado, e.cidade, e.rua, e.numero, e.bairro, e.referencia, e.id_venda from entrega as e order by e.id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    entregas.Add(getEntrega(dt.Rows[i].ItemArray));
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
            return entregas;
        }

        public static long getLastId()
        {
            return DBUtil.getLastId("entrega");
        }

        public static Boolean Persistir(Entrega entrega)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (entrega.Id > 0)
                {
                    cmd = new SqlCommand("update entrega set data=@data, estado=@estado, cidade=@cidade, rua=@rua, numero=@numero, bairro=@bairro, referencia=@referencia, id_venda=@id_venda where id=@id", conexao);
                }
                else
                {
                    entrega.Id = DBUtil.getNextId("entrega");

                    cmd = new SqlCommand("insert into entrega(id,data,estado,cidade,rua,numero,bairro,referencia,id_venda) values (@id,@data,@estado,@cidade,@rua,@numero,@bairro,@referencia,@id_venda)", conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", entrega.Id);
                cmd.Parameters.AddWithValue("@data", entrega.Data);
                cmd.Parameters.AddWithValue("@estado", entrega.Estado);
                cmd.Parameters.AddWithValue("@cidade", entrega.Cidade);
                cmd.Parameters.AddWithValue("@rua", entrega.Rua);
                cmd.Parameters.AddWithValue("@numero", entrega.Numero);
                cmd.Parameters.AddWithValue("@bairro", entrega.Bairro);
                cmd.Parameters.AddWithValue("@referencia", entrega.Referencia);
                cmd.Parameters.AddWithValue("@id_venda", entrega.Venda.Id);

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

        public static Entrega BuscarPorId(long id)
        {
            Entrega entrega = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select e.id, e.data, e.estado, e.cidade, e.rua, e.numero, e.bairro, e.referencia, e.id_venda from entrega as e where e.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                entrega = getEntrega(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id da entrega" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return entrega;
        }



        public static Boolean Excluir(Entrega entrega)
        {
            return Excluir(entrega.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from entrega where id=@id", conexao);
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

        public static List<Entrega> buscar(String Parametro)
        {
            List<Entrega> entregas = new List<Entrega>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select e.id, e.data, e.estado, " +
                    " e.cidade, e.rua, e.numero, e.bairro, e.referencia, e.id_venda from entrega AS e " +
                    " WHERE e.estado LIKE @estado " +
                    " OR e.id = @id " +
                    " OR e.referencia LIKE @referencia " +
                    " ORDER BY e.id ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@estado", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@referencia", "%" + Parametro + "%");

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    entregas.Add(getEntrega(ds.Tables[0].Rows[i].ItemArray));
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
            return entregas;
        }
    }
}