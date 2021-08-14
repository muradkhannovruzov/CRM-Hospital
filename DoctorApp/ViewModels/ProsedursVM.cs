using DoctorApp.Messenging;
using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows;
using DoctorApp.Services;
using DoctorApp.Repository;

namespace DoctorApp.ViewModels
{
    class ProsedursVM : ViewModelBase
    {
        public ObservableCollection<int> Indexes { get => indexes; set => Set(ref indexes, value); }
        public int DeviceIndex
        {
            get => deviceIndex; set
            {
                Set(ref deviceIndex, value);
                RemoveDevice.RaiseCanExecuteChanged();
            }
        }
        public int MedicineIndex
        {
            get => medicineIndex; set
            {
                Set(ref medicineIndex, value);
                RemoveMedicines.RaiseCanExecuteChanged();
            }
        }
        public int DozeOfCurrnet { get => dozeOfCurrnet; set => Set(ref dozeOfCurrnet, value); }
        public string TimeOfP { get => time1; set => Set(ref time1, value); }
        public Medicine Selected
        {
            get => selected; set
            {
                Set(ref selected, value);
                if (selected != null)
                {
                    DozeOfCurrnet = value.Count;
                }
                else
                {
                    DozeOfCurrnet = 0;
                }
            }
        }
        public int Index
        {
            get => index; set
            {
                Set(ref index, value);
                AddMedicines.RaiseCanExecuteChanged();
                AddRoom.RaiseCanExecuteChanged();
                AddDevice.RaiseCanExecuteChanged();
                AddTime.RaiseCanExecuteChanged();
                RemoveRoom.RaiseCanExecuteChanged();
                if (Index != -1)
                {
                    DocDevices = Doc.DoctorCanMake[Index].UsingDevice;
                    DocMedicines = Doc.DoctorCanMake[Index].UsingMedicine;
                    CurrentRoom = Doc.DoctorCanMake[Index].Room;
                    TimeOfP = Doc.DoctorCanMake[Index].TimeOfProsedur;
                }
                else
                {
                    DocDevices = new ObservableCollection<Device>();
                    DocMedicines = new ObservableCollection<Medicine>();
                    CurrentRoom = new Room();
                    TimeOfP = "";
                }
                RemoveProsedur.RaiseCanExecuteChanged();
                Set(ref index, value);
            }
        }
        public Doctor Doc { get => doc; set => Set(ref doc, value); }
        public Room CurrentRoom
        {
            get => room; set
            {
                Set(ref room, value);
            }
        }
        public string Time
        {
            get => time; set
            {
                Set(ref time, value);
                AddTime.RaiseCanExecuteChanged();
            }
        }
        public string Prosedur
        {
            get => prosedur; set
            {
                Set(ref prosedur, value);
                AddProsedur.RaiseCanExecuteChanged();
            }
        }
        public Device UsingDevices
        {
            get => usingDevices; set
            {
                Set(ref usingDevices, value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }
        public Medicine UsingMedicines
        {
            get => usingMedicines; set
            {
                Set(ref usingMedicines, value);
                AddMedicines.RaiseCanExecuteChanged();
            }
        }
        public string UsingDoze
        {
            get => usingDoze; set
            {
                Set(ref usingDoze, value);
                AddMedicines.RaiseCanExecuteChanged();
            }
        }
        public Room UsingRoom
        {
            get => usingRoom; set
            {
                Set(ref usingRoom, value);
                AddRoom.RaiseCanExecuteChanged();
            }
        }

        private List<MedicalProsedurs> prosedurs;
        private RelayCommand addDevice;
        private string prosedur;
        private Device usingDevices;
        private Medicine usingMedicines;
        private RelayCommand addMedicines;
        private RelayCommand addRoom;
        private string usingDoze;
        private Room usingRoom;
        private string time;
        private RelayCommand addTime;
        private RelayCommand addProsedur;
        private Doctor doc;
        private ObservableCollection<string> docProsedurs = new ObservableCollection<string>();
        private int index = -1;
        private ObservableCollection<Device> docDevices = new ObservableCollection<Device>();
        private ObservableCollection<Medicine> docMedicines = new ObservableCollection<Medicine>();
        private int dozeOfCurrnet;
        private Medicine selected;
        private Room room;
        private string time1;
        private int medicineIndex = -1;
        private RelayCommand removeProsedur;
        private RelayCommand removeDevice;
        private int deviceIndex = -1;
        private RelayCommand removeMedicines;
        private RelayCommand removeRoom;
        private ObservableCollection<int> indexes = new ObservableCollection<int>();
        private RelayCommand save;
        private List<Device> devices;
        private List<Medicine> medicine;
        private List<Room> rooms;

        public ObservableCollection<string> Doze { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Times { get; set; } = new ObservableCollection<string>();
        public List<Device> Devices { get => devices; set => Set(ref devices,value); }
        public List<Medicine> Medicine { get => medicine; set => Set(ref medicine, value); }
        public List<Room> Rooms { get => rooms; set => Set(ref rooms,value); }
        public List<MedicalProsedurs> Prosedurs { get => prosedurs; set => Set(ref prosedurs, value); }
        public ObservableCollection<string> DocProsedurs { get => docProsedurs; set => Set(ref docProsedurs, value); }
        public ObservableCollection<Device> DocDevices { get => docDevices; set => Set(ref docDevices, value); }
        public ObservableCollection<Medicine> DocMedicines { get => docMedicines; set => Set(ref docMedicines, value); }
        public IRepository<Medicine> MedicineService { get; set; }
        public IRepository<Device> DeviceService { get; set; }
        public IRepository<Room> RoomService { get; set; }
        public IRepository<Doctor> DoctorService { get; set; }
        public IRepository<MedicalProsedurs> MedicalPService { get; set; }
        public ProsedursVM(IRepository<Doctor> _DoctorService, Messenger messenger, IRepository<Medicine> _Medicine, IRepository<MedicalProsedurs> _MedcalP , IRepository<Device> _Device, IRepository<Room> _Room)
        {
            DoctorService = _DoctorService;
            MedicineService = _Medicine;
            DeviceService = _Device;
            RoomService = _Room;
            MedicalPService = _MedcalP;
            Doc = new Doctor();
            var Messenger = messenger;
            messenger.Register<CurrentDrChange>(this, x =>
            {
                DocMedicines = new ObservableCollection<Medicine>();
                DocDevices = new ObservableCollection<Device>();
                DocProsedurs = new ObservableCollection<string>();
                Doc = x.CurrnetDr;
                UsingRoom = Doc.DefaultRoom;
                foreach (var item in Doc.DoctorCanMake)
                {
                    DocProsedurs.Add(item.NameOfProsedur);
                }
            }, true);
            ComboBoxItems a = new ComboBoxItems();
            Times = a.GetTimeAndDoze();
            Doze = a.GetTimeAndDoze();
            Rooms = RoomService.GetAll() as List<Room>;
            Devices = DeviceService.GetAll() as List<Device>;
            Medicine = MedicineService.GetAll() as List<Medicine>;
            Prosedurs = MedicalPService.GetAll() as List<MedicalProsedurs>;

        }
        public RelayCommand AddDevice => addDevice ??= new RelayCommand(() =>
        {
            bool NotNew = true;
            foreach (var item in Doc.DoctorCanMake[Index].UsingDevice)
            {
                if (item == UsingDevices) NotNew = false;
            }
            if (NotNew)
            {
                Doc.DoctorCanMake[Index].UsingDevice.Add(UsingDevices);
            }
        }, () => UsingDevices != null && Index != -1);

        public RelayCommand AddRoom => addRoom ??= new RelayCommand(() =>
        {
            Doc.DoctorCanMake[Index].Room = UsingRoom;
            CurrentRoom = UsingRoom;
        }, () => UsingRoom != null && Index != -1);

        public RelayCommand AddMedicines => addMedicines ??= new RelayCommand(() =>
        {
            bool NotNew = true;
            int i = 0;
            foreach (var item in Doc.DoctorCanMake[Index].UsingMedicine)
            {
                if (item.Name == UsingMedicines.Name)
                {
                    NotNew = false;
                    MedicineIndex = i;
                    break;
                }
                i++;
            }
            if (NotNew)
            {
                UsingMedicines.Count = int.Parse(UsingDoze);
                Doc.DoctorCanMake[Index].UsingMedicine.Add(UsingMedicines);
            }
            else
            {
                DozeOfCurrnet = int.Parse(UsingDoze);
                Doc.DoctorCanMake[Index].UsingMedicine[MedicineIndex].Count = int.Parse(UsingDoze);
            }
        }, () => UsingMedicines != null && !string.IsNullOrWhiteSpace(UsingDoze) && Index != -1);
        public RelayCommand AddTime => addTime ??= new RelayCommand(() =>
        {
            Doc.DoctorCanMake[Index].TimeOfProsedur = Time;
            TimeOfP = Time;
        }, () => (!string.IsNullOrWhiteSpace(Time) && Index != -1));

        public RelayCommand AddProsedur => addProsedur ??= new RelayCommand(() =>
        {
            bool NotNew = true;
            foreach (var item in Doc.DoctorCanMake)
            {
                if (item.NameOfProsedur == Prosedur) NotNew = false;
            }
            if (NotNew)
            {
                Doc.DoctorCanMake.Add(new DoctorCanMake() { NameOfProsedur = Prosedur, TimeOfProsedur = "1", Room = Doc.DefaultRoom });
                DocProsedurs.Add(Prosedur);
            }
        }, () => !string.IsNullOrWhiteSpace(Prosedur));

        public RelayCommand RemoveProsedur => removeProsedur ??= new RelayCommand(() =>
        {
            Doc.DoctorCanMake.RemoveAt(Index);
            DocProsedurs.RemoveAt(Index);
            Index = -1;
        }, () => Index != -1);

        public RelayCommand RemoveDevice => removeDevice ??= new RelayCommand(() =>
        {
            Devices.RemoveAt(DeviceIndex);
            Doc.DoctorCanMake[Index].UsingDevice.RemoveAt(DeviceIndex);
            DeviceIndex = -1;
        }, () => DeviceIndex != -1);
        public RelayCommand RemoveMedicines => removeMedicines ??= new RelayCommand(() =>
        {
            Medicine.RemoveAt(MedicineIndex);
            Doc.DoctorCanMake[Index].UsingMedicine.RemoveAt(MedicineIndex);
            MedicineIndex = -1;
        }, () => MedicineIndex != -1);

        public RelayCommand RemoveRoom => removeRoom ??= new RelayCommand(() =>
        {
            CurrentRoom = Doc.DefaultRoom;
            Doc.DoctorCanMake[Index].Room = Doc.DefaultRoom;
        }, () => Index != -1);

        public RelayCommand Save => save ??= new RelayCommand(() =>
        {
            DoctorService.Update(x => { return x.Id == Doc.Id; }, Doc);
        });
    }
}
