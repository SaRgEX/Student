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
    public partial class TimetablePage : Page
    {
        public ObservableCollection<Timetable> timetables { get; set; } = new ObservableCollection<Timetable>();
        public TimetablePage()
        {
            InitializeComponent();

            DataContext = this;

            Placeholder.SetElement(textBoxDate, "Date", "Enter date");
            Placeholder.SetElement(textBoxCabinet, "Class", "Enter cabinet");
            Placeholder.SetElement(textBoxBilling, "Billing", "Enter billing");
            LoadTimetable();
            comboBoxClass.Items.Add(1);
            comboBoxClass.Items.Add(2);
            comboBoxClass.Items.Add(3);
            comboBoxClass.Items.Add(4);
            comboBoxClass.Items.Add(5);
            comboBoxClass.Items.Add(6);
            comboBoxGroup.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = PageControl.GetGroupPage.group
            });
        }

        private void LoadTimetable()
        {
            try
            {
                NpgsqlCommand command = Connection.GetCommand("SELECT \"Group\".\"IdGroup\", \"Cabinet\", \"Class\", \"Date\" " +
                    "FROM \"Timetable\", \"Billing\", \"Group\" " +
                    "WHERE (\"Billing\".\"IdBilling\" = \"Timetable\".\"IdBilling\" AND \"Billing\".\"IdGroup\" = \"Group\".\"IdGroup\")");
                var text = command.ExecuteReader();
                bool result = true;
                if (text.HasRows)
                {
                    while (result && text.Read())
                    {
                        timetables.Add(new Timetable(text.GetInt32(0), text.GetString(1), text.GetInt32(2), text.GetDateTime(3)));
                    }
                }
                text.Close();
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
            NavigationService.Navigate(PageControl.GetGroupPage);
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetTimetablePage);
        }

        private void ButtonAddTimetable(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommand command = Connection.GetCommand("INSERT INTO \"Timetable\" (\"IdBilling\", \"Cabinet\", \"Class\", \"Date\") " +
                    "VALUES(@IdBilling, @Cabinet, @Class, @Date)");
                command.Parameters.AddWithValue("@IdBilling", Convert.ToInt32(textBoxBilling.Text));
                command.Parameters.AddWithValue("@Cabinet", textBoxCabinet.Text);
                command.Parameters.AddWithValue("@Class", comboBoxClass.SelectedItem);
                command.Parameters.AddWithValue("@Date", Convert.ToDateTime(textBoxDate.Text));
                command.ExecuteNonQuery();
                LoadTimetable();
            }
            catch (Exception ex)
            {
                MainWindow.MessageShow(ex.Message);
            }
        }
    }
}
