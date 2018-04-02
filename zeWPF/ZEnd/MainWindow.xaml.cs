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
using zeAPI;

namespace ZEnd
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Users users;

        string login;
        string password;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Sign in (Authorization)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Messenger messenger = new Messenger();

            messenger.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = sender as TextBox;
            login = txtBox.Text;
        }

        //Sign up (Registration)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();

            registration.Show();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pasBox = sender as PasswordBox;
            password = pasBox.Password;
        }
    }
}
