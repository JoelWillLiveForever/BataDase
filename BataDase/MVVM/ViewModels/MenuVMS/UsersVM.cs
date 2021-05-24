using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.Views;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class UsersVM : ObjectModel
    {
        // Контекст и список с данными
        public BindingList<UsersM> SourceList { get; set; }
        private AppDBContext dbContext;

        // Пачка контролов, характерных для данной таблицы
        private TextBlock Surname, Name, Lastname, Sex, Birthday, Login, Password, Access, Bill;
        private TextBox surname, name, lastname, birthday, login, password, bill;
        private ComboBox sex, access;

        // Конструктор
        public UsersVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.UsersMs.Load();

            // Margin
            Thickness temp = new Thickness(5);

            // Назначение свойств пачке контролов
            // Лэйбл "Фамилия", назначение текста, строки и колонки в Grid
            Surname = new TextBlock();
            Surname.Text = App.Current.Resources["Text_Surname"] + ":";
            Grid.SetRow(Surname, 0);
            Grid.SetColumn(Surname, 0);

            // Лэйбл "Имя", назначение текста, строки и колонки в Grid
            Name = new TextBlock();
			Name.Text = App.Current.Resources["Text_Name"] + ":";
			Grid.SetRow(Name, 1);
            Grid.SetColumn(Name, 0);

            // Лэйбл "Отчество", назначение текста, строки и колонки в Grid
            Lastname = new TextBlock();
            Lastname.Text = App.Current.Resources["Text_Lastname"] + ":";
            Grid.SetRow(Lastname, 2);
            Grid.SetColumn(Lastname, 0);

            // Лэйбл "Пол", назначение текста, строки и колонки в Grid
            Sex = new TextBlock();
            Sex.Text = App.Current.Resources["Text_Sex"] + ":";
            Grid.SetRow(Sex, 3);
            Grid.SetColumn(Sex, 0);

            // Лэйбл "Дата рождения", назначение текста, строки и колонки в Grid
            Birthday = new TextBlock();
            Birthday.Text = App.Current.Resources["Text_Birthday"] + ":";
            Grid.SetRow(Birthday, 4);
            Grid.SetColumn(Birthday, 0);

            // Лэйбл "Логин", назначение текста, строки и колонки в Grid
            Login = new TextBlock();
            Login.Text = App.Current.Resources["Text_Login"] + ":";
            Grid.SetRow(Login, 5);
            Grid.SetColumn(Login, 0);

            // Лэйбл "Пароль", назначение текста, строки и колонки в Grid
            Password = new TextBlock();
            Password.Text = App.Current.Resources["Text_Password"] + ":";
            Grid.SetRow(Password, 6);
            Grid.SetColumn(Password, 0);

            // Лэйбл "Уровень доступа", назначение текста, строки и колонки в Grid
            Access = new TextBlock();
            Access.Text = App.Current.Resources["Text_Access"] + ":";
            Grid.SetRow(Access, 7);
            Grid.SetColumn(Access, 0);

            // Лэйбл "Денежный счёт", назначение текста, строки и колонки в Grid
            Bill = new TextBlock();
            Bill.Text = App.Current.Resources["Text_Bill"] + ":";
            Grid.SetRow(Bill, 8);
            Grid.SetColumn(Bill, 0);

            // Текстбокс "Фамилия", назначение отсутпа (Margin), строки и колонки в Grid
            surname = new TextBox();
            surname.Margin = temp;
            Grid.SetRow(surname, 0);
            Grid.SetColumn(surname, 1);

            // Текстбокс "Имя", назначение отсутпа (Margin), строки и колонки в Grid
            name = new TextBox();
            name.Margin = temp;
            Grid.SetRow(name, 1);
            Grid.SetColumn(name, 1);

            // Текстбокс "Отчество", назначение отсутпа (Margin), строки и колонки в Grid
            lastname = new TextBox();
            lastname.Margin = temp;
            Grid.SetRow(lastname, 2);
            Grid.SetColumn(lastname, 1);

            // Комбобокс "Пол", назначение отсутпа (Margin), строки и колонки в Grid
            // А также, назначение элементов комбобокс: "Мужской" и "Женский"
            sex = new ComboBox();
            sex.Margin = temp;
            Grid.SetRow(sex, 3);
            Grid.SetColumn(sex, 1);

            // Вытягивание полов из ресурсов
            string male = (string)App.Current.Resources["Text_Male"];
            string female = (string)App.Current.Resources["Text_Female"];

            // Назначение элементов комбобокс
            sex.Items.Insert(0, male);
            sex.Items.Insert(1, female);
            sex.SelectedIndex = 0;

            // Текстбокс "Дата рождения", назначение отсутпа (Margin), строки и колонки в Grid
            birthday = new TextBox();
            birthday.Margin = temp;
            Grid.SetRow(birthday, 4);
            Grid.SetColumn(birthday, 1);

            // Текстбокс "Логин", назначение отсутпа (Margin), строки и колонки в Grid
            login = new TextBox();
            login.Margin = temp;
            Grid.SetRow(login, 5);
            Grid.SetColumn(login, 1);

            // Текстбокс "Пароль", назначение отсутпа (Margin), строки и колонки в Grid
            password = new TextBox();
            password.Margin = temp;
            Grid.SetRow(password, 6);
            Grid.SetColumn(password, 1);

            // Комбобокс "Уровень доступа", назначение отсутпа (Margin), строки и колонки в Grid
            // А также, назначение элементов комбобокс: "Админ" и "Юзер"
            access = new ComboBox();
            access.Margin = temp;
            Grid.SetRow(access, 7);
            Grid.SetColumn(access, 1);

            // Вытягивание access'ов из ресурсов
            string admin = (string)App.Current.Resources["Text_Admin"];
            string user = (string)App.Current.Resources["Text_User"];

            // Назначение элементов комбобокс
            access.Items.Insert(0, admin);
            access.Items.Insert(1, user);
            access.SelectedIndex = 0;

            // Текстбокс "Денежный счёт", назначение отсутпа (Margin), строки и колонки в Grid
            bill = new TextBox();
            bill.Margin = temp;
            Grid.SetRow(bill, 8);
            Grid.SetColumn(bill, 1);

            // Инициализация списка элементов БД
            SourceList = dbContext.UsersMs.Local.ToBindingList();
        }

        // Deprecated
        public void Close()
        {
            // Обнуление контекста БД и списка элементов
            //if (dbContext != null) dbContext = null;
        }

        // Метод, открывающий подключение в БД, когда данная таблица видна пользователю
        public void Connect()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.UsersMs.Load();

            // Инициализация списка элементов
            SourceList = dbContext.UsersMs.Local.ToBindingList();
        }

        // Метод, срабатывающий при нажатии на кнопку "Запрос"
        public void Request()
        {
            throw new System.NotImplementedException();
        }

        // Метод, срабатывающий при нажатии на кнопку "Добавить"
        public void Add()
        {
            // Показываем диалоговое окно
            DialogV dialogV = new DialogV();
            Grid dialogGrid = dialogV.Dialog_Grid;

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(Surname);
            dialogGrid.Children.Add(Name);
            dialogGrid.Children.Add(Lastname);
            dialogGrid.Children.Add(Sex);
            dialogGrid.Children.Add(Birthday);
            dialogGrid.Children.Add(Login);
            dialogGrid.Children.Add(Password);
            dialogGrid.Children.Add(Access);
            dialogGrid.Children.Add(Bill);

            dialogGrid.Children.Add(surname);
            dialogGrid.Children.Add(name);
            dialogGrid.Children.Add(lastname);
            dialogGrid.Children.Add(sex);
            dialogGrid.Children.Add(birthday);
            dialogGrid.Children.Add(login);
            dialogGrid.Children.Add(password);
            dialogGrid.Children.Add(access);
            dialogGrid.Children.Add(bill);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия
            Button button = dialogV.Button_Execute;
            button.Content = App.Current.Resources["Text_Add"];
            button.Click += new RoutedEventHandler(ExecuteAdd);

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(surname);
            dialogGrid.Children.Remove(name);
            dialogGrid.Children.Remove(lastname);
            dialogGrid.Children.Remove(sex);
            dialogGrid.Children.Remove(birthday);
            dialogGrid.Children.Remove(login);
            dialogGrid.Children.Remove(password);
            dialogGrid.Children.Remove(access);
            dialogGrid.Children.Remove(bill);

            dialogGrid.Children.Remove(Surname);
            dialogGrid.Children.Remove(Name);
            dialogGrid.Children.Remove(Lastname);
            dialogGrid.Children.Remove(Sex);
            dialogGrid.Children.Remove(Birthday);
            dialogGrid.Children.Remove(Login);
            dialogGrid.Children.Remove(Password);
            dialogGrid.Children.Remove(Access);
            dialogGrid.Children.Remove(Bill);
        }

        // Метод, срабатывающий при нажатии на кнопку в окне добавления юзера
        public void ExecuteAdd(object sender, RoutedEventArgs e)
        {
            // Проверки TextBox на null и пустую строку
            if (surname.Text == null || surname.Text == "")
            {
                MessageBox.Show("Укажите фамилию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (name.Text == null || name.Text == "")
            {
                MessageBox.Show("Укажите имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка DateTime на соответствие паттерну даты dd.MM.yyyy
            DateTime dateTime;
            if (!DateTime.TryParseExact(birthday.Text, "dd.MM.yyyy", App.Language, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                MessageBox.Show("Некорректная дата рождения!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверки TextBox на null и пустую строку
            if (login.Text == null || login.Text == "")
            {
                MessageBox.Show("Укажите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password.Text == null || password.Text == "")
            {
                MessageBox.Show("Укажите пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверки TextBox для денег на null и пустую строку
            // Если строка пустая, то вставляется "0"
            float result;
            if (bill.Text == null || bill.Text == "")
            {
                bill.Text = "0";
            } else if (!float.TryParse(bill.Text, out result))
            {
                MessageBox.Show("Некорректная сумма!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создание нового Юзера
            UsersM temp = new UsersM();
            temp._surname = surname.Text;
            temp._name = name.Text;
            temp._lastname = lastname.Text;
            temp._sex = sex.SelectedIndex;

            dateTime = DateTime.ParseExact(birthday.Text, "dd.MM.yyyy", null);
            temp._birthday = dateTime.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;

            temp._login = login.Text;
            temp._pass = password.Text;
            temp._bill = float.Parse(bill.Text);

            // Сохранение нового юзера в БД
            dbContext.UsersMs.Add(temp);
            dbContext.SaveChanges();

            // Обновление списка
            //dbContext.UsersMs.Load();
            SourceList = dbContext.UsersMs.Local.ToBindingList();

            // В конце нужно насильно пнуть ебанный датагрид
            // Он рили уже заебал как и ваш шарп в целом
            // Ты ебать обновляешь ему ItemsSource
            // А он как долбоеб такой смотрит на тя, ваще ПОХУЙ...
            // Настолько похуй, что даже господь со всей своей силой
            // не смог бы поднять этот ебанный член похуиста датагрида :/
            //MenuV.MenuV_DataGrid.Items.Refresh();
        }

        // Метод, срабатывающий при нажатии на кнопку "Изменить"
        public void Edit()
        {
            throw new System.NotImplementedException();
        }

        // Метод, срабатывающий при нажатии на кнопку "Удалить"
        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
