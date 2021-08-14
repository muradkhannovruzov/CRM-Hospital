using DoctorApp.Messenging;
using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using DoctorApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DoctorApp.ViewModels
{
    class PInfoVM : ViewModelBase
    {
        private Messenger messenger;
        private ObservableCollection<Analysis> analysis;
        private ObservableCollection<Prossedur> prossedurs;
        private IProssedurServices prossedurServices;
        private IAnalysisServices analysisServices;
        private Analysis selectedAnalysis;
        private Prossedur selectedProssedur;

        public Prossedur SelectedProssedur { get => selectedProssedur; set => Set(ref selectedProssedur, value); }

        public Analysis SelectedAnalysis { get => selectedAnalysis; set => Set(ref selectedAnalysis, value); }


        public ObservableCollection<Prossedur> Prossedurs { get => prossedurs; set => Set(ref prossedurs, value); }
        public ObservableCollection<Analysis> Analysis { get => analysis; set => Set(ref analysis, value); }
        public PInfoVM(Messenger messenger, IProssedurServices prossedurServices, IAnalysisServices analysisServices)
        {
            this.analysisServices = analysisServices;
            this.prossedurServices = prossedurServices;
            this.messenger = messenger;
            messenger.Register<CurrentPatientChange>(this, x =>
            {
                Analysis = this.analysisServices.FindAnalyses(x.CrPatient?.HistoryBook.AnalyseResultsId) as ObservableCollection<Analysis>;
                Prossedurs = this.prossedurServices.FindProssedurs(x.CrPatient?.HistoryBook.ProssedursId) as ObservableCollection<Prossedur>;
            },true);
        }
    }
}
