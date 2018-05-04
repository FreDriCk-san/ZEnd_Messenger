using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ZEnd
{
    /// <summary>
    /// Логика взаимодействия для Messenger.xaml
    /// </summary>
    public partial class Messenger : Window
    {
        private zeAPI.Users users { get; set; }
        private zeAPI.Chats currentChat { get; set; }
        private List<zeAPI.Chats> chats = new List<zeAPI.Chats>();
        private List<zeAPI.Users> allUsers = new List<zeAPI.Users>();


        public Messenger(zeAPI.Users users)
        {
            InitializeComponent();
            welcome.Content = "Welcome back, " + users.Name + ", again...";
            this.users = users;
            currentChat = null;
            //Store name of User----

            zeAPI.Connection.Connect(users, c => Dispatcher.Invoke(() =>
            {
                if (null != currentChat && c.ChatId == currentChat.Id)
                {
                    listBox2.Items.Add(zeAPI.Users.GetInfo(c.UserId).Name + " - " + c.Text + " / " + DateTime.UtcNow.ToShortTimeString());
                }
            }), c => Dispatcher.Invoke(() => { chats.Add(c); listBox1.Items.Add(c.Name); }));

            if (null != users.Avatar)
            {
                var avatar = new BitmapImage();
                avatar.BeginInit();
                try
                {
                    avatar.StreamSource = new System.IO.MemoryStream(zeAPI.Attachments.GetFileAsBytesAsync(users.Avatar));
                    avatar.EndInit();
                    image1.Source = avatar;
                }
                catch { }
            }
        }

        private void createChat_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = new HashSet<zeAPI.Users>();
            foreach (var us in zeAPI.Users.AllUsers())
            {
                allUsers.Add(zeAPI.Users.GetInfo(us.Id));
                //allUsers.Add(zeAPI.Users.GetInfo(int.Parse(us.ToString().Substring(0,2))));
            }
            users.CreateChat(chatName.Text, zeAPI.ChatType.Public, allUsers);
            chatName.Clear();
        }

        private void listBox1_Loaded(object sender, RoutedEventArgs e)
        {
            chats.Clear();
            foreach (var chat in users.GetChats())
            {
                chats.Add(chat);
                listBox1.Items.Add(chat.Name);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null != listBox1.SelectedItem)
            {
                listBox2.Items.Clear();
                foreach (var message in (users.GetMessages(chats[listBox1.SelectedIndex])))
                {
                    listBox2.Items.Add(zeAPI.Users.GetInfo(message.UserId).Name + " - " + message.Text + " | " + message.Date.ToShortTimeString());
                }
                currentChat = chats[listBox1.SelectedIndex];
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            users.SendMessage(messageBox.Text, currentChat);
            messageBox.Clear();
        }

        ~Messenger()
        {
            zeAPI.Connection.Disconnect();
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            allUsers.Clear();
            foreach (var user in zeAPI.Users.FindUsers(userLogin.Text))
            {
                allUsers.Add(user);
                listBox.Items.Add(user.Name);
            }
        }

        private void listBox3_Loaded(object sender, RoutedEventArgs e)
        {
            listBox3.Items.Clear();
            foreach (var user in zeAPI.Users.AllUsers())
            {
                listBox3.Items.Add(user.Id + " - " + user.Name);
            }
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg|*.jpg| bmp|*.bmp";

            Nullable<bool> result = dialog.ShowDialog();

            string fileName = "";

            if (true == result)
            {
                fileName = dialog.FileName;
            }
            users.ChangeAvatar(fileName);
            users = zeAPI.Users.GetInfo(users.Id);

            var avatar = new BitmapImage();
            avatar.BeginInit();
            try
            {
                avatar.StreamSource = new System.IO.MemoryStream(zeAPI.Attachments.GetFileAsBytesAsync(users.Avatar));
                avatar.EndInit();
                image1.Source = avatar;
            }
            catch { }
        }
    }
}
