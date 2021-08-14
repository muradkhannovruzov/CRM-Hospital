using DoctorApp.Messenging;
using DoctorApp.Models;
using DoctorApp.Repository;
using DoctorApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace DoctorApp.ViewModels
{
    class ScheduleVM : ViewModelBase
    {
        private string hourOfStartDay;
        private string hourOfEndDay;
        private string minuteOfStartBreak;
        private string minuteOfEndBreak;
        private int index = 0;
        private RelayCommand add;
        private string hourOfStartBreak;
        private string hourOfEndBreak;
        private bool offDay;

        public bool OffDay
        {
            get => offDay; set
            {
                Set(ref offDay, value);
                Add.RaiseCanExecuteChanged();
            }
        }

        public string HourOfStartDay
        {
            get => hourOfStartDay; set
            {
                Set(ref hourOfStartDay, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string HourOfEndDay
        {
            get => hourOfEndDay; set
            {
                Set(ref hourOfEndDay, value);
                Add.RaiseCanExecuteChanged();

            }
        }

        public string MinuteOfStartBreak
        {
            get => minuteOfStartBreak; set
            {
                Set(ref minuteOfStartBreak, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string MinuteOfEndBreak
        {
            get => minuteOfEndBreak; set
            {
                Set(ref minuteOfEndBreak, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string HourOfStartBreak
        {
            get => hourOfStartBreak; set
            {
                Set(ref hourOfStartBreak, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string HourOfEndBreak
        {
            get => hourOfEndBreak; set
            {
                Set(ref hourOfEndBreak, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public int Index
        {
            get => index; set
            {
                Set(ref index, value);
                OffDay = Doc.Schedule.ScheduleElements[Index].OffDay;
                HourOfStartDay = Doc.Schedule.ScheduleElements[Index].BeginTime.Hours.ToString();
                HourOfEndDay = Doc.Schedule.ScheduleElements[Index].EndTime.Hours.ToString();
                HourOfStartBreak = Doc.Schedule.ScheduleElements[Index].BreakBeginTime.Hours.ToString();
                HourOfEndBreak = Doc.Schedule.ScheduleElements[Index].BreakEndTime.Hours.ToString();
                MinuteOfStartBreak = Doc.Schedule.ScheduleElements[Index].BreakBeginTime.Minutes.ToString();
                MinuteOfEndBreak = Doc.Schedule.ScheduleElements[Index].BreakEndTime.Minutes.ToString();
            }
        }

        public Doctor Doc { get; set; }
        public ObservableCollection<string> Hours { get; set; }
        public ObservableCollection<string> Minites { get; set; }
        IRepository<Doctor> DoctorService;
        public ScheduleVM(IRepository<Doctor> _DoctorService)
        {
            DoctorService = _DoctorService;
            var messenger = App.Container.GetInstance<Messenger>();
            messenger.Register<CurrentDrChange>(this, message =>
            {
                Doc = message.CurrnetDr;
            });
            ComboBoxItems a = new ComboBoxItems();
            Hours = new ObservableCollection<string>();
            Minites = new ObservableCollection<string>();
            Minites = a.GetMinutes();
            Hours = a.GetHours();
            Doc = new Doctor();
        }
        public RelayCommand Add => add ??= new RelayCommand(() =>
        {
            if (OffDay)
            {
                Doc.Schedule.ScheduleElements[Index].OffDay = true;
            }
            if ((!OffDay) && ((!string.IsNullOrWhiteSpace(MinuteOfEndBreak) && !string.IsNullOrWhiteSpace(MinuteOfStartBreak)) &&
                 (!string.IsNullOrWhiteSpace(HourOfEndBreak) && !string.IsNullOrWhiteSpace(HourOfStartBreak))))
            {
                Doc.Schedule.ScheduleElements[Index].BreakBeginTime = new TimeSpan(int.Parse(HourOfStartBreak), int.Parse(MinuteOfStartBreak), 0);
                Doc.Schedule.ScheduleElements[Index].BreakEndTime = new TimeSpan(int.Parse(HourOfEndBreak), int.Parse(MinuteOfEndBreak), 0);
            }
            if ((!OffDay) && (!string.IsNullOrWhiteSpace(HourOfEndDay) && !string.IsNullOrWhiteSpace(HourOfStartDay)))
            {
                Doc.Schedule.ScheduleElements[Index].BeginTime = new TimeSpan(int.Parse(HourOfStartDay), 0, 0);
                Doc.Schedule.ScheduleElements[Index].EndTime = new TimeSpan(int.Parse(HourOfEndDay), 0, 0);
            }
            DoctorService.Update(x => { return x.Id == Doc.Id; }, Doc);
        }, () => (OffDay) || (((!string.IsNullOrWhiteSpace(MinuteOfEndBreak) && !string.IsNullOrWhiteSpace(MinuteOfStartBreak)) &&
        (!string.IsNullOrWhiteSpace(HourOfEndBreak) && !string.IsNullOrWhiteSpace(HourOfStartBreak))) ||
        (!string.IsNullOrWhiteSpace(HourOfEndDay) && !string.IsNullOrWhiteSpace(HourOfStartDay))));
    }
}
