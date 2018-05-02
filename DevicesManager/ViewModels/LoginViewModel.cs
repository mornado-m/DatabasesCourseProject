using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class LoginViewModel : Screen
    {
        public LoginViewModel()
        {
            DisplayName = "Вхід";
        }
        
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value; 
                NotifyOfPropertyChange(() => Login);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public void TryLogin()
        {
            var UserId = LoginModel.VerifyLogin(Login, Password);
            if (UserId.Equals(-1))
            {
                MessageBox.Show("Неправильний логін або пароль.", "Помилка!");
                return;
            }
            DisplayName = "Вихід";
            UserLogedIn?.Invoke(this, new LoginEventArgs {UserId = UserId, UserLogin = Login});
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (DisplayName.Equals("Вихід"))
            {
                DisplayName = "Вхід";
                UserLogedOut?.Invoke(this, EventArgs.Empty);
            }
        }

        public delegate void LoginHandler(object sender, LoginEventArgs e);
        public event LoginHandler UserLogedIn;
        public event EventHandler UserLogedOut;

        public class LoginEventArgs : EventArgs
        {
            public int UserId { get; set; }
            public string UserLogin { get; set; }
        }
    }
}
