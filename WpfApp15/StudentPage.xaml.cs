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

        private Connection _connection = new Connection();

        public StudentPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadStudent();
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
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "SELECT \"IdStudent\", \"FullName\", \"IdGroup\" FROM \"Student\"";
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
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonAddStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "INSERT INTO \"Student\" (\"FullName\", \"IdGroup\")" +
                    "VALUES (@FullName, @IdGroup)";
                command.Parameters.AddWithValue("@FullName", NpgsqlDbType.Varchar, textBox.Text);
                command.Parameters.AddWithValue("@IdGroup", NpgsqlDbType.Integer, (comboBox.SelectedItem as Group).IdGroup);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Success");
                }
                LoadStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetMainPage);
        }
    }
}
