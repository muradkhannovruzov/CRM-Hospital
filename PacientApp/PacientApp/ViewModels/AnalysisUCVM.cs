using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Models;
using HealthCare.Repository;
using HealthCare.Service;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PacientApp.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using static HealthCare.Models.Analyses;

namespace PacientApp.ViewModels
{

    public class AnalysisUCVM : ViewModelBase
    {
        readonly IRepository<Patient> repository;
        private IRepository<Analysis> AnalysisRepo;

        readonly IFileService fileService;

        readonly IMessenger messenger;

        readonly Patient patient;

        public ICommand AddAnalysisCommand { get; private set; }

        public ICommand GetAnalysisCommand { get; private set; }

        public ICommand HomeCommand { get; private set; }

        public ICommand SelectedCommand { get; set; }

        public List<Analysis> UserAnalises { get; set; }

        public List<string> Analyses { get; private set; }

        public Analyses SelectedAnalyses { get; set; }

        public AnalysisUCVM(IRepository<Patient> repository, IFileService fileService, IMessenger messenger, Patient patient)
        {
            this.repository = repository;

            this.fileService = fileService;

            this.messenger = messenger;

            this.patient = patient;

            AnalysisRepo = new Repository<Analysis>(this.fileService);

            UserAnalises = new List<Analysis>();

            HomeCommand = new RelayCommand<object>(HomeCommandExecute);

            SelectedCommand = new RelayCommand<object>(SelectedCommandExecute);

            AddAnalysisCommand = new RelayCommand<object>(AddAnalysisCommandExecute);

            GetAnalysisCommand = new RelayCommand<object>(GetAnalysisCommandExecute);
        }

        private void HomeCommandExecute(object obj)
        {
            messenger.Send(new VMChange() { ViewModel = App.Container.GetInstance<RegisterUCVM>() });
        }

        private void SelectedCommandExecute(object obj)
        {
            SelectedAnalyses = (obj as Analyses);
        }

        private void AddAnalysisCommandExecute(object obj)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Json file (*.json)|*.json"
            };

            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    //var analyses = fileService.JsonFileRead<List<Analysis>>(saveFileDialog.FileName);

                    //if (analyses != null || analyses.Count == 0)
                    //{

                    //    if (patient.AnalysesId == null)
                    //    {
                    //        patient.AnalysesId = new List<string>();
                    //    }
                    //    UserAnalises = new List<Analysis>();
                    //    UserAnalises.AddRange(analyses.Where(x => x.Id == patient.Id));
                    //foreach (var item in UserAnalises)
                    //{
                    //    patient.AnalysesId.Add(item.Id);
                    //}




                    //    repository.Update(p => p.Username == patient.Username && p.Password == patient.Password, patient);

                    //}
                }
            }
            catch (System.Exception) { }

        }

        private void GetAnalysisCommandExecute(object obj)
        {

            var analyses = AnalysisRepo.GetAll().ToList();

            if (analyses != null || analyses.Count == 0)
            {
                UserAnalises = new List<Analysis>();
                UserAnalises.AddRange(analyses.Where(x => x.Id == patient.Id));

            }

        }
    }
}

