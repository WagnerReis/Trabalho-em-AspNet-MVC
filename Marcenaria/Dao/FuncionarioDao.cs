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
    public class FuncionarioDao
    {
        private static Funcionario GetFuncionario(object[] dados)
        {
            Funcionario funcionarios = new Funcionario();
            funcionarios.Id = Convert.ToInt32(dados.GetValue(0));   
            funcionarios.Nome = Convert.ToString(dados.GetValue(1)); 
            funcionarios.Tipo = System.DBNull.Value.Equals(dados.GetValue(2)) ? true : Convert.ToBoolean(dados.GetValue(2));
            funcionarios.Cpf = Convert.ToString(dados.GetValue(3));
            funcionarios.Cnpj = Convert.ToString(dados.GetValue(4));
            funcionarios.Telefone = Convert.ToString(dados.GetValue(5));
            funcionarios.Celular = Convert.ToString(dados.GetValue(6));
            funcionarios.Estado = Convert.ToString(dados.GetValue(7));
            funcionarios.Cidade = Convert.ToString(dados.GetValue(8));
            funcionarios.Bairro = Convert.ToString(dados.GetValue(9));
            funcionarios.Rua = Convert.ToString(dados.GetValue(10));
            funcionarios.Numero = Convert.ToInt32(dados.GetValue(11));
            funcionarios.Usuario = UsuarioDao.BuscarPorId(Convert.ToInt32(dados.GetValue(12)));      
            


            return funcionarios;
        }
        //--------------------------------------------------------
        public static long getLastId()
        {
            return DBUtil.getLastId("funcionario");
        }
        //--------------------------------------------------------
        public static List<Funcionario> BuscarTodos()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            try
            {
                SqlCommand cmd = new SqlCommand("select fu.id, fu.nome, fu.tipo, fu.cpf, fu.cnpj, fu.telefone, fu.celular," +
                    " fu.estado, fu.cidade, fu.bairro, fu.rua, fu.numero, fu.id_usuario " +
                    " from funcionario as fu ", DBUtil.getConnection());

                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    funcionarios.Add(GetFuncionario(dt.Rows[i].ItemArray));
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
            return funcionarios;
        }
        //--------------------------------------------------------
        public static Boolean Persistir(Funcionario funcionarios)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (funcionarios.Id > 0)//update
                {
                    cmd = new SqlCommand("update funcionario set nome=@nome, tipo=@tipo, cpf=@cpf, cnpj=@cnpj, telefone=@telefone, celular=@celular, estado=@estado, cidade=@cidade, bairro=@bairro, rua=@rua, numero=@numero, id_usuario=@id_usuario where id=@id", conexao);
                }                                                                                  
                else //insert
                {

                    funcionarios.Id = DBUtil.getNextId("funcionario");

                    cmd = new SqlCommand("insert into funcionario(id,nome,tipo,cpf,cnpj,telefone,celular,estado,cidade,bairro,rua,numero,id_usuario) values (@id,@nome,@tipo,@cpf,@cnpj,@telefone,@celular,@estado,@cidade,@bairro,@rua,@numero,@id_usuario)", conexao);
                }                                                             

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", funcionarios.Id);
                cmd.Parameters.AddWithValue("@nome", funcionarios.Nome);
                cmd.Parameters.AddWithValue("@tipo", funcionarios.Tipo);
                cmd.Parameters.AddWithValue("@cpf", funcionarios.Cpf);
                cmd.Parameters.AddWithValue("@cnpj", funcionarios.Cnpj);
                cmd.Parameters.AddWithValue("@telefone", funcionarios.Telefone);
                cmd.Parameters.AddWithValue("@celular", funcionarios.Celular);
                cmd.Parameters.AddWithValue("@estado", funcionarios.Estado);
                cmd.Parameters.AddWithValue("@cidade", funcionarios.Cidade);
                cmd.Parameters.AddWithValue("@bairro", funcionarios.Bairro);
                cmd.Parameters.AddWithValue("@rua", funcionarios.Rua);
                cmd.Parameters.AddWithValue("@numero", funcionarios.Numero);
                cmd.Parameters.AddWithValue("@id_usuario", funcionarios.Usuario.Id);

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
        public static Funcionario BuscarPorId(long id)
        {
            Funcionario funcionarios = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select * from funcionario as fu where fu.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                funcionarios = GetFuncionario(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do funcionario" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return funcionarios;
        }
        //--------------------------------------------------------
        public static Boolean Excluir(Funcionario funcionarios)
        {
            return Excluir(funcionarios.Id);
        }
        //--------------------------------------------------------
        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from funcionario where id=@id", conexao);
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
        public static List<Funcionario> buscar(String Parametro)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select fu.id, fu.nome, fu.tipo, fu.cpf, fu.cnpj," +
                    " fu.telefone, fu.celular, fu.estado, fu.cidade, fu.bairro, fu.rua, fu.numero, fu.id_usuario " +
                    " from funcionario AS fu " +
                    " WHERE fu.nome LIKE @nome " +
                    "  OR fu.id = @id " +
                    "  OR fu.telefone LIKE @telefone " +
                    "  OR fu.celular LIKE @celular " +
                    "  OR fu.estado LIKE @estado " +
                    "  OR fu.cidade LIKE @cidade " +
                    "  OR fu.bairro LIKE @bairro " +
                    "  OR fu.rua LIKE @rua " +
                    "  ORDER BY fu.nome ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@nome", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@telefone", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@celular", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@estado", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@cidade", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@bairro", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@rua", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    funcionarios.Add(GetFuncionario(ds.Tables[0].Rows[i].ItemArray));
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
            return funcionarios;
        }
    }
}
