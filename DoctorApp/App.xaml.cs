using DoctorApp.Messenging;
using DoctorApp.ViewModels;
using DoctorApp.Repository;
using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DoctorApp.Models;
using DoctorApp.Services;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;

namespace DoctorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public class RoomumsuBisey
    {
        public int Number { get; set; }
        public bool IsEmpty { get; set; } = true;
        public TypeOfRoom RoomType { get; set; }
    }
    public partial class App : Application
    {
        public static Container Container { get; set; }
        public App()
        {
            Container = new Container();
            Container.RegisterSingleton<ProfileVM>();
            Container.RegisterSingleton<ScheduleVM>();
            Container.RegisterSingleton<MainWindowVM>();
            Container.RegisterSingleton<ProsedursVM>();
            Container.RegisterSingleton<HomeVM>();
            Container.RegisterSingleton<ProcessVM>();
            Container.RegisterSingleton<LoginVM>();
            Container.Register<StartProsedurVM>();
            Container.RegisterSingleton<PInfoVM>();
            Container.RegisterSingleton<PAnalysisVM>();
            Container.RegisterSingleton<PDiagnosisVM>();
            Container.RegisterSingleton<PRecipeVM>();
            Container.RegisterSingleton<ProssedurWritingVM>();
            Container.RegisterSingleton<VoteVM>();

            Container.RegisterSingleton<IFileService, FileService>();
            Container.RegisterSingleton<IDoctorServices, DoctorServices>();
            Container.RegisterSingleton<IProssedurServices, ProssedurServices>();
            Container.RegisterSingleton<IAnalysisServices, AnalysisServices>();
            Container.RegisterSingleton<IPatientServices, PatientServices>();
            Container.RegisterSingleton<IRepository<Doctor>, Repository<Doctor>>();
            Container.RegisterSingleton<IRepository<Patient>, Repository<Patient>>();
            Container.RegisterSingleton<IRepository<Prossedur>, Repository<Prossedur>>();
            Container.RegisterSingleton<IRepository<Medicine>, Repository<Medicine>>();
            Container.RegisterSingleton<IRepository<DoctorRequest>, Repository<DoctorRequest>>();
            Container.RegisterSingleton<IRepository<Analysis>, Repository<Analysis>>();
            Container.RegisterSingleton<IRepository<Room>, Repository<Room>>();
            Container.RegisterSingleton<IRepository<Device>, Repository<Device>>();
            Container.RegisterSingleton<IRepository<MedicalProsedurs>, Repository<MedicalProsedurs>>();

            Container.RegisterSingleton<Messenger>();


            //Doctor d1 = new Doctor()
            //{
            //    Name = "Muradxan",
            //    Surname = "Novruzov",
            //    MainProfitPercent = 20,
            //    Username = "a",
            //    Password = "a",
            //    BirthDate = new DateTime(1999, 09, 26),
            //    Number = "0552816108",
            //    Mail = "muradxan46@gmail.com",
            //    DefaultRoom = new Room() { Number = 12, RoomType = TypeOfRoom.Dental }
            //};

            //Doctor d2 = new Doctor()
            //{
            //    Name = "Samur",
            //    Surname = "Aganiyev",
            //    MainProfitPercent = 20,
            //    Username = "s",
            //    Password = "s",
            //    BirthDate = new DateTime(200, 09, 26),
            //    Number = "0552816108",
            //    Mail = "samur@gmail.com",
            //    DefaultRoom = new Room() { Number = 11, RoomType = TypeOfRoom.Dental }
            //};
            //Patient p1 = new Patient()
            //{
            //    Name = "Eli",

            //    Surname = "Veliyev",
            //    BirthDate = new DateTime(1968, 1, 19),
            //    Mail = "eli@mail.ru",
            //    Number = "0503654512",
            //    Password = "eli",
            //    Username = "eli"
            //};
            //Patient p2 = new Patient()
            //{
            //    Name = "Veli",

            //    Surname = "Eliyev",
            //    BirthDate = new DateTime(1968, 1, 19),
            //    Mail = "veli@mail.ru",
            //    Number = "0503654512",
            //    Password = "veli",
            //    Username = "veli"
            //};


            //Prossedur pr1 = new Prossedur()
            //{
            //    PatientDescription = "Disim agriyir",
            //    Categoria = ProssedurCategories.Rehabilitative,
            //    DrName = "Muradxan",
            //    PatientId = p1.Id,
            //    Status = Status.Waiting,
            //    DateBegin = DateTime.Now,
            //    DateEnd = new DateTime(2021, 1, 25, 23, 45, 00),
            //    PatientName = p1.Name,
            //    PatientSurname = p1.Surname,
            //    Price = 200,
            //    Room = new Room() { Number = 1, RoomType = TypeOfRoom.Dental },

            //};

            //Prossedur pr2 = new Prossedur()
            //{
            //    PatientDescription = "Disim agriyir",
            //    Categoria = ProssedurCategories.Rehabilitative,
            //    DrName = "Muradxan",
            //    PatientId = p1.Id,
            //    Status = Status.Waiting,
            //    DateBegin = new DateTime(2021, 1, 25, 23, 18, 00),
            //    DateEnd = new DateTime(2021, 1, 25, 23, 59, 00),
            //    PatientName = p1.Name,
            //    PatientSurname = p1.Surname,
            //    Price = 300,
            //    Room = new Room() { Number = 1, RoomType = TypeOfRoom.Dental },

            //};
            //Prossedur hpr1 = new Prossedur()
            //{
            //    PatientDescription = "Disim agriyir",
            //    Categoria = ProssedurCategories.Rehabilitative,
            //    DrName = "Muradxan",
            //    PatientId = p1.Id,
            //    Status = Status.End,
            //    DateBegin = new DateTime(2021, 1, 19, 10, 00, 00),
            //    DateEnd = new DateTime(2021, 1, 19, 23, 00, 00),
            //    PatientName = p1.Name,
            //    PatientSurname = p1.Surname,
            //    Price = 300,
            //    Room = new Room() { Number = 2, RoomType = TypeOfRoom.Dental },
            //};

            //Prossedur hpr2 = new Prossedur()
            //{
            //    PatientDescription = "Disim agriyir",
            //    Categoria = ProssedurCategories.Rehabilitative,
            //    DrName = "Qasim",
            //    PatientId = p1.Id,
            //    Status = Status.End,
            //    DateBegin = new DateTime(2021, 1, 19, 10, 00, 00),
            //    DateEnd = new DateTime(2021, 1, 19, 23, 00, 00),
            //    PatientName = p1.Name,
            //    PatientSurname = p1.Surname,
            //    Price = 300,
            //    Room = new Room() { Number = 2, RoomType = TypeOfRoom.Dental },
            //};


            //hpr1.UsingDevice.Add(new Device() { Name = "aa", IsBusy = false, Profit = 15 });
            //hpr1.UsingDevice.Add(new Device() { Name = "bb", IsBusy = false, Profit = 15 });

            //hpr2.UsingDevice.Add(new Device() { Name = "zz", IsBusy = false, Profit = 15 });
            //hpr2.UsingDevice.Add(new Device() { Name = "qq", IsBusy = false, Profit = 15 });



            //d1.ProssedurId.Add(pr1.Id);
            //p1.ProssedursId.Add(pr1.Id);
            //p1.ProssedursId.Add(pr2.Id);
            //d1.ProssedurId.Add(pr2.Id);

            //p1.HistoryBook.ProssedursId.Add(hpr1.Id);
            //p1.HistoryBook.ProssedursId.Add(hpr2.Id);


            //var a1 = new Analysis() { Name = "cc" };
            //a1.GetAnalys.Add(new AnalysisElement() { Name = "aaa", Count = 15 });
            //a1.GetAnalys.Add(new AnalysisElement() { Name = "qqq", Count = 15 });

            //var a2 = new Analysis() { Name = "ccgyftf" };
            //a2.GetAnalys.Add(new AnalysisElement() { Name = "aaa", Count = 15 });
            //a2.GetAnalys.Add(new AnalysisElement() { Name = "sss", Count = 15 });

            //p1.HistoryBook.AnalyseResultsId.Add(a1.Id);
            //p1.HistoryBook.AnalyseResultsId.Add(a2.Id);

            //var d = Container.GetInstance<IRepository<Doctor>>();
            //var a = Container.GetInstance<IRepository<Analysis>>();
            //var p = Container.GetInstance<IRepository<Patient>>();
            //var pr = Container.GetInstance<IRepository<Prossedur>>();

            //d.Add(d1);
            //d.Add(d2);

            //p.Add(p1);
            //p.Add(p2);

            //pr.Add(pr1);
            //pr.Add(pr2);
            //pr.Add(hpr1);
            //pr.Add(hpr2);

            //a.Add(a1);
            //a.Add(a2);


        }
    }
}
