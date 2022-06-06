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
    /// Логика взаимодействия для GoodsSoldPage.xaml
    /// </summary>
    public partial class GoodsSoldPage : Page
    {
        public GoodsSoldPage()
        {
            InitializeComponent();
            filtProduct.ItemsSource = App.connect.Products.ToList();
        }

        private void btnAddIncGoods_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddGoodsSoldPage(null));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.connect.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridGoodsSold.ItemsSource = App.connect.GoodsSold.ToList();
            }
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ///dGridGoodsSold.ItemsSource = App.connect.GoodsSold.ToList().Where(p => p.Name.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new PrintGoodsSoldPage());
        }

        private void filtProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filt = filtProduct.SelectedItem as Products;
            dGridGoodsSold.ItemsSource = App.connect.GoodsSold.Where(p => p.Products.Id == filt.Id).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(endDate.Text) && !string.IsNullOrWhiteSpace(startDate.Text))
            {
                dGridGoodsSold.ItemsSource = App.connect.GoodsSold.Where(p => p.Date >= startDate.SelectedDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(endDate.Text) && string.IsNullOrWhiteSpace(startDate.Text))
            {
                dGridGoodsSold.ItemsSource = App.connect.GoodsSold.Where(p => p.Date <= endDate.SelectedDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(endDate.Text) && !string.IsNullOrWhiteSpace(startDate.Text))
            {
                dGridGoodsSold.ItemsSource = App.connect.GoodsSold.Where(p => p.Date >= startDate.SelectedDate && p.Date <= endDate.SelectedDate).ToList();
            }
            else
            {
                MessageBox.Show("Введите даты!", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
