using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace oanh
{
    internal class Database
    {
        private static SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0026BRR\SQLEXPRESS;Initial Catalog=QLCHQA;Integrated Security=True;TrustServerCertificate=True");

        public static void Execute(string sql, Dictionary<string, object> parameter = null)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            if (parameter != null)
            {
                foreach (string key in parameter.Keys)
                {
                    command.Parameters.Add(new SqlParameter(key, parameter[key]));
                }
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static DataTable Query(string sql, Dictionary<string, object> parameter = null)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            if (parameter != null)
            {
                foreach (string key in parameter.Keys)
                {
                    command.Parameters.Add(new SqlParameter(key, parameter[key]));
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();
            return table;
        }
    }
}
