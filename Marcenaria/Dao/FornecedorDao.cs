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
    public class FornecedorDao
    {
        private static Fornecedor GetFornecedor(object[] dados)
        {
            Fornecedor fornecedores = new Fornecedor();
            fornecedores.Id = Convert.ToInt32(dados.GetValue(0));
            fornecedores.RazaoSocial = Convert.ToString(dados.GetValue(1));
            fornecedores.Telefone = Convert.ToString(dados.GetValue(2));
            fornecedores.Celular = Convert.ToString(dados.GetValue(3));
            fornecedores.Cnpj = Convert.ToString(dados.GetValue(4));
            fornecedores.Estado = Convert.ToString(dados.GetValue(5));
            fornecedores.Cidade = Convert.ToString(dados.GetValue(6));
            fornecedores.Bairro = Convert.ToString(dados.GetValue(7));
            fornecedores.Rua = Convert.ToString(dados.GetValue(8));
            fornecedores.Numero = Convert.ToInt32(dados.GetValue(9));

            return fornecedores;
        }
        //--------------------------------------------------------
        public static long getLastId()
        {
            return DBUtil.getLastId("fornecedor");
        }
        //--------------------------------------------------------
        public static List<Fornecedor> BuscarTodos()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            try
            {
                SqlCommand cmd = new SqlCommand("select f.id, f.razao_social, f.telefone, f.celular," +
                    " f.cnpj, f.estado, f.cidade, f.bairro," +
                    " f.rua, f.numero from fornecedor as f ", DBUtil.getConnection());

                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fornecedores.Add(GetFornecedor(dt.Rows[i].ItemArray));
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
            return fornecedores;
        }
        //--------------------------------------------------------
        public static Boolean Persistir(Fornecedor fornecedores)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (fornecedores.Id > 0)
                {
                    cmd = new SqlCommand("update fornecedor set razao_social=@razao_social, telefone=@telefone, celular=@celular, " +
                        "  cnpj=@cnpj, estado=@estado," +
                        "cidade=@cidade, bairro=@bairro, rua=@rua, numero=@numero where id=@id", conexao);
                }                                                                                   
                else //insert
                {

                    fornecedores.Id = DBUtil.getNextId("fornecedor");

                    cmd = new SqlCommand("insert into fornecedor(id,razao_social,telefone,celular,cnpj,estado,cidade,bairro,rua,numero) values (@id,@razao_social,@telefone,@celular,@cnpj,@estado,@cidade,@bairro,@rua,@numero)", conexao);
                }                                                             

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", fornecedores.Id);
                cmd.Parameters.AddWithValue("@razao_social", fornecedores.RazaoSocial);
                cmd.Parameters.AddWithValue("@telefone", fornecedores.Telefone);
                cmd.Parameters.AddWithValue("@celular", fornecedores.Celular);
                cmd.Parameters.AddWithValue("@cnpj", fornecedores.Cnpj);
                cmd.Parameters.AddWithValue("@estado", fornecedores.Estado);
                cmd.Parameters.AddWithValue("@cidade", fornecedores.Cidade);
                cmd.Parameters.AddWithValue("@bairro", fornecedores.Bairro);
                cmd.Parameters.AddWithValue("@rua", fornecedores.Rua);
                cmd.Parameters.AddWithValue("@numero", fornecedores.Numero);

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
        public static Fornecedor BuscarPorId(long id)
        {
            Fornecedor fornecedores = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select * from fornecedor as f where f.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                fornecedores = GetFornecedor(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do fornecedor" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return fornecedores;
        }
        //--------------------------------------------------------
        public static Boolean Excluir(Fornecedor fornecedores)
        {
            return Excluir(fornecedores.Id);
        }
        //--------------------------------------------------------
        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from fornecedor where id=@id", conexao);
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
        public static List<Fornecedor> buscar(String Parametro)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select f.id, f.razao_social, f.telefone, f.celular, " +
                    " f.cnpj, f.estado, f.cidade, f.bairro, f.rua, f.numero"+
                    " from fornecedor AS f " +
                    " WHERE f.razao_social LIKE @razao_social " +
                    "  OR f.id = @id " +
                    "  OR f.telefone LIKE @telefone " +
                    "  OR f.celular LIKE @celular " +
                    "  OR f.cnpj LIKE @cnpj " +
                    "  ORDER BY f.razao_social ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@razao_social", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@telefone", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@celular", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@cnpj", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    fornecedores.Add(GetFornecedor(ds.Tables[0].Rows[i].ItemArray));
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
            return fornecedores;
        }
    }
}
