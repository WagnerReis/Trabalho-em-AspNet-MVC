
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
    public static class ClienteDAO
    {
        private static Cliente getCliente(object[] dados)
        {
            Cliente cliente = new Cliente();
            cliente.Id = Convert.ToInt32(dados.GetValue(0));
            cliente.Nome = Convert.ToString(dados.GetValue(1));
            cliente.Telefone = Convert.ToString(dados.GetValue(2));
            cliente.Celular = Convert.ToString(dados.GetValue(3));
            cliente.Tipo = System.DBNull.Value.Equals(dados.GetValue(4)) ? true : Convert.ToBoolean(dados.GetValue(4));
            cliente.Cpf = Convert.ToString(dados.GetValue(5));
            cliente.Cnpj = Convert.ToString(dados.GetValue(6));
            cliente.Estado = Convert.ToString(dados.GetValue(7));
            cliente.Cidade = Convert.ToString(dados.GetValue(8));
            cliente.Bairro = Convert.ToString(dados.GetValue(9));
            cliente.Rua = Convert.ToString(dados.GetValue(10));
            cliente.Numero = Convert.ToInt16(dados.GetValue(11));

            return cliente;
        }

        public static List<Cliente> BuscarTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                SqlCommand cmd = new SqlCommand("select c.id, c.nome, c.telefone, c.celular, c.tipo, c.cpf, c.cnpj, c.estado, c.cidade, c.bairro, c.rua, c.numero from cliente as c order by c.id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clientes.Add(getCliente(dt.Rows[i].ItemArray));
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
            return clientes;
        }

        public static long getLastId()
        {
            return DBUtil.getLastId("cliente");
        }

        public static Boolean Persistir(Cliente cliente)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (cliente.Id > 0)
                {
                    cmd = new SqlCommand("update cliente set nome=@nome, telefone=@telefone, celular=@celular, tipo=@tipo, cpf=@cpf, cnpj=@cnpj, estado=@estado, cidade=@cidade, bairro=@bairro, rua=@rua, numero=@numero where id=@id", conexao);
                }
                else
                {
                    cliente.Id = DBUtil.getNextId("cliente");

                    cmd = new SqlCommand("insert into cliente(id,nome,telefone,celular,tipo,cpf,cnpj,estado,cidade,bairro,rua,numero) values (@id,@nome,@telefone,@celular,@tipo,@cpf,@cnpj,@estado,@cidade,@bairro,@rua,@numero)", conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@celular", cliente.Celular);
                cmd.Parameters.AddWithValue("@tipo", cliente.Tipo);
                cmd.Parameters.AddWithValue("@cpf", cliente.Cpf);
                cmd.Parameters.AddWithValue("@cnpj", cliente.Cnpj);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);
                cmd.Parameters.AddWithValue("@cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@rua", cliente.Rua);
                cmd.Parameters.AddWithValue("@numero", cliente.Numero);

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

        public static Cliente BuscarPorId(long id)
        {
            Cliente cliente = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select c.id, c.nome, c.telefone, c.celular, c.tipo, c.cpf, c.cnpj, c.estado, c.cidade, c.bairro, c.rua, c.numero from cliente as c where c.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                cliente = getCliente(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do cliente" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return cliente;
        }



        public static Boolean Excluir(Cliente cliente)
        {
            return Excluir(cliente.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from cliente where id=@id", conexao);
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

        public static List<Cliente> buscar(String Parametro)
        {
            List<Cliente> clientes = new List<Cliente>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select c.id, c.nome, c.telefone, " +
                    " c.celular, c.tipo, c.cpf, c.cnpj, c.estado, c.cidade, c.bairro, c.rua, c.numero from cliente AS c " +
                    " WHERE c.nome LIKE @nome " +
                    " OR c.id = @id " +
                    " OR c.cpf LIKE @cpf " +
                    " OR c.cnpj LIKE @cnpj " +
                    " OR c.estado LIKE @estado " +
                    " ORDER BY c.id ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@nome", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@cpf", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@cnpj", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@estado", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clientes.Add(getCliente(ds.Tables[0].Rows[i].ItemArray));
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
            return clientes;
        }
    }
}