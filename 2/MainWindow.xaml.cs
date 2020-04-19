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

namespace _2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!MenuUtils.Initialized)
                MenuUtils.Initialize();

            list_users.ItemsSource = MenuUtils.Users.Keys;
        }

        // image buttons

        private void btn_prev_image_Click(object sender, RoutedEventArgs e)
        {
            img_profile.Source = MenuUtils.GetPreviousImage();
            UpdateUserImage();
        }

        private void btn_next_image_Click(object sender, RoutedEventArgs e)
        {
            img_profile.Source = MenuUtils.GetNextImage();
            UpdateUserImage();
        }


        // game buttons

        private void btn_new_user_Click(object sender, RoutedEventArgs e)
        {
            Window window = new NewUserWindow();
            window.Show();
            Close();
        }

        private void btn_delete_user_Click(object sender, RoutedEventArgs e)
        {
            if(list_users.SelectedItem!=null)
            {
                MenuUtils.DeleteUser(list_users.SelectedItem.ToString());
                list_users.Items.Refresh();
            }
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            if (list_users.SelectedItem != null)
            {
                GameUtils.User = MenuUtils.Users[list_users.SelectedItem.ToString()];

                Window window = new GameWindow();
                window.Show();

                Close();
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            MenuUtils.SaveUsers();
            Close();
        }

        // list

        private void list_users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(list_users.SelectedItem!=null)
            {
                img_profile.Source = MenuUtils.GetUserImage(list_users.SelectedItem.ToString());
            }
        }

        // auxilliary

        private void UpdateUserImage()
        {
            if (list_users.SelectedItem != null)
                MenuUtils.UpdateUserImage(list_users.SelectedItem.ToString());
        }
    }
}
