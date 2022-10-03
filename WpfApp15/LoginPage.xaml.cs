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
        public LoginPage()
        {
            InitializeComponent();
            Placeholder.SetElement(textBoxLogin, "Login", "Enter login");
            Placeholder.SetElement(textBoxPassword, "Password", "Enter password");
        }

        private void ButtonGoToRegPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetRegistrationPage);
        }

        private void ButtonLogin(object sender, RoutedEventArgs e)
        {
            if (!CheckAuthorization())
            {
                MainWindow.MessageShow("Valid input");
                return;
            }
            if (!CheckLogin())
            {
                MainWindow.MessageShow("Wrong login or password");
                return;
            }
            NavigationService.Navigate(PageControl.GetMainPage);
        }

        private bool CheckAuthorization()
        {
            return textBoxLogin.Text != "Enter login" ||
                   textBoxPassword.Text != "Enter password" ||
                   textBoxLogin.Text != String.Empty ||
                   textBoxPassword.Text != String.Empty;
        }

        private bool CheckLogin()
        {
            try
            {
                NpgsqlCommand command = Connection.GetCommand("SELECT \"Role\" FROM \"Employee\" WHERE \"Login\"=@login AND \"Password\"=@password");
                command.Parameters.AddWithValue("@login", textBoxLogin.Text.Trim());
                command.Parameters.AddWithValue("@password", textBoxPassword.Text.Trim());
                var role = command.ExecuteReader();
                if (role.HasRows)
                {
                    role.Read();
                    if (role.IsDBNull(0))
                    {
                        role.Close();
                        PageControl.GetSpecialtyPage.HideFromUser();
                        PageControl.GetGroupPage.HideFromUser();
                        PageControl.GetStudentPage.HideFromUser();
                        PageControl.GetTeacherPage.HideFromUser();
                    }

                    //data.Read();

                    //data.GetString(0);

                    role.Close();
                    return true;
                }
                role.Close();
                return false;

            }
            catch (Exception ex)
            {
                MainWindow.MessageShow(ex.Message);
            }
            return false;
        }

    }
}
