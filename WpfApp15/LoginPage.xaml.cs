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
    public partial class LoginPage : Page
    {
        private Connection _connection = new Connection();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonGoToRegPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetRegistrationPage);
        }

        private void ButtonLogin(object sender, RoutedEventArgs e)
        {
            if (!CheckAuthorization())
            {
                MessageBox.Show("Valid input");
                return;
            }
            if (CheckLogin())
            {
                NavigationService.Navigate(PageControl.GetMainPage);
            }
        }
        private bool CheckAuthorization()
        {
            return textBoxLogin.Text != String.Empty ||
                   textBoxPassword.Text != String.Empty;
        }
        private bool CheckLogin()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "SELECT \"Role\" FROM \"Employee\" WHERE \"Login\"=@login AND \"Password\"=@password";
                command.Parameters.AddWithValue("@login", textBoxLogin.Text.Trim());
                command.Parameters.AddWithValue("@password", textBoxPassword.Text.Trim());
                var role = command.ExecuteReader();
                role.Read();
                if (role.GetString(0) == null)
                {
                    PageControl.GetSpecialtyPage.DeleteForUser();
                    PageControl.GetGroupPage.DeleteForUser();
                    PageControl.GetStudentPage.DeleteForUsers();
                }
                NpgsqlDataReader data = command.ExecuteReader();
                if (data.HasRows)
                {
                    //data.Read();

                    //data.GetString(0);

                    data.Close();
                    return true;
                }
                data.Close();
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        
    }
}
