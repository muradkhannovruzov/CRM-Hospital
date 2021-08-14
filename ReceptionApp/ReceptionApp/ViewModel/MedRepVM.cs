using Newtonsoft.Json;
using PropertyChanged;using ReceptionApp.Model;using System;using System.Collections.ObjectModel;using System.IO;using System.Text.Json;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MedRepVM : BaseVM
    {
        public ObservableCollection<Medicine> MedicinesList { get; set; }
        public MedRepVM()
        {
            MedicinesList = new ObservableCollection<Medicine>();
            GetMedicines();
        }

        private void GetMedicines()
        {
            var medicines = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas")
                .ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Medicine.json");
            var jsMedicines = JsonConvert.DeserializeObject<ObservableCollection<Medicine>>(medicines);
            MedicinesList = jsMedicines;
        }
    }
}
