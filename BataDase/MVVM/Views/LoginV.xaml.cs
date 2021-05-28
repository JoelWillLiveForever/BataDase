using BataDase.Core;
using BataDase.MVVM.ViewModels;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace BataDase.MVVM.Views
{
    public partial class LoginV : UserControl
    {
        public LoginV()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Button_SignIn)
            {
                // Авторизация
                // Проверка логинов
                if (TextBox_Login.Text == null || TextBox_Login.Text == "")
                {
                    MessageBox.Show("Введите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (TextBox_Pass.Password == null || TextBox_Pass.Password == "")
                {
                    MessageBox.Show("Введите пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Универсальный логин и пароль админа
                if (TextBox_Login.Text == "admin" && TextBox_Pass.Password == "admin")
                {
                    Properties.Settings.Default.IsAdmin = true;
                    Properties.Settings.Default.Visibility = "Visible";

                    MainVM.MenuVM_RelayCommand.Execute(null);
                } else
                {
                    AppDBContext dBContext = AppDBContext.GetInstance();
                    dBContext.UsersMs.Load();

                    string login = TextBox_Login.Text;
                    string pass = TextBox_Pass.Password;

                    var user = (from u in dBContext.UsersMs
                                where u._login == login && u._pass == pass
                                select u).FirstOrDefault();

                    if (user == null)
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int accessLevel = user._access;

                    if (accessLevel == 0) BataDase.Properties.Settings.Default.IsAdmin = true;
                    else if (accessLevel == 1) BataDase.Properties.Settings.Default.IsAdmin = false;

                    // Сохранение id пользователя, который в данный момент находится в системе
                    BataDase.Properties.Settings.Default.CurrentUserId = user._user_id;

                    MainVM.MenuVM_RelayCommand.Execute(null);
                }

            } else if (sender == Button_SignUp)
            {
                // Открыть страницу регистрации
                MainVM.RegistrationVM_RelayCommand.Execute(null);
            }
        }
    }
}
