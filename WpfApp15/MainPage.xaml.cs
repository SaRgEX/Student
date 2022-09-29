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

namespace WpfApp15
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void ButtonMoveToSpecialityPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetSpecialtyPage);
        }

        private void ButtonMoveToGroupPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetGroupPage);
        }

        private void ButtonMoveToStudentPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetStudentPage);
        }

        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.GetLoginPage);
        }
    }
}
