using DoctorApp.Models;
using PacientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PacientApp.Views
{
    /// <summary>
    /// Interaction logic for DoctorVoteUC.xaml
    /// </summary>
    public partial class DoctorVoteUC : Window
    {
        public DoctorVoteUC(Patient currentUser, Prossedur prossedur)
        {
            InitializeComponent();
            this.DataContext = new  DoctorVoteWindowVM(currentUser,prossedur);
        }
    }
}
