using Newtonsoft.Json;
using PropertyChanged;using ReceptionApp.Commands;
using ReceptionApp.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel:BaseVM
    {
        public ICommand AddPBtn { get; set; }
        public ICommand AddDBtn { get; set; }
        public ICommand DoctorTableBtn { get; set; }
        public ICommand PatientTableBtn { get; set; }
        public ICommand SubscriptionBtn { get; set; }
        public ICommand MedicineReposBtn { get; set; }
        public ICommand NotifBtn { get; set; }
        public ICommand RoomReposBtn { get; set; }
        public ICommand DeviceReposBtn { get; set; }
        public ICommand ProcedureReposBtn { get; set; }
        public ICommand GenerateBtn { get; set; }
        public ICommand SalaryBtn { get; set; }
        public ICommand HomeBtn { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
      
        public MainViewModel()
        {
            Patients = new ObservableCollection<Patient>();            
            AddPBtn = new RelayCommand(x=> { CurrentViewModel = App.container.GetInstance<AddPAtientVM>(); });
            AddDBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<AddDoctorVM>(); });
            DoctorTableBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<DoctorsTableVM>(); });
            PatientTableBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<PatientTableVM>(); });
            SubscriptionBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<SubscVM>(); });
            MedicineReposBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<MedRepVM>(); });
            RoomReposBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<RoomRepVM>(); });
            DeviceReposBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<DevRepVM>(); });
            ProcedureReposBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<ProcRepVM>(); });
            GenerateBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<GenerUserPasswVM>(); });
            SalaryBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<DoctorSalaryVM>(); });
            HomeBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<HomeVM>(); });
            NotifBtn = new RelayCommand(x => { CurrentViewModel = App.container.GetInstance<AddPAtientVM>(); });
        }
    }
}
