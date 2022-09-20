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
    public partial class GroupPage : Page
    {
        public ObservableCollection<Course> course { get; set; } = new ObservableCollection<Course>();
        public ObservableCollection<Group> group { get; set; } = new ObservableCollection<Group>();
        private Connection _connection = new Connection();
        public GroupPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadCourse();
            LoadGroup();

            comboBoxSpeciality.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = PageControl.GetSpecialtyPage.specialities
            });
        }
        private void LoadGroup()
        {
            try
            {
                group.Clear();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "SELECT \"IdGroup\", \"IdSpeciality\", \"IdCourse\" FROM \"Group\"";
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        group.Add(new Group(result.GetInt32(0), result.GetInt32(1), result.GetInt32(2)));
                    }
                }
                result.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadCourse()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection.connection;
            command.CommandText = "SELECT \"IdCourse\" FROM \"Course\"";
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    course.Add(new Course(result.GetInt32(0)));
                }
            }
            result.Close();
        }
        private void ButtonAddGroup(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = _connection.connection;
                command.CommandText = "INSERT INTO \"Group\" (\"IdGroup\", \"IdSpeciality\", \"IdCourse\") " +
                    "VALUES (@IdGroup, @IdSpeciality, @IdCourse)";
                command.Parameters.AddWithValue("@IdGroup", NpgsqlDbType.Integer, Convert.ToInt32(textBoxGroup.Text));
                command.Parameters.AddWithValue("@IdSpeciality", NpgsqlDbType.Integer, (comboBoxSpeciality.SelectedItem as Speciality).IdSpeciality);
                command.Parameters.AddWithValue("@IdCourse", NpgsqlDbType.Integer, (comboBoxCourse.SelectedItem as Course).Id);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Success");
                }
                LoadGroup();
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
