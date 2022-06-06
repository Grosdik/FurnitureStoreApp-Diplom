using FurnitureStoreApp.DataBase;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FurnitureStoreApp.Views.AddEditPage
{
    /// <summary>
    /// Логика взаимодействия для AddEditSupplierPage.xaml
    /// </summary>
    public partial class AddEditSupplierPage : Page
    {
        private Suppliers _currentSuppliers = new Suppliers();
        public AddEditSupplierPage(Suppliers selectedSuppliers)
        {
            InitializeComponent();
            if (selectedSuppliers != null)
            {
                _currentSuppliers = selectedSuppliers;
            }
            DataContext = _currentSuppliers;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentSuppliers.Company))
            {
                errors.AppendLine("Укажите название компании!");
            }
            if (string.IsNullOrWhiteSpace(_currentSuppliers.Phone))
            {
                errors.AppendLine("Укажите номер телефона компании!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentSuppliers.Id == 0)
            {
                App.connect.Suppliers.Add(_currentSuppliers);
            }

            try
            {
                App.connect.SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Transfer.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
