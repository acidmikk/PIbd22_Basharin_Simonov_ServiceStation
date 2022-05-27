using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using ServiceStationDatabaseImplement.Implements;
using ServiceStationBusinessLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using ServiceStationContracts.BusinessLogicsContracts;

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
            currentContainer.RegisterType<ICarStorage, CarStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IInspectorStorage, InspectorStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDefectStorage, DefectStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRepairStorage, RepairStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISparesStorage, SparesStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITechnicalMaintenanceStorage, TechnicalMaintenanceStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkStorage, WorkStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMasterLogic, MasterLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarLogic, CarLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IInspectorLogic, InspectorLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDefectLogic, DefectLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRepairLogic, RepairLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISparesLogic, SparesLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITechnicalMaintenanceLogic, TechnicalMaintenanceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkLogic, WorkLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
