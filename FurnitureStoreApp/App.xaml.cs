﻿using FurnitureStoreApp.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Furniture_storeEntities connect { get; } = new Furniture_storeEntities();
    }
}
