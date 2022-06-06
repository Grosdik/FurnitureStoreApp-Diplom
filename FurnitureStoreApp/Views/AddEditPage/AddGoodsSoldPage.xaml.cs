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
    /// Логика взаимодействия для AddGoodsSoldPage.xaml
    /// </summary>
    public partial class AddGoodsSoldPage : Page
    {
        private GoodsSold _currentGoodsSold = new GoodsSold();
        public AddGoodsSoldPage(GoodsSold selectedGoodsSold)
        {
            InitializeComponent();
            if (selectedGoodsSold != null)
            {
                _currentGoodsSold = selectedGoodsSold;
            }
            DataContext = _currentGoodsSold;
            dPickerDate.Text = DateTime.Now.ToString();
            comboProducts.ItemsSource = App.connect.Products.ToList();
        }
        private void soldQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void soldQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private void soldQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal soldAmount = 0;
            decimal sellPrice = 0;
            if (!string.IsNullOrWhiteSpace(soldQuantity.Text) && !string.IsNullOrWhiteSpace(sellingPrice.Text))
            {
                soldAmount = Convert.ToDecimal(soldQuantity.Text);
                sellPrice = Convert.ToDecimal(sellingPrice.Text);
            }

            revenue.Text = (soldAmount * sellPrice).ToString();
        }

        private void comboProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sellingPrice.Text = Math.Round((comboProducts.SelectedItem as Products).Cost, 2).ToString();
        }

        private void sellingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal soldAmount = 0;
            decimal sellPrice = 0;
            if (!string.IsNullOrWhiteSpace(soldQuantity.Text) && !string.IsNullOrWhiteSpace(sellingPrice.Text))
            {
                soldAmount = Convert.ToDecimal(soldQuantity.Text);
                sellPrice = Convert.ToDecimal(sellingPrice.Text);
            }

            revenue.Text = Math.Round(soldAmount * sellPrice, 2).ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dPickerDate.Text))
            {
                DateTime date = DateTime.Parse(dPickerDate.Text);
                _currentGoodsSold.Date = DateTime.Parse(date.ToString("yyyy/MM/dd"));
            }
            else
            {
                errors.AppendLine("Укажите дату!");
            }
            if (_currentGoodsSold.Product == 1)
            {
                errors.AppendLine("Укажите нужный товар!");
            }
            if (_currentGoodsSold.Amount < 1)
            {
                errors.AppendLine("Укажите количество проданного товара!");
            }
            if (_currentGoodsSold.SellinPrice < 1)
            {
                errors.AppendLine("Укажите цену проданного товара!");
            }
            if (_currentGoodsSold.SumOfSale < 1)
            {
                errors.AppendLine("Укажите cумму продажи");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentGoodsSold.Id == 0)
            {
                App.connect.GoodsSold.Add(_currentGoodsSold);
            }

            try
            {
                var temporaryVariable = comboProducts.SelectedItem as Products;
                var writeOff = App.connect.Products.Find(temporaryVariable.Id);
                if (writeOff != null)
                {
                    if (Convert.ToInt32(soldQuantity.Text) <= 0)
                    {
                        MessageBox.Show("");
                    }
                    else if (Convert.ToInt32(soldQuantity.Text) > writeOff.Quantity)
                    {
                        MessageBox.Show($"Количество товаров на складе: {writeOff.Quantity}");
                        return;
                    }
                    else
                    {
                        writeOff.Quantity -= Convert.ToInt32(soldQuantity.Text);
                    }
                }
                App.connect.Entry(writeOff).State = System.Data.Entity.EntityState.Modified;
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
