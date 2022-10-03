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
using System.Collections.ObjectModel;
using Npgsql;
using NpgsqlTypes;

namespace WpfApp15
{
    public partial class TeacherPage : Page
    {
        public ObservableCollection<Teacher> teachers { get; set; } = new ObservableCollection<Teacher>();

        public TeacherPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadTeacher();
            Placeholder.SetElement(textBox, "FullNameTeacher", "Enter full name");
        }

        private void ButtonAddTeacher(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidInput())
                {
                    MainWindow.MessageShow("Valid input");
                    return;
                }
                NpgsqlCommand command = Connection.GetCommand("INSERT INTO \"Teacher\" (\"FullName\") VALUES (@fullName)");
                command.Parameters.AddWithValue("@fullName", textBox.Text);
                command.ExecuteNonQuery();
                LoadTeacher();
            }
            catch (Exception ex)
            {
                MainWindow.MessageShow(ex.Message);
            }
        }

        private bool ValidInput()
        {
            return textBox.Text != String.Empty &&
                   textBox.Text != "Enter full name";
        }
        private void LoadTeacher()
        {
            try
            {
                NpgsqlCommand command = Connection.GetCommand("SELECT \"IdTeacher\", \"FullName\" FROM \"Teacher\"");
                var text = command.ExecuteReader();
                while (text.Read())
                {
                    teachers.Add(new Teacher(text.GetInt32(0), text.GetString(1)));
                }
                text.Close();
            }
            catch(Exception ex)
            {
                MainWindow.MessageShow(ex.Message);
            }
        }

        private void ButtonHome(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetMainPage);
        }

        private void ButtonNext(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetTimetablePage);
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetStudentPage);
        }

        public void HideFromUser()
        {
            for (int i = 0; i < stackPanel.Children.Count -1; i++)
            {
                stackPanel.Children[i].Visibility = Visibility.Collapsed;
            }
        }
    }
}
