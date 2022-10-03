using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;
using System.Collections.ObjectModel;

namespace WpfApp15
{

    public class Connection
    {
        private static NpgsqlConnection connection;
        public Connection()
        {
        }
        public static void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Server={0};Port={1};User ID={2};Password={3};DataBase={4}", host, port, user, pass, dbname);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }
        public static NpgsqlCommand GetCommand(string _command)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = _command;
            return command;
        }
    }
}
