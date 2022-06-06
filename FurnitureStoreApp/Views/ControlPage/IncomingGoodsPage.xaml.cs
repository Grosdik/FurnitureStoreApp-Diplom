using FurnitureStoreApp.DataBase;
using FurnitureStoreApp.Utils;
using FurnitureStoreApp.Views.AddEditPage;
using FurnitureStoreApp.Views.PrintPage;
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

namespace FurnitureStoreApp.Views.ControlPage
{
    /// <summary>
    /// Логика взаимодействия для IncomingGoodsPage.xaml
    /// </summary>
    public partial class IncomingGoodsPage : Page
    {
        public IncomingGoodsPage()
        {
            InitializeComponent();
            filtProduct.ItemsSource = App.connect.Products.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.connect.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridIncGoods.ItemsSource = App.connect.IncomingGoods.ToList();
            }
        }

        private void btnAddCIncGoods_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddIncomingGoodsPage(null));
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*dGridIncGoods.ItemsSource = App.connect.IncomingGoods.ToList().Where(p => p.Product.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();*/
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new PrintIncomingGoodsPage());
        }

        private void filtProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filt = filtProduct.SelectedItem as Products;
            dGridIncGoods.ItemsSource = App.connect.IncomingGoods.Where(p => p.Products.Id == filt.Id).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(endDate.Text) && !string.IsNullOrWhiteSpace(startDate.Text))
            {
                dGridIncGoods.ItemsSource = App.connect.IncomingGoods.Where(p => p.Date >= startDate.SelectedDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(endDate.Text) && string.IsNullOrWhiteSpace(startDate.Text))
            {
                dGridIncGoods.ItemsSource = App.connect.IncomingGoods.Where(p => p.Date <= endDate.SelectedDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(endDate.Text) && !string.IsNullOrWhiteSpace(startDate.Text))
            {
                dGridIncGoods.ItemsSource = App.connect.IncomingGoods.Where(p => p.Date >= startDate.SelectedDate && p.Date <= endDate.SelectedDate).ToList();
            }
            else
            {
                MessageBox.Show("Введите даты!", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
