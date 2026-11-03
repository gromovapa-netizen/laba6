using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        List<Product> products = new List<Product>();
        Product? selectedProduct = null;

        public MainWindow()
        {
            InitializeComponent();

            
            products.Add(new Product { Id = 1, Name = "Яблоки", Price = 370, Description = "Сочные яблоки", Count = 140, ImagePath = "apple.jpg" });
            products.Add(new Product { Id = 2, Name = "Виноград", Price = 370, Description = "Зеленый", Count = 270, ImagePath = "grapes.jpg" });

            RefreshList();
        }

        private void RefreshList()
        {
            ProductList.ItemsSource = null;
            ProductList.ItemsSource = products;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductList.SelectedItem != null)
            {
                selectedProduct = ProductList.SelectedItem as Product;
                DeleteButton.IsEnabled = true;
                EditButton.IsEnabled = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow win = new ProductWindow();
            win.Title = "Добавление товара";

            if (win.ShowDialog() == true)
            {
                Product newProd = win.EditableProduct;
                newProd.Id = products.Count + 1;
                products.Add(newProd);
                RefreshList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null) return;

            ProductWindow win = new ProductWindow();
            win.Title = "Редактирование товара";
            win.EditableProduct = selectedProduct;

            // Заполняем поля в новом окне текущими данными
            win.NameBox.Text = selectedProduct.Name;
            win.PriceBox.Text = selectedProduct.Price.ToString();
            win.DescBox.Text = selectedProduct.Description;
            win.CountBox.Text = selectedProduct.Count.ToString();
            win.ImageBox.Text = selectedProduct.ImagePath;

            if (win.ShowDialog() == true)
            {
                RefreshList();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                products.Remove(selectedProduct);
                selectedProduct = null;
                DeleteButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                RefreshList();
            }
        }
    }
}