using FurnitureStoreApp.DataBase;
using FurnitureStoreApp.Utils;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private byte[] imageProduct;
        private Products _currentProduct = new Products();
        public AddEditProductPage(Products selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
            {
                _currentProduct = selectedProduct;
            }
            DataContext = _currentProduct;
            comboType.ItemsSource = App.connect.TypeOfFurniture.ToList();
        }

        private void btnAddPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.JPG, *.PNG,)|*.jpg;*.png;*.jpeg";
            if(fileDialog.ShowDialog() == true)
            {
                imageProduct = File.ReadAllBytes(fileDialog.FileName);
                imageProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(imageProduct);
                _currentProduct.Image = imageProduct;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentProduct.Name))
            {
                errors.AppendLine("Укажите название товара!");
            }
            if (_currentProduct.Cost < 1)
            {
                errors.AppendLine("Укажите стоимость товара!");
            }
            if (_currentProduct.Quantity < 1)
            {
                errors.AppendLine("Укажите количество товара!");
            }
            if (_currentProduct.TypeOfFurniture1 == null)
            {
                errors.AppendLine("Укажите тип мебели!");
            }
            if(_currentProduct.Image != null)
            {
                imageProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_currentProduct.Image);
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProduct.Id == 0)
            {
                App.connect.Products.Add(_currentProduct);
            }

            try
            {
                App.connect.SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Utils.Transfer.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
