using Newtonsoft.Json;using PropertyChanged;using ReceptionApp.MediatorClass;using ReceptionApp.Messages;using ReceptionApp.Model;using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DoctorsTableVM : BaseVM
    {
        public ObservableCollection<Doctor> DoctorWorkList { get; set; }        
        public DoctorsTableVM()
        {            
            DoctorWorkList = new ObservableCollection<Doctor>();    
            
            var doctors= File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Doctor.json");
            var jsDoctors = JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(doctors);
            DoctorWorkList = jsDoctors;
            if (DoctorWorkList != null)
            {
                foreach (var item in DoctorWorkList)
                {
                    int a = item.Schedule.ScheduleElements.Count;
                    for (int i = 0; i < a-7; i++)
                    {
                        item.Schedule.ScheduleElements.RemoveAt(0);
                    }
                }
            }
        }
    }
}
