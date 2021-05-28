using BataDase.Core;
using BataDase.MVVM.ViewModels;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using BataDase.MVVM.Models.MenuVMS;

namespace BataDase.MVVM.Views
{
    public partial class RegistrationV : UserControl
    {
        public RegistrationV()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Button_Back)
            {
                MainVM.LoginVM_RelayCommand.Execute(null);
            } else if (sender == Button_SignUp)
            {
                // Регистрация
                // Проверки TextBox на null и пустую строку
                if (TextBox_Surname.Text == null || TextBox_Surname.Text == "")
                {
                    MessageBox.Show("Укажите фамилию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (TextBox_Name.Text == null || TextBox_Name.Text == "")
                {
                    MessageBox.Show("Укажите имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка DateTime на соответствие паттерну даты dd.MM.yyyy
                DateTime dateTime;
                if (!DateTime.TryParseExact(TextBox_Birthday.Text, "dd.MM.yyyy", App.Language, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    MessageBox.Show("Дата указана некорректно!\nУкажите дату в виде: \"dd.MM.yyyy\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppDBContext dBContext = AppDBContext.GetInstance();
                dBContext.UsersMs.Load();

                // Проверки TextBox на null и пустую строку
                if (TextBox_Login.Text == null || TextBox_Login.Text == "")
                {
                    MessageBox.Show("Укажите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    string login = TextBox_Login.Text;

                    var user = (from u in dBContext.UsersMs
                                where u._login == login
                                select u).FirstOrDefault();

                    // Если юзер с данным логином уже есть в таблице, то вывести ошибку
                    if (user != null)
                    {
                        MessageBox.Show("Пользователь с данным логином уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (TextBox_Pass.Text == null || TextBox_Pass.Text == "")
                {
                    MessageBox.Show("Укажите пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (TextBox_RepeatPass.Text != TextBox_Pass.Text)
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание нового Юзера
                UsersM temp = new UsersM();
                temp._surname = TextBox_Surname.Text;
                temp._name = TextBox_Name.Text;
                temp._lastname = TextBox_Lastname.Text;
                temp._sex = ComboBox_Sex.SelectedIndex;

                dateTime = DateTime.ParseExact(TextBox_Birthday.Text, "dd.MM.yyyy", null);
                temp._birthday = dateTime.Subtract(DateTime.MinValue).TotalMilliseconds;

                temp._login = TextBox_Login.Text;
                temp._pass = TextBox_Pass.Text;
                temp._access = ComboBox_AccessLevel.SelectedIndex;
                temp._bill = 0;

                dBContext.UsersMs.Local.Add(temp);
                dBContext.SaveChanges();

                MessageBox.Show("Вы были успешно зарегистрированы!", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);

                // Переход обратно на логин
                MainVM.LoginVM_RelayCommand.Execute(null);
            }
        }
    }
}
