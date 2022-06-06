﻿using System;
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

namespace FurnitureStoreApp.Views.PrintPage
{
    /// <summary>
    /// Логика взаимодействия для PrintIncomingGoodsPage.xaml
    /// </summary>
    public partial class PrintIncomingGoodsPage : Page
    {
        public PrintIncomingGoodsPage()
        {
            InitializeComponent();
            dGridIncomingGoods.ItemsSource = App.connect.IncomingGoods.ToList();
        }
    }
}
