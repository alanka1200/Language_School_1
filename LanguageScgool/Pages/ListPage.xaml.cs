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
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        IEnumerable<ClientService> filterProduct = App.db.ClientService.ToList();
        public ListPage()
        {
            InitializeComponent();
            IEnumerable<ClientService> products = App.db.ClientService.OrderByDescending(x => x.StartTime).ToList();
            LvSecv.ItemsSource = products.Where(x => x.StartTime < DateTime.Now + TimeSpan.FromDays(2) && x.StartTime> DateTime.Now.Date).ToList();
            TbPages.Text = $" {App.db.ClientService.Where(x => x.StartTime > DateTime.Now).Count()} из {App.db.ClientService.Count()} ";
        }

        private void BtServList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage());
        }
    }
}
