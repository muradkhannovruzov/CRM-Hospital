using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Repository;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace PacientApp.ViewModels
{
    public class DoctorRequestsUCVM : ViewModelBase
    {
        private IRepository<Patient> PatientRepo;
        private IFileService FileService;
        public Patient CurrentUser { get; set; }
        private IMessenger Messenger;

       //public  MyProperty { get; set; }


        public DoctorRequestsUCVM(IRepository<Patient> repository, IFileService fileService, IMessenger messenger, Patient patient)
        {
            Messenger = messenger;
            PatientRepo = repository;
            FileService = fileService;
            CurrentUser = patient;


    }
}

}
