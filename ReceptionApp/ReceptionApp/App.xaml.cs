using ReceptionApp.MediatorClass;
using ReceptionApp.ViewModel;
using SimpleInjector;
using System.Windows;

namespace ReceptionApp
{    
    public partial class App : Application
    {
        public static Container container;
        public App()
        {
            container = new Container();
            container.RegisterSingleton<Mediator>();
            container.RegisterSingleton<AddDoctorVM>();
            container.RegisterSingleton<AddPAtientVM>();
            container.Register<DoctorsTableVM>();
            container.Register<PatientTableVM>();
            container.RegisterSingleton<MainViewModel>();
            container.RegisterSingleton<SubscVM>();
            container.RegisterSingleton<MedRepVM>();
            container.RegisterSingleton<RoomRepVM>();
            container.RegisterSingleton<DevRepVM>();
            container.RegisterSingleton<ProcRepVM>();
            container.Register<GenerUserPasswVM>();
            container.RegisterSingleton<DoctorSalaryVM>();
            container.RegisterSingleton<HomeVM>();
        }
    }
}
