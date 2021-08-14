using DoctorApp.Models;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Repository;
using HealthCare.Service;
using PacientApp.ViewModels;
using SimpleInjector;
using System.ComponentModel;
using System.Windows;

namespace PacientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SimpleInjector.Container Container { get; set; }

        public App()
        {
            Container = new SimpleInjector.Container();

            Container.RegisterSingleton<MainVM>();

            Container.Register<HomeUCVM>();
            Container.Register<LoginUCVM>();
            Container.Register<HistoryUCVM>();
            Container.Register<AnalysisUCVM>();
            Container.Register<RegisterUCVM>();
            Container.Register<ProcedureUCVM>();
            Container.Register<DoctorRequestsUCVM>();


            Container.RegisterSingleton<Patient>();

            Container.RegisterSingleton<IMessenger, Messenger>();

            Container.RegisterSingleton<IFileService, FileService>();

            Container.RegisterSingleton<IRepository<Patient>, Repository<Patient>>();
        }

    }
}
