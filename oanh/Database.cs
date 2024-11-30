using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace oanh
{
    class Database
    {
        private static SqlConnection connection = new SqlConnection(
            @"Data Source=PTKDrake-PC;Initial Catalog=QLCHQA;Integrated Security=True;Encrypt=False");

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

                command.ExecuteNonQuery();
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