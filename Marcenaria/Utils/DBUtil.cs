using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scaweb.Utils
{
    class DBUtil
    {
        private static SqlConnection con;

        public static SqlConnection getConnection()
        {
            if (con == null)
            {
                string strcon = @"Data Source=DESKTOP-M8FGI40\SQLEXPRESS;Initial Catalog=marcenaria;Integrated Security=True";
                con = new SqlConnection(strcon);
                con.ConnectionString = strcon;
            }

            return con;
        }

        public static void closeConnection()
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        /**
         * Calcula o proximo ID para a Tabela
         * parametro tabela - Nome da tabela para o calculo do proximo ID.
         */
        public static int getNextId(string tabela)
        {
            SqlCommand cmd = new SqlCommand("SP_GETNEXTID", getConnection());

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TABELA", tabela);
            cmd.Parameters.AddWithValue("@CAMPO", "id");

            SqlParameter paraout = new SqlParameter("@ID", SqlDbType.BigInt);
            paraout.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paraout);

            getConnection().Open();
            cmd.ExecuteNonQuery();

            int valor = (int.Parse(cmd.Parameters["@ID"].Value.ToString()));
            closeConnection();

            return valor;
        }



        /// <summary>
        /// Método responsável por criar a conexão com o banco e executar as query.
        /// </summary>
        public static bool GetResultQuery(string query, DataTable dt)
        {
            try
            {
                if (dt == null)
                    return false;

                // Cria a conexão com o banco.
                SqlConnection conn = getConnection();
                conn.Open();

                // Executar a query. 
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                // Fill-preenche o datatable com o que foi guardado no dataadapter.
                da.Fill(dt);

                conn.Close();

                return true;

            }
            catch (Exception e)
            {
                closeConnection();
                return false;
            }

        }

        public static long getLastId(String tabela)
        {
            long retorno = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("select max(i.id) as total from " + tabela + " as i", DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                retorno = Convert.ToInt64(dt.Rows[0].ItemArray[0]);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id da revisao" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return retorno;
        }

    }
}
