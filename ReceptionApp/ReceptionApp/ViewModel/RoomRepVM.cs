using Newtonsoft.Json;using PropertyChanged;using ReceptionApp.Model;using System;using System.Collections.ObjectModel;using System.IO;using System.Text.Json;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class RoomRepVM:BaseVM
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public RoomRepVM()
        {
            RoomList = new ObservableCollection<Room>();
            GetRooms();
        }

        private void GetRooms()
        {
            var rooms = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas")
                .ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Room.json");
            var jsRooms = JsonConvert.DeserializeObject<ObservableCollection<Room>>(rooms);
            RoomList = jsRooms;           
        }
    }
}
