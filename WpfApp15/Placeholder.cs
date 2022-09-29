using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp15
{
    public class Placeholder
    {
        private static Dictionary<string, string> defaultPlaceholder = new Dictionary<string, string>();
        private static Brush placeholderColor = Brushes.Gray;
        private static Brush textColor = Brushes.Black;
        private static List<UIElement> uIElements = new List<UIElement>();

        public static void SetElement(TextBox element, string uid, string placeholder)
        {
            element.Uid = uid;
            element.Text = placeholder;
            //element.Padding = new Thickness(10, 0, 0, 0);
            element.VerticalContentAlignment = VerticalAlignment.Center;
            element.HorizontalContentAlignment = HorizontalAlignment.Left;
            //element.FontFamily = new FontFamily("Verdana");
            //element.FontSize = 14;
            element.Foreground = placeholderColor;
            element.GotFocus += textBoxFocus;
            element.LostFocus += textBoxLostFocus;
            element.TextChanged += textBoxTextChanged;

            defaultPlaceholder[uid] = placeholder;
        }

        private static void textBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text == defaultPlaceholder[textBox.Uid])
                {
                    textBox.Foreground = placeholderColor;
                }
                else
                {
                    textBox.Foreground = textColor;
                }
            }
        }

        private static void textBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text.Length == 0)
                {
                    textBox.Text = defaultPlaceholder[textBox.Uid];
                }

                if (textBox.Text == defaultPlaceholder[textBox.Uid])
                {
                    textBox.Foreground = placeholderColor;
                }
            }
        }

        private static void textBoxFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text == defaultPlaceholder[textBox.Uid])
                {
                    textBox.Text = "";
                    textBox.Foreground = textColor;
                }
            }
        }
        private static void setTextBoxPlaceholder()
        {
            foreach (var element in uIElements)
            {
                if (element.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)element;
                    textBox.Foreground = placeholderColor;
                    textBox.Text = defaultPlaceholder[textBox.Uid];
                    textBox.GotFocus += textBoxFocus;
                    textBox.LostFocus += textBoxLostFocus;
                    textBox.TextChanged += textBoxTextChanged;
                }
            }
        }
    }
}
