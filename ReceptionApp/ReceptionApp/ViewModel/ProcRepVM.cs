using Newtonsoft.Json;
using PropertyChanged;using ReceptionApp.Model;using System;using System.Collections.ObjectModel;using System.IO;using System.Text.Json;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ProcRepVM:BaseVM
    {
        public ObservableCollection<Prossedur> ProcedureList { get; set; }
        public ProcRepVM()
        {
            ProcedureList = new ObservableCollection<Prossedur>();
            GetProcedures();
        }

        public void GetProcedures()
        {
            var procedures = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas")
                .ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Prossedur.json");
            var jsProcedures = JsonConvert.DeserializeObject<ObservableCollection<Prossedur>>(procedures);
            ProcedureList = jsProcedures;
        }
    }
}
