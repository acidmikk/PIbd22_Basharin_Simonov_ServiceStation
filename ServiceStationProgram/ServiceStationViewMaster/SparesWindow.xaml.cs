using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.ViewModels;
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
    /// Логика взаимодействия для SparesWindow.xaml
    /// </summary>
    public partial class SparesWindow : Window
    {
        private readonly ISparesLogic _logic;
        public SparesWindow(ISparesLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var form = App.Container.Resolve<SparesChangeWindow>();
                form.Id = Convert.ToInt32(((SparesViewModel)dataGrid.SelectedItem).Id);
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id =
                   Convert.ToInt32(((SparesViewModel)dataGrid.SelectedItem).Id);
                    try
                    {
                        _logic.Delete(new SparesBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _logic.Read(new SparesBindingModel { MasterId = App.Master.Id });
                if (list != null)
                {
                    dataGrid.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

            var form = App.Container.Resolve<SparesChangeWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }

        }
    }
}
