using GalaSoft.MvvmLight;
using DoctorApp.Models;
using DoctorApp.Messenging;
using DoctorApp.Services;

using DoctorApp.Repository;

using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;

namespace DoctorApp.ViewModels
{
    public class ProssedurWritingVM : ViewModelBase
    {
        private IRepository<DoctorRequest> DocReqRep;
        private IRepository<Patient> PatRep;
        private ProssedurCategories selectedProCat;
        private string name;
        private DateTime date = DateTime.Now;

        public RelayCommand AddCommand => addCommand ?? (addCommand = new RelayCommand(() =>
        {
            var request = new DoctorRequest() { DocName = Doctor.Name, RequestTime = Date, Category = SelectedDocCat };
            Patient.DoctorRequestId.Add(request.Id);
            DocReqRep.Add(request);
            PatRep.Update(x => { return x.Id == Patient.Id; }, Patient);
        }, () => Date >= DateTime.Now));

        private Messenger messanger;
        private RelayCommand addCommand;
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Date
        {
            get => date; set
            {
                if(value >= DateTime.Now) Set(ref date, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        public string Name { get => name; set => Set(ref name, value); }
        public ProssedurCategories SelectedDocCat { get => selectedProCat; set => Set(ref selectedProCat, value); }

        public ProssedurWritingVM(Messenger messenger, IRepository<Patient> patRep,IRepository<DoctorRequest> docReqRep)
        {
            DocReqRep = docReqRep;
            PatRep = patRep;
            this.messanger = messenger;
            this.messanger.Register<CurrentPatientChange>(this, x =>
             {
                 Patient = x.CrPatient;
             });
            this.messanger.Register<CurrentDrChange>(this, x =>
            {
                Doctor = x.CurrnetDr;
            });
        }
    }
}
