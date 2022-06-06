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
    /// Логика взаимодействия для AddEditTypePage.xaml
    /// </summary>
    public partial class AddEditTypePage : Page
    {
        private TypeOfFurniture _currentType = new TypeOfFurniture();
        public AddEditTypePage(TypeOfFurniture selectedType)
        {
            InitializeComponent();
            if (selectedType != null)
            {
                _currentType = selectedType;
            }
            DataContext = _currentType;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentType.Name))
            {
                errors.AppendLine("Укажите вид мебели!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentType.Id == 0)
            {
                App.connect.TypeOfFurniture.Add(_currentType);
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
