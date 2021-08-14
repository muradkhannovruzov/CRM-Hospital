using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using DoctorApp.Messenging;
using DoctorApp.Repository;
using DoctorApp.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Linq;
using DoctorApp.Models;

namespace DoctorApp.ViewModels
{
    class VoteVM : ViewModelBase
    {
        private Messenger messenger;
        private int vote;
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Prossedur Prossedur { get; set; }

        private IRepository<Doctor> DocRep;
        private IRepository<Patient> PatRep;
        private IRepository<Prossedur> ProRep;
        private IRepository<Room> RoomRep;
        private IRepository<Device> DevRep;

        public int Vote { get => vote; set
            {
                Set(ref vote, value);
                Thread thread = new Thread(UpdateText);
                thread.Start();
            }
        }
        private void UpdateText()
        {
            Thread.Sleep(1000);

            double mebleg = Convert.ToDouble(Prossedur.Price);


            Prossedur.Status = Status.End;
            Prossedur.Room.IsEmpty = true;
            if (Prossedur.UsingDevice != null)
            {
                foreach (var item in Prossedur.UsingDevice)
                {
                    item.IsBusy = true;
                    mebleg -= Convert.ToDouble(item.Profit);

                    DevRep.Update(x => { return x.Name == item.Name; }, item);
                }
            }
            if (Prossedur.UsingMedicine != null)
            {
                foreach (var item in Prossedur.UsingMedicine)
                {
                    mebleg -= Convert.ToDouble(item.Price) * item.Count;
                }
            }
            mebleg = mebleg * Doctor.MainProfitPercent / 100;
            Doctor.OldPacients.Add(new OldPatient() { Name = Patient.Name, Money = mebleg, DateTime = DateTime.Now });

            Patient.Raiting.AddVote(new Vote() { Mail = Doctor.Mail, StarPoint = Vote, Username = Doctor.Username });
            Patient.HistoryBook.ProssedursId.Add(Prossedur.Id);
            Patient.ProssedursId.Remove(Prossedur.Id);

            Doctor.OldProssedurId.Add(Prossedur.Id);
            Doctor.Pacients.Add(Patient.Id);
            Doctor.ProssedurId.Remove(Prossedur.Id);

            RoomRep.Update(x => { return x.Number == Prossedur.Room.Number; }, Prossedur.Room);


            DocRep.Update(x => { return x.Id == Doctor.Id; }, Doctor);
            PatRep.Update(x => { return x.Id == Patient.Id; }, Patient);
            ProRep.Update(x => { return x.Id == Prossedur.Id; }, Prossedur);
            messenger.Send(new CurrentDrChange() { CurrnetDr = Doctor });

            messenger.Send(new ViewChange() { ViewModel = App.Container.GetInstance<ProcessVM>(), x = false });
            vote = 0;
        }

        public VoteVM(Messenger messenger, IRepository<Doctor> docRep, IRepository<Patient> patRep, IRepository<Prossedur> proRep, IRepository<Room> roomRep, IRepository<Device> devRep)
        {

            DocRep = docRep;
            PatRep = patRep;
            DevRep = devRep;
            ProRep = proRep;
            RoomRep = roomRep;

            this.messenger = messenger;
            this.messenger.Register<CurrentDrChange>(this, x =>
            {
                Doctor = x.CurrnetDr;
            },true);
            this.messenger.Register<CurrentPatientChange>(this, x =>
            {
                Patient = x.CrPatient;
            }, true);
            this.messenger.Register<CurrentProssChange>(this, x =>
            {
                Prossedur = x.CurrentProssedur;
            },true);
        }
    }
}
