using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
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
using Unity;

namespace ServiceStationViewMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly IMasterLogic _logic;
        public RegistrationWindow(IMasterLogic logic)
        {
            _logic = logic;
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните логин", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Password))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxFio.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new MasterBindingModel
                {
                    Email = textBoxLogin.Text,
                    Password = textBoxPassword.Password,
                    MasterFIO = textBoxFio.Text
                });

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}
