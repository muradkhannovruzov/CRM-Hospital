using DoctorApp.Messenging;
using DoctorApp.Models;
using DoctorApp.Repository;
using DoctorApp.Validation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DoctorApp.ViewModels
{
    class ProfileVM : ViewModelBase, IDataErrorInfo
    {
        private string room = "34";
        private RelayCommand add;
        private string name;
        private string surname;
        private string mail;
        private string number;
        private RelayCommand cancel;
        private string password;

        public string Name
        {
            get => name; set
            {
                Set(ref name, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string Password { get => password; set
            {
                Set(ref password, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string Surname
        {
            get => surname; set
            {
                Set(ref surname, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string Mail
        {
            get => mail; set
            {
                Set(ref mail, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public string Number
        {
            get => number; set
            {
                Set(ref number, value);
                Add.RaiseCanExecuteChanged();
            }
        }
        public Doctor Doc { get; set; } = new Doctor();
        public string Room { get => room; set => Set(ref room, value); }
        public List<string> RoomNumbers { get; set; } = new List<string>();
        public IRepository<Room> RoomService { get; set; }
        public IRepository<Doctor> DoctorService { get; set; }
        private Messenger messenger;
        public ProfileVM(IRepository<Room> _RoomService, IRepository<Doctor> _DoctorService)
        {
            RoomService = _RoomService;
            DoctorService = _DoctorService;
            messenger = App.Container.GetInstance<Messenger>();
            messenger.Register<CurrentDrChange>(this, message =>
            {
                Doc = message.CurrnetDr;
                Name = Doc.Name;
                Surname = Doc.Surname;
                Mail = Doc.Mail;
                Number = Doc.Number;
                Password = Doc.Password;
                Room = Doc.DefaultRoom.Number.ToString();
            });
            foreach (var item in RoomService.GetAll() as List<Room>)
            {
                RoomNumbers.Add(item.Number.ToString() + " " + item.RoomType.ToString());
            }

        }
        public RelayCommand Cancel => cancel ?? new RelayCommand(() =>
            {
                Name = Doc.Name;
                Surname = Doc.Surname;
                Number = Doc.Number;
                Mail = Doc.Mail;
                Room = Doc.DefaultRoom.Number.ToString();
            });
        public RelayCommand Add => add ??= new RelayCommand(() =>
        {
            Doc.Name = Name;
            Doc.Surname = Surname;
            Doc.Number = Number;
            Doc.Mail = Mail;
            Doc.DefaultRoom.Number = int.Parse(Room.Split(' ')[0]);
            Doc.Password = Password;
            DoctorService.Update(x => { return x.Id == Doc.Id; }, Doc);
            messenger.Send(new CurrentDrChange() { CurrnetDr = Doc });
        }, () =>
        {
            return Mail.Length > 5 && Surname.Length > 2 && Name.Length > 2 && Number.Length > 3;
        });
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                var validator = new ProfileValidation();
                var result = validator.Validate(this);
                if (!result.IsValid)
                {
                    var error = result.Errors.FirstOrDefault(x => x.PropertyName.Contains(columnName));
                    if (error != null)
                        return error.ErrorMessage;
                }
                return string.Empty;
            }
        }
    }
}
