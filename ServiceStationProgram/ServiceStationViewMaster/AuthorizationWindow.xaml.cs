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
using System.Windows.Shapes;
using Unity;

namespace ServiceStationViewMaster
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private readonly IMasterLogic _logic;
        public AuthorizationWindow(IMasterLogic logic)
        {
            _logic = logic;
            InitializeComponent();
        }

        private void buttonLogIn_Click(object sender, RoutedEventArgs e)
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
            try
            {
                var list = _logic.Read(new MasterBindingModel
                {
                    Email = textBoxLogin.Text,
                    Password = textBoxPassword.Password
                });

                if (list.Count > 0 && list != null)
                {

                    App.Master = (list != null && list.Count > 0) ? list[0] : null;
                    var form = App.Container.Resolve<MainWindow>();
                    Close();
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("Ошибка входа");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<RegistrationWindow>();
            form.ShowDialog();
        }


    }
}
