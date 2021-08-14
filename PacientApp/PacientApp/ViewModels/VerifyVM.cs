using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
    public class VerifyVM
    {
        readonly string verificationCode;
        public string Text { get; set; }

        public ICommand VerifyCommand { get; set; }

        public bool CloseSignal { get; set; } = default;

        public bool? DialogResult { get; set; } = false;

        public VerifyVM(string mail)
        {
            verificationCode = VerificationCodeGenerator();

            string to = mail;

            string subject = "HealthCare Verification Code";

            string content = $"Verification Code: {verificationCode}";

            SendMail(to, subject, content);

            VerifyCommand = new RelayCommand<object>(VerifyCommandExecute);
        }

        private void VerifyCommandExecute(object obj)
        {
            if (verificationCode == Text)
            {
                var window = obj as Window;

                window.DialogResult = true;

                window.Close();
            }
        }

        private void SendMail(string to, string subject, string content)
        {
            try
            {
                using MailMessage mail = new MailMessage();
                using SmtpClient SmtpServer = new SmtpClient("smtp.outlook.com");

                string username = ConfigurationManager.AppSettings.Get("MailUsername").ToString();
                string password = ConfigurationManager.AppSettings.Get("MailPassword").ToString();

                mail.From = new MailAddress(username);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = content;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                MessageBox.Show("Connection problem");
            }
        }

        private string VerificationCodeGenerator()
        {
            return new Random().Next(1000, 9999).ToString();
        }

    }
}
