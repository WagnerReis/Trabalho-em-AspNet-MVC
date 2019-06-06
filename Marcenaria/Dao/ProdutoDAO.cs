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
    public static class ProdutoDAO
    {
        private static Produto getProduto(object[] dados)
        {
            Produto produto = new Produto();
            produto.Id = Convert.ToInt32(dados.GetValue(0));
            produto.Nome = Convert.ToString(dados.GetValue(1));
            produto.Valor = Convert.ToDecimal(dados.GetValue(2));
            produto.Descricao = Convert.ToString(dados.GetValue(3));

            return produto;
        }

        public static List<Produto> BuscarTodos()
        {
            List<Produto> produtos = new List<Produto>();
            try
            {
                SqlCommand cmd = new SqlCommand("select p.id, p.nome, p.valor, p.descricao from produto as p order by p.id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable ds = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    produtos.Add(getProduto(ds.Rows[i].ItemArray));
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
            return produtos;
        }

        public static long getLastId()
        {
            return DBUtil.getLastId("produto");
        }

        public static Boolean Persistir(Produto produto)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                if (produto.Id > 0)//update
                {
                    cmd = new SqlCommand("update produto set nome=@nome, valor=@valor, descricao=@descricao where id=@id", conexao);
                }
                else//inset
                {
                    produto.Id = DBUtil.getNextId("produto");
                    cmd = new SqlCommand("insert into produto(id, nome, valor, descricao) values (@id, @nome, @valor, @descricao)", conexao);
                }
                conexao.Open();
                cmd.Parameters.AddWithValue("@id", produto.Id);
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@valor", produto.Valor);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Persistência - Erro ao conectar ao banco de dados" + e.Message);
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }

        public static Produto BuscarPorId(long id)
        {
            Produto produto = null;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select p.id, p.nome, p.valor, p.descricao from produto as p where p.id=@id", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                produto = getProduto(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do produto" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }
            return produto;
        }

        public static Boolean Excluir(Produto produto)
        {
            return Excluir(produto.Id);
        }

        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from produto where id=@id", conexao);
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

        public static List<Produto> Buscar(String Parametro)
        {
            List<Produto> produtos = new List<Produto>();

            long Id = 0;
            try
            {
                Id = long.Parse(Parametro);
            }
            catch { }

            try
            {
                SqlCommand cmd = new SqlCommand("select p.id, p.nome, p.valor," +
                    " p.descricao from produto AS p " +
                    " WHERE p.nome LIKE @nome " +
                    " OR p.id = @id " +
                    " OR p.descricao LIKE @descricao " +
                    " ORDER BY p.id ",

                DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@nome", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@descricao", "%" + Parametro + "%");
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    produtos.Add(getProduto(ds.Tables[0].Rows[i].ItemArray));
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
            return produtos;
        }
    }
}
