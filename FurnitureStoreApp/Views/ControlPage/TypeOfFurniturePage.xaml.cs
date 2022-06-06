using FurnitureStoreApp.DataBase;
using FurnitureStoreApp.Utils;
using FurnitureStoreApp.Views.AddEditPage;
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
    /// Логика взаимодействия для TypeOfFurniturePage.xaml
    /// </summary>
    public partial class TypeOfFurniturePage : Page
    {
        private List<TypeOfFurniture> TypeOfFurniture;
        public TypeOfFurniturePage()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddEditTypePage((sender as Button).DataContext as TypeOfFurniture));
        }

        private void btnAddTypeOfFurniture_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AddEditTypePage(null));
        }

        private void btnDeleteTypeOfFurniture_Click(object sender, RoutedEventArgs e)
        {
            var typeForRemoving = dGridTypeOfFurniture.SelectedItems.Cast<TypeOfFurniture>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {typeForRemoving.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    App.connect.TypeOfFurniture.RemoveRange(typeForRemoving);
                    App.connect.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dGridTypeOfFurniture.ItemsSource = App.connect.TypeOfFurniture.ToList();
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
                dGridTypeOfFurniture.ItemsSource = App.connect.TypeOfFurniture.ToList();
            }
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGridTypeOfFurniture.ItemsSource = App.connect.TypeOfFurniture.ToList().Where(p => p.Name.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }
    }
}
