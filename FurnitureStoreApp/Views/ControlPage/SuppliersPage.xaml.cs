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
    /// Логика взаимодействия для SuppliersPage.xaml
    /// </summary>
    public partial class SuppliersPage : Page
    {
        private List<Suppliers> Suppliers;
        public SuppliersPage()
        {
            InitializeComponent();
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGridSuppliers.ItemsSource = App.connect.Suppliers.ToList().Where(p => p.Company.ToLower().Contains(txtBoxSearch.Text.ToLower()) ||
                                                                                   p.Phone.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddEditSupplierPage((sender as Button).DataContext as Suppliers));
        }

        private void btnAddSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddEditSupplierPage(null));
        }

        private void btnDeleteSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var supplersForRemoving = dGridSuppliers.SelectedItems.Cast<Suppliers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {supplersForRemoving.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    App.connect.Suppliers.RemoveRange(supplersForRemoving);
                    App.connect.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dGridSuppliers.ItemsSource = App.connect.Suppliers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.connect.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridSuppliers.ItemsSource = App.connect.Suppliers.ToList();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new PrintSuppliersPage());
        }
    }
}
