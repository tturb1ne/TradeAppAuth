using System;
using TradeApp.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels;
using System.Windows;
using TradeApp.Models;

namespace TradeApp.ViewModel
{
    internal class AuthVerificationVM : NotifierVM
    {
        private string lgn;
        private string pwd;

        public string Login 
        {
            get => lgn;
            set { lgn = value;

                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => pwd;
            set
            {
                pwd = value;

                OnPropertyChanged(nameof(Password));
            }
        }

        public void Verify()            //Переделал цикл регистрации, теперь должен работать над окном с администраторами
        {
            if (lgn == null || pwd == null)
            {
                MessageBox.Show("Please fill out the blank fields." + MessageBoxButton.OK + MessageBoxImage.Stop);
                return;
            }

            using(var db = new TradeEntities())        
            {
                var check = db.User.FirstOrDefault(u => u.UserLogin == lgn && u.UserPassword == pwd);   //Необходимо добавить проверку на логин/пароль админа
                if (check != null)
                {
                    MessageBox.Show("Authorization complete." + MessageBoxButton.OK);

                    var NewAdWindow = new AdminDataWindow();

                    NewAdWindow.Show();

                    foreach (Window wnd in Application.Current.Windows)
                    {
                        if (wnd is AuthWindow)
                        {
                            wnd.Close();
                        }
                    }
                }
                else MessageBox.Show("Incorrect Login Or Password" + MessageBoxButton.OK + MessageBoxImage.Warning);
            }
        }

        public void GuestAuth()
        {

        }
    }
}
