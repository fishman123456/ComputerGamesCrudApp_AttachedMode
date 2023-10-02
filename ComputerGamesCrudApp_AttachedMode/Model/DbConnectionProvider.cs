using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGamesCrudApp_AttachedMode.Model
{
    // класс, настраивающий и обеспечивающий подключение к БД
    internal class DbConnectionProvider
    {
        // метод, создающий и открывающий подключение к БД
        public SqlConnection OpenDbConnection()
        {
            // строка подключения считывается из файла конфигурации
            string connectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
