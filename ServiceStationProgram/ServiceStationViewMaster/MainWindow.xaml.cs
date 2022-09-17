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
using System.Windows.Shapes;
using Unity;

namespace ServiceStationViewMaster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuItemSpares_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<SparesWindow>();
            form.ShowDialog();
        }

        private void menuItemRepair_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<RepairsWindow>();
            form.ShowDialog();
        }

        private void menuItemWork_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WorksWindow>();
            form.ShowDialog();
        }

        private void menuItemGetList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemBindCurrency_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
