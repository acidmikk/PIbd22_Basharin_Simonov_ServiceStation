using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace ServiceStationViewMaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static MasterViewModel Master;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AuthorizationWindow authorizationWindow = Container.Resolve<AuthorizationWindow>();
            authorizationWindow.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IMasterStorage, MasterStorage>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
