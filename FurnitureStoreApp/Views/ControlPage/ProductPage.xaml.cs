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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            var currentProduct = App.connect.Products.ToList();
            lViewsProduct.ItemsSource = currentProduct;
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            lViewsProduct.ItemsSource = App.connect.Products.ToList().Where(p => p.Name.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new PrintProductsPage());
        }

        private void goIncGoods_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new IncomingGoodsPage());
        }

        private void goGoodsSold_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new GoodsSoldPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddEditProductPage(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var delprod = lViewsProduct.SelectedItems.Cast<Products>().ToList();
            if (MessageBox.Show($"Вы точно ходите удалить следующие{delprod.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    App.connect.Products.RemoveRange(delprod);
                    App.connect.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    lViewsProduct.ItemsSource = App.connect.Products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void bntEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddEditProductPage(lViewsProduct.SelectedItem as Products));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.connect.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                lViewsProduct.ItemsSource = App.connect.Products.ToList();
            }
        }
    }
}
