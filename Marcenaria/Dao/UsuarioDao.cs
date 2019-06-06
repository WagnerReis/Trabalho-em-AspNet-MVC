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
    public class UsuarioDao
    {
        private static Usuario GetUsuario(object[] dados)
        {
            Usuario usuarios = new Usuario();
            usuarios.Id = Convert.ToInt32(dados.GetValue(0));
            usuarios.Login = Convert.ToString(dados.GetValue(1));
            usuarios.Senha = Convert.ToString(dados.GetValue(2));
           // usuarios.Funcionario = FuncionarioDao.BuscarPorId(Convert.ToInt32(dados.GetValue(3)));    
            


            return usuarios;
        }
        //--------------------------------------------------------
        public static long getLastId()
        {
            return DBUtil.getLastId("usuario");
        }
        //--------------------------------------------------------
        public static List<Usuario> BuscarTodos()
        {
            List<Usuario> revisoes = new List<Usuario>();
            try
            {
                SqlCommand cmd = new SqlCommand("select u.id, u.login, u.senha from usuario as u ", DBUtil.getConnection());

                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    revisoes.Add(GetUsuario(dt.Rows[i].ItemArray));
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
        public static Boolean Persistir(Usuario usuarios)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (usuarios.Id > 0)
                {
                    cmd = new SqlCommand("update usuario set login=@login, senha=@senha where id=@id", conexao);
                }                                                                                   
                else 
                {

                    usuarios.Id = DBUtil.getNextId("usuario");

                    cmd = new SqlCommand("insert into usuario(id,login,senha) values (@id,@login,@senha)", conexao);
                }                                                         

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", usuarios.Id);
                cmd.Parameters.AddWithValue("@login", usuarios.Login);
                cmd.Parameters.AddWithValue("@senha", usuarios.Senha);
                

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
        public static Usuario BuscarPorId(long id)
        {
            Usuario usuarios = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select * from usuario as u where u.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                usuarios = GetUsuario(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do usuario" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return usuarios;
        }
        //--------------------------------------------------------
        public static Boolean Excluir(Usuario usuarios)
        {
            return Excluir(usuarios.Id);
        }
        //--------------------------------------------------------
        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from usuario where id=@id", conexao);
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
        public static List<Usuario> buscar(String Parametro)
        {
            List<Usuario> usuarios = new List<Usuario>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select u.id, u.login, u.senha " +
                    " from usuario AS u " +
                    " WHERE u.login LIKE @login " +
                    "  OR u.id = @id " +
                    "  ORDER BY u.login ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@login", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@senha", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    usuarios.Add(GetUsuario(ds.Tables[0].Rows[i].ItemArray));
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
            return usuarios;
        }
    }
}
