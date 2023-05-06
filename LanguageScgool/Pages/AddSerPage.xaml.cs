using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddSerPage.xaml
    /// </summary>
    public partial class AddSerPage : Page
    {
        Service contextClientServive;
        public AddSerPage(Service clientService)
        {
            InitializeComponent();
            contextClientServive = clientService;
            DataContext = contextClientServive;
            CbClient.ItemsSource = App.db.Client.ToList();
            TbTimes.Text = DateTime.Now.ToString("t");
            DbStart.Text = DateTime.Now.ToString();
        }

        private void BtAddImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextClientServive.MainImage = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextClientServive;
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (CbClient.SelectedItem != null && DbStart.SelectedDate != null && DbStart.SelectedDate > DateTime.Now)
            {
                string times = DbStart.Text + " " + TbTimes.Text;

                ClientService clientService1 = new ClientService();
                clientService1.Service = contextClientServive;
                clientService1.Client = (CbClient.SelectedItem as Client);
                clientService1.StartTime = DateTime.Parse(times);
                App.db.ClientService.Add(clientService1);


                App.db.SaveChanges();
                NavigationService.Navigate(new ServicePage());
            }
            else
            {
                MessageBox.Show("Выбрать дату и клиента");
                return;
            }
        }
        private void BtCansel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage());
        }

        private void TbTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbInMinuts.Text = $"{contextClientServive.DurationInSeconds / 60} минут/ ";
        }

        private void TbDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbDiscInPr.Text = $" {contextClientServive.Discount * 100}%";
        }

        private void TbTimes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9:]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbTitle_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbCost_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9:]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbTime_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9:]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbDiscount_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9:]") == false)
            {
                e.Handled = true;
            }
        }
    }
}
