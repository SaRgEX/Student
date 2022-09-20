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
    public partial class MainWindow : Window
    {
        private NpgsqlConnection connection;
        public ObservableCollection<Student> students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Speciality> specialities { get; set; } = new ObservableCollection<Speciality>();
        public ObservableCollection<Course> course { get; set; } = new ObservableCollection<Course>();
        public ObservableCollection<Group> group { get; set; } = new ObservableCollection<Group>();
        
        public MainWindow()
        {
            InitializeComponent();
            Connect("10.14.206.27", "5432", "student", "1234", "Students");
            DataContext = this;

            LoadSpecialty();
            LoadCourse();
            LoadGroup();
        }
        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Server={0};Port={1};User ID={2};Password={3};DataBase={4}", host, port, user, pass, dbname);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }
        private void LoadGroup()
        {
            try
            {
                group.Clear();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
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
            command.Connection = connection;
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
        private void TryAddSpecialityButton(object sender, RoutedEventArgs e)
        {
            try
            {
                string nameSpeciality = textBoxSpeciality.Text.Trim();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO \"Speciality\"(\"NameSpeciality\") VALUES (@NameSpeciality)";
                command.Parameters.AddWithValue("@NameSpeciality", NpgsqlDbType.Varchar, nameSpeciality);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Success");
                    LoadSpecialty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Already exist " + ex.Message);
            }
        }

        private void LoadSpecialty()
        {
            specialities.Clear();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT \"IdSpeciality\", \"NameSpeciality\" FROM \"Speciality\"";
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        specialities.Add(new Speciality(result.GetInt32(0),result.GetString(1)));
                    }
                    MessageBox.Show("Success");
                }
                result.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Already exist" + ex.Message);
            }
        }

        private void ButtonDeleteSpeciality(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedItem = (listBox.SelectedItem as Speciality).IdSpeciality;
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM \"Speciality\" WHERE \"Speciality\".\"IdSpeciality\"=@IdSpeciality;";
                command.Parameters.AddWithValue("@IdSpeciality", NpgsqlDbType.Integer, selectedItem);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Success");
                    LoadSpecialty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is no items " + ex.Message);
            }
        }

        private void ButtonAddGroup(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection=connection;
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
    }

}
