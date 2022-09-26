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
    public partial class RegistrationPage : Page
    {
        private Connection _connection = new Connection();

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void ButtonGoToLoginPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetLoginPage);
        }

        private void ButtonRegistration(object sender, RoutedEventArgs e)
        {
            if (!ValidInput())
            {
                MessageBox.Show("!zaebis");
                return;
            }
            InsertData();
        }
        private bool ValidInput()
        {
            if (EmptyInput())
            {
                MessageBox.Show("(in)Valid input");
                return false;
            }
            if (LoginExists())
            {
                MessageBox.Show("Login has already existed");
                return false;
            }
            return true;
        }
        private bool EmptyInput()
        {
            return textBoxName.Text == String.Empty ||
                   textBoxLogin.Text == String.Empty ||
                   textBoxPassword.Text == String.Empty;
        }

        private bool LoginExists()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "SELECT \"Login\" FROM \"Employee\"";
                NpgsqlDataReader login = command.ExecuteReader();
                while (login.Read())
                {
                    if (textBoxLogin.Text == login.GetString(0)) return true;
                }
                login.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        private void InsertData()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "INSERT INTO \"Employee\" (\"FullName\", \"Login\", \"Password\") VALUES (@FullName, @Login, @Password)";
                command.Parameters.AddWithValue("@FullName", NpgsqlDbType.Varchar, textBoxName.Text);
                command.Parameters.AddWithValue("@Login", NpgsqlDbType.Varchar, textBoxLogin.Text);
                command.Parameters.AddWithValue("@Password", NpgsqlDbType.Varchar, textBoxPassword.Text);
                var result = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
