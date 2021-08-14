using Newtonsoft.Json;
using PropertyChanged;
using ReceptionApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DoctorSalaryVM : BaseVM
    {        
        public ObservableCollection<Prossedur> DoctorSalaries { get; set; }
        public DoctorSalaryVM()
        {
            DoctorSalaries = new ObservableCollection<Prossedur>();

            CalculateDoctorSalary();            
        }

        private void CalculateDoctorSalary()
        {
            var procedures = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Prossedur.json");

            var jsProcedures = JsonConvert.DeserializeObject<ObservableCollection<Prossedur>>(procedures);
            if (jsProcedures != null)
            {
                foreach (var procedure in jsProcedures)
                {
                    procedure.DoctorProfit += procedure.Price * 0.2m;
                }
                 DoctorSalaries = jsProcedures;
            }

        }
    }
}
