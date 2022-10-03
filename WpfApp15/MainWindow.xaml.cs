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
using System.Windows.Threading;

namespace WpfApp15
{
    public partial class MainWindow : Window
    {
        public Connection _connection = new Connection();

        static DispatcherTimer _timer = new DispatcherTimer();

        static Border _border = new Border();

        static TextBlock _textBlock = new TextBlock();

        public MainWindow()
        {
            InitializeComponent();
            Connection.Connect("10.14.206.27", "5432", "student", "1234", "Students");

            _timer.Interval = new TimeSpan(0, 0, 0, 5);
            _timer.Tick += _timer_Tick;

            _border = message;
            _textBlock = messageText;

        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            messageText.Text = "";
            message.Visibility = Visibility.Hidden;
            _timer.Stop();
        }

        public static void MessageShow(string message)
        {
            _textBlock.Text = message;
            _border.Visibility = Visibility.Visible;
            _timer.Start();
        }
    }
}
