using Newtonsoft.Json;
using PropertyChanged;
using ReceptionApp.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DevRepVM:BaseVM
    {
        public ObservableCollection<Device> DevicesList  { get; set; }
        public DevRepVM()
        {
            DevicesList = new ObservableCollection<Device>();
            GetDevices();
        }

        public void GetDevices()
        {
            var devices = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas")
                .ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Device.json");
            var jsDevices = JsonConvert.DeserializeObject<ObservableCollection<Device>>(devices);
            DevicesList = jsDevices;
        }
    }
}
