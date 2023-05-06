
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
using LanguageScgool.Model;

namespace LanguageScgool.Pages
{
    /// <summary>
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        IEnumerable<Service> filterProductAll = App.db.Service.Where(x => x.IsDelete != true).ToList();
        public ServicePage()
        {
            InitializeComponent();
            InitializeComponent();
            if (App.Admin == true)
            {
                BtAddServ.Visibility = Visibility.Visible;
                BtServList.Visibility = Visibility.Visible;
            }
            else
            {
                BtAddServ.Visibility = Visibility.Collapsed;
                BtServList.Visibility = Visibility.Collapsed;
            }
            CbDiscount.SelectedIndex = 0;
            CbSort.SelectedIndex = 0;
            Update();
            TbPages.Text = $" {App.db.Service.Where(x => x.IsDelete != true).Count()} из {App.db.Service.Count()} ";
        }
        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new EditSerPages(select));
        }
        private void DellBt_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Service;
            if (App.db.ClientService.FirstOrDefault(x => x.ServiceID == select.ID) != null)
            {
                MessageBox.Show("Удаление услуги невозможно, имеются данные");
                return;
            }
            select.IsDelete = true;
            App.db.SaveChanges();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            IEnumerable<Service> filterProduct = App.db.Service.Where(x => x.IsDelete != true).ToList();
            if (CbSort.SelectedIndex == 1)
                filterProduct = filterProduct.OrderBy(x => x.CostDisc);
            else if (CbDiscount.SelectedIndex == 2)
                filterProduct = filterProduct.OrderByDescending(x => x.CostDisc);
            if (CbDiscount.SelectedIndex > 0)
            {

                if (CbDiscount.SelectedIndex == 1)
                    filterProduct = filterProduct.Where(x => x.Discount >= 0 && x.Discount < 0.05 || x.Discount == null).ToList();
                else if (CbDiscount.SelectedIndex == 2)
                    filterProduct = filterProduct.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                else if (CbDiscount.SelectedIndex == 3)
                    filterProduct = filterProduct.Where(x => x.Discount >= 0.15 && x.Discount < 0.30).ToList();
                else if (CbDiscount.SelectedIndex == 4)
                    filterProduct = filterProduct.Where(x => x.Discount >= 0.30 && x.Discount < 0.70).ToList();
                else if (CbDiscount.SelectedIndex == 5)
                    filterProduct = filterProduct.Where(x => x.Discount >= 0.70 && x.Discount < 1).ToList();

                  
            }
            if (TbSelect.Text.Length > 0)
            {
                filterProduct = filterProduct.Where(x => x.Title.ToLower().StartsWith(TbSelect.Text.ToLower()) || x.Description.ToLower().StartsWith(TbSelect.Text.ToLower()));
            }
            LvSecv.ItemsSource = filterProduct.ToList();
            string inpageas = filterProduct.Count().ToString();
            TbPages.Text = $"{inpageas} из {filterProductAll.Count()}";
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CbDiscount_SelectionChanged(object sender,  SelectionChangedEventArgs e)
        {
            Update();
        }
         
        private void BtAddServ_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new EditSerPages(new Service()));
        }

        private void BrAddClintInServ_Click(object sender, RoutedEventArgs e)
        {

            var select = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new AddSerPage(select));
        }
        private void BtServList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPage());
        }
    }
}
