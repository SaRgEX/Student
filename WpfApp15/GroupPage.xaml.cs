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

        public GroupPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadCourse();
            LoadGroup();
            Placeholder.SetElement(textBoxGroup, "Group", "Enter Group");
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
                NpgsqlCommand command = Connection.GetCommand("SELECT \"IdGroup\", \"IdSpeciality\", \"IdCourse\" FROM \"Group\"");
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
                MainWindow.MessageShow(ex.Message);
            }
        }

        private void LoadCourse()
        {
            NpgsqlCommand command = Connection.GetCommand("SELECT \"IdCourse\" FROM \"Course\"");
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
                NpgsqlCommand command = Connection.GetCommand("INSERT INTO \"Group\" (\"IdGroup\", \"IdCourse\", \"IdSpeciality\") VALUES (@IdGroup, @IdCourse, @IdSpeciality)");
                command.Parameters.AddWithValue("@IdGroup", NpgsqlDbType.Integer, Convert.ToInt32(textBoxGroup.Text));
                command.Parameters.AddWithValue("@IdSpeciality", NpgsqlDbType.Integer, (comboBoxSpeciality.SelectedItem as Speciality).IdSpeciality);
                command.Parameters.AddWithValue("@IdCourse", NpgsqlDbType.Integer, (comboBoxCourse.SelectedItem as Course).Id);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                }
                LoadGroup();
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
            NavigationService.Navigate(PageControl.GetStudentPage);
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetSpecialtyPage);
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
