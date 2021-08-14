using Newtonsoft.Json;using PropertyChanged;using ReceptionApp.MediatorClass;using ReceptionApp.Messages;using ReceptionApp.Model;using System.Collections.ObjectModel;
using System.IO;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class PatientTableVM:BaseVM
    {
        public ObservableCollection<Patient> Patients { get; set; }        
        public PatientTableVM()
        {            
            Patients = new ObservableCollection<Patient>();
                       
            var patients = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Patient.json");
            var jspatients = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(patients);
            Patients = jspatients;            
        }
    }
}
