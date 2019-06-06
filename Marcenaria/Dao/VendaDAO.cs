
using Marcenaria.Dao;
using Marcenaria.Models;
using scaweb.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Marcenaria.Daos
{
    public static class VendaDAO
    {
        private static Venda getVenda(object[] dados)
        {
            Venda venda = new Venda();
            venda.Id = Convert.ToInt32(dados.GetValue(0));
            venda.Data = System.DBNull.Value.Equals(dados.GetValue(1)) ? DateTime.Now : Convert.ToDateTime(dados.GetValue(1));
            venda.Cliente = ClienteDAO.BuscarPorId(Convert.ToInt32(dados.GetValue(2)));
            venda.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));

            return venda;
        }

        public static List<Venda> BuscarTodos()
        {
            List<Venda> vendas = new List<Venda>();
            try
            {
                SqlCommand cmd = new SqlCommand("select v.id, v.data, v.id_cliente, v.id_funcionario from venda as v order by v.id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vendas.Add(getVenda(dt.Rows[i].ItemArray));
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
            return vendas;
        }

        public static long getLastId()
        {
            return DBUtil.getLastId("venda");
        }

        public static Boolean Persistir(Venda venda)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (venda.Id > 0)
                {
                    cmd = new SqlCommand("update venda set data=@data, id_cliente=@id_cliente, id_funcionario=@id_funcionario where id=@id", conexao);
                }
                else
                {
                    venda.Id = DBUtil.getNextId("venda");

                    cmd = new SqlCommand("insert into venda(id,data,id_cliente,id_funcionario) values (@id,@data,@id_cliente,@id_funcionario)", conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", venda.Id);
                cmd.Parameters.AddWithValue("@data", venda.Data);
                cmd.Parameters.AddWithValue("@id_cliente", venda.Cliente.Id);
                cmd.Parameters.AddWithValue("@id_funcionario", venda.Funcionario.Id);

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

        public static Venda BuscarPorId(long id)
        {
            Venda venda = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select v.id, v.data, v.id_cliente, v.id_funcionario from venda as v where v.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                venda = getVenda(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id da venda" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return venda;
        }

        public static Boolean Excluir(Venda venda)
        {
            return Excluir(venda.Id);
        }

        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from venda where id=@id", conexao);
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

        public static List<Venda> buscar(String Parametro)
        {
            List<Venda> vendas = new List<Venda>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select v.id, v.data, v.id_cliente, " +
                    " v.id_funcionario from venda AS v " +
                    " WHERE v.id = @id " +
                    " ORDER BY v.id ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    vendas.Add(getVenda(ds.Tables[0].Rows[i].ItemArray));
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
            return vendas;
        }
    }
}