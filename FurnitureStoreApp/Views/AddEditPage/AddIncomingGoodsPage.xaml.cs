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
    /// Логика взаимодействия для AddIncomingGoodsPage.xaml
    /// </summary>
    public partial class AddIncomingGoodsPage : Page
    {
        private IncomingGoods _currentIncomingGood = new IncomingGoods();
        public AddIncomingGoodsPage(IncomingGoods selectedIncomingGoods)
        {
            InitializeComponent();
            if (selectedIncomingGoods != null)
            {
                _currentIncomingGood = selectedIncomingGoods;
            }
            DataContext = _currentIncomingGood;
            dPickerDate.Text = DateTime.Now.ToString();
            comboProducts.ItemsSource = App.connect.Products.ToList();
            comboSupplier.ItemsSource = App.connect.Suppliers.ToList();
        }
        private void quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void quantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void purchasePrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void purchasePrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            long amount = 0;
            long purPrice = 0;
            if (!string.IsNullOrWhiteSpace(quantity.Text) && !string.IsNullOrWhiteSpace(purchasePrice.Text))
            {
                amount = Convert.ToInt64(quantity.Text);
                purPrice = Convert.ToInt64(purchasePrice.Text);
            }

            deliveryСost.Text = (amount * purPrice).ToString();
        }

        private void purchasePrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            long amount = 0;
            long purPrice = 0;
            if (!string.IsNullOrWhiteSpace(quantity.Text) && !string.IsNullOrWhiteSpace(purchasePrice.Text))
            {
                amount = Convert.ToInt64(quantity.Text);
                purPrice = Convert.ToInt64(purchasePrice.Text);
            }

            deliveryСost.Text = (amount * purPrice).ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dPickerDate.Text))
            {
                DateTime date = DateTime.Parse(dPickerDate.Text);
                _currentIncomingGood.Date = DateTime.Parse(date.ToString("yyyy/MM/dd"));
            }
            else
            {
                errors.AppendLine("Укажите дату!");
            }
            if (_currentIncomingGood.Product == 1)
            {
                errors.AppendLine("Укажите нужный товар!");
            }
            if (_currentIncomingGood.Supplier == 1)
            {
                errors.AppendLine("Укажите поставщика товара!");
            }
            if (_currentIncomingGood.Amount < 1)
            {
                errors.AppendLine("Укажите количество поступившего товара!");
            }
            if (_currentIncomingGood.UnitPrice < 1)
            {
                errors.AppendLine("Укажите цену закупки товара!");
            }
            if (_currentIncomingGood.PurchaseAmount < 1)
            {
                errors.AppendLine("Укажите стоимость поставки продажи");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentIncomingGood.Id == 0)
            {
                App.connect.IncomingGoods.Add(_currentIncomingGood);
            }

            try
            {
                var temporaryVariable = comboProducts.SelectedItem as Products;
                var accrual = App.connect.Products.Find(temporaryVariable.Id);
                if (accrual != null)
                {
                    if (Convert.ToInt32(quantity.Text) <= 0)
                    {
                        MessageBox.Show("Введите количество поступивших товаров!");
                    }
                    else
                    {
                        accrual.Quantity += Convert.ToInt32(quantity.Text);
                    }
                }
                App.connect.Entry(accrual).State = System.Data.Entity.EntityState.Modified;
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
