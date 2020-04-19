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

namespace _2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void btn_add_user_Click(object sender, RoutedEventArgs e)
        {
            MenuUtils.AddUser(txtBox_name.Text);
            CloseWindow();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            Window window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
