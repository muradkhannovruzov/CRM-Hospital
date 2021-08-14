using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PacientApp.Messaging;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
    public class HomeUCVM : ViewModelBase
    {
        readonly IMessenger messenger;

        public HomeUCVM(IMessenger messenger)
        {
            this.messenger = messenger;

            CurrentHomeVM = App.Container.GetInstance<AnalysisUCVM>();

            ProcedureCommand = new RelayCommand<object>(ProcedureCommandExecute);

            AnalysisCommand = new RelayCommand<object>(AnalysisCommandExecute);

            HistoryCommand = new RelayCommand<object>(HistoryCommandExecute);

            LogOutCommand = new RelayCommand<object>(LogOutCommandExecute);

            DoctorsRequestsCommand = new RelayCommand<object>(DoctorsRequestsCommandExecute);
        }

        public ICommand ProcedureCommand { get; private set; }

        public ICommand HistoryCommand { get; private set; }

        public ICommand AnalysisCommand { get; private set; }

        public ICommand LogOutCommand { get; private set; }

        public ICommand DoctorsRequestsCommand { get; set; }

        public ViewModelBase CurrentHomeVM { get; private set; }

        private void DoctorsRequestsCommandExecute(object obj)
        {
            CurrentHomeVM = App.Container.GetInstance<DoctorRequestsUCVM>();
        }


        private void AnalysisCommandExecute(object obj)
        {
            CurrentHomeVM = App.Container.GetInstance<AnalysisUCVM>();
        }

        private void HistoryCommandExecute(object obj)
        {
            CurrentHomeVM = App.Container.GetInstance<HistoryUCVM>();
        }

        private void ProcedureCommandExecute(object obj)
        {
            CurrentHomeVM = App.Container.GetInstance<ProcedureUCVM>();
        }

        private void LogOutCommandExecute(object obj)
        {
            messenger.Send(new VMChange()
            {
                ViewModel = App.Container.GetInstance<LoginUCVM>()
            });
        }
    }
}
