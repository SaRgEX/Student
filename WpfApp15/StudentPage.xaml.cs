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
    public partial class StudentPage : Page
    {
        public ObservableCollection<Student> students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Group> group { get; set; } = new ObservableCollection<Group>();

        public StudentPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadStudent();
            Placeholder.SetElement(textBox, "Student", "Enter student");
            comboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = PageControl.GetGroupPage.group
            });
        }

        private void LoadStudent()
        {
            try
            {
                students.Clear();
                NpgsqlCommand command = Connection.GetCommand("SELECT \"IdStudent\", \"FullName\", \"IdGroup\" FROM \"Student\"");
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        students.Add(new Student(result.GetInt32(0), result.GetString(1), result.GetInt32(2)));
                    }
                }
                result.Close();
            }
            catch (Exception ex)
            {
                MainWindow.MessageShow(ex.Message);
            }
        }

        private void ButtonAddStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommand command = Connection.GetCommand("INSERT INTO \"Student\" (\"FullName\", \"IdGroup\") VALUES (@FullName, @IdGroup)");
                command.Parameters.AddWithValue("@FullName", NpgsqlDbType.Varchar, textBox.Text);
                command.Parameters.AddWithValue("@IdGroup", NpgsqlDbType.Integer, (comboBox.SelectedItem as Group).IdGroup);
                int result = command.ExecuteNonQuery();
                LoadStudent();
            }
            catch (Exception ex)
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
            NavigationService.Navigate(PageControl.GetTeacherPage);
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetGroupPage);
        }
        public void HideFromUser()
        {
            for (int i = 0; i < stackPanel.Children.Count - 1; i++)
            {
                stackPanel.Children[i].Visibility = Visibility.Collapsed;
            }
        } 
    }
}
