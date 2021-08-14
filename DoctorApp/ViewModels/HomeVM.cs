using DoctorApp.Messenging;
using DoctorApp.Models;
using DoctorApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace DoctorApp.ViewModels
{
    class HomeVM : ViewModelBase
    {
        public void AddMedicine(Prossedur a)
        {
            SelectedMedicine = new ObservableCollection<string>();
            foreach (var item in a.UsingMedicine)
            {
                SelectedMedicine.Add(item.Name);
            }
        }
        public void AddDevice(Prossedur a)
        {
            SelectedDevice = new ObservableCollection<string>();
            foreach (var item in a.UsingDevice)
            {
                SelectedDevice.Add(item.Name);
            }
        }
        public int Index
        {
            get => index; set
            {
                Set(ref index, value);
                AddDevice(Old[Index]);
                AddMedicine(Old[Index]);
                Set(ref index, value);

            }
        }
        public ObservableCollection<Prossedur> Old { get; set; } = new ObservableCollection<Prossedur>();
        public Doctor Doctor { get => doctor; set => Set(ref doctor, value); }
        public ObservableCollection<Vote> votes { get => votes1; set => Set(ref votes1, value); }
        public List<OldPatient> OldPacients { get => oldPacients; set => Set(ref oldPacients, value); }
        private Messenger messenger;
        private IProssedurServices prossedurServices;
        private IPatientServices patientServices;
        private Doctor doctor;
        private ObservableCollection<Vote> votes1;
        private List<OldPatient> oldPacients = new List<OldPatient>();
        private int index;
        private ObservableCollection<string> selectedDevice = new ObservableCollection<string>();
        private ObservableCollection<string> selectedMedicine = new ObservableCollection<string>();

        public ObservableCollection<string> OldProsedurs { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> SelectedDevice { get => selectedDevice; set => Set(ref selectedDevice,value); }
        public ObservableCollection<string> SelectedMedicine { get => selectedMedicine; set => Set(ref selectedMedicine, value); }
        public HomeVM(Messenger messenger, IProssedurServices prossedurServices, IPatientServices patientServices)
        {
            this.messenger = messenger;
            this.prossedurServices = prossedurServices;
            this.patientServices = patientServices;
            Doctor = new Doctor();
            messenger.Register<CurrentDrChange>(this, x =>
            {
                SelectedMedicine = new ObservableCollection<string>();
                SelectedDevice = new ObservableCollection<string>();
                Doctor = x.CurrnetDr;
                votes = Doctor.Raiting.votes;
                OldPacients = Doctor.OldPacients;
                
                Old = prossedurServices.FindProssedurs(Doctor.OldProssedurId) as ObservableCollection<Prossedur>;
                foreach (var item in Old)
                {
                    try
                    {
                        OldProsedurs.Add(item.ToString()); 

                    }
                    catch (NotSupportedException)
                    {

                        break;
                    }
                    
                }
                if (OldProsedurs.Count >= 1) Index = 0;
            }, true);
        }
    }
}
