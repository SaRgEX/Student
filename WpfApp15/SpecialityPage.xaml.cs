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
    public partial class SpecialtyPage : Page
    {
        public ObservableCollection<Speciality> specialities { get; set; } = new ObservableCollection<Speciality>();

        public SpecialtyPage()
        {
            InitializeComponent();
            LoadSpecialty();
            DataContext = this;
            Placeholder.SetElement(textBox, "Speciality", "Enter Sepciality");
        }

        private void TryAddSpecialityButton(object sender, RoutedEventArgs e)
        {
            try
            {
                string nameSpeciality = textBox.Text.Trim();
                NpgsqlCommand command = Connection.GetCommand("INSERT INTO \"Speciality\"(\"NameSpeciality\") VALUES (@NameSpeciality)");
                command.Parameters.AddWithValue("@NameSpeciality", NpgsqlDbType.Varchar, nameSpeciality);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    LoadSpecialty();
                }
            }
            catch (Exception ex)
            {
                MainWindow.MessageShow("Already exist " + ex.Message);
            }
        }

        private void LoadSpecialty()
        {
            specialities.Clear();
            try
            {
                NpgsqlCommand command = Connection.GetCommand("SELECT \"IdSpeciality\", \"NameSpeciality\" FROM \"Speciality\"");
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        specialities.Add(new Speciality(result.GetInt32(0), result.GetString(1)));
                    }
                }
                result.Close();
            }
            catch (Exception ex)
            {
                MainWindow.MessageShow("Already exist" + ex.Message);
            }
        }

        private void ButtonDeleteSpeciality(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listBox.SelectedItem == null) return;
                int selectedItem = (listBox.SelectedItem as Speciality).IdSpeciality;
                NpgsqlCommand command = Connection.GetCommand("DELETE FROM \"Speciality\" WHERE \"Speciality\".\"IdSpeciality\"=@IdSpeciality;");
                command.Parameters.AddWithValue("@IdSpeciality", NpgsqlDbType.Integer, selectedItem);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    LoadSpecialty();
                }
            }
            catch (Exception ex)
            {
                MainWindow.MessageShow("There is no items " + ex.Message);
            }
        }

        private void ButtonHome(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetMainPage);
        }
        private void ButtonNext(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetGroupPage);
        }
        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetTimetablePage);
        }
        public void HideFromUser()
        {
            textBox.Visibility = Visibility.Collapsed;
            buttonCreate.Visibility = Visibility.Collapsed;
            buttonDelete.Visibility = Visibility.Collapsed;
        }
    }
}
