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

namespace Ишбулатов41размер
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private List<Product> allProducts;
        public ProductPage()
        {
           
            InitializeComponent();
            var currentProduct = Ishbulatov41Entities.GetContext().Product.ToList();

            allProducts = Ishbulatov41Entities.GetContext().Product.ToList();

            ProductListView.ItemsSource = currentProduct;
            ComboType.SelectedIndex = 0;
            UpdateProduct();

        }
        private void UpdateStatus()
        {
            int displayCount = ProductListView.Items.Count;
            int totalCount = allProducts.Count;
            CountTextBlock.Text = "кол-во " + displayCount + " из " + totalCount;
        }

        private void UpdateProduct()
        {
            var currentProduct = allProducts.ToList();
            if (ComboType.SelectedIndex == 0)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) <= 100)).ToList();
            }
            else if (ComboType.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) <= 9.99)).ToList();
            }
            else if (ComboType.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 10 && Convert.ToInt32(p.ProductDiscountAmount) <= 14.99)).ToList();
            }
            else if (ComboType.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 15 && Convert.ToInt32(p.ProductDiscountAmount) <= 100)).ToList();
            }
            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if(RButtonDown.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            else if (RButtonUp.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            ProductListView.ItemsSource = currentProduct;
            UpdateStatus();

        }       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }
    }
}
