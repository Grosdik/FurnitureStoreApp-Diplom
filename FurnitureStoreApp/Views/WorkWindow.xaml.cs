using FurnitureStoreApp.Utils;
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

namespace FurnitureStoreApp.Views
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        public WorkWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizationPage());
            Transfer.MainFrame = MainFrame;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.GoBack();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Transfer.MainFrame.CanGoBack)
            {
                this.ResizeMode = ResizeMode.CanResize;
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                this.ResizeMode = ResizeMode.NoResize;
                btnBack.Visibility = Visibility.Collapsed;
            }
        }
    }
}
