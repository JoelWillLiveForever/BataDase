using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.Views;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class UsersVM : ObjectModel
    {
        private bool isAdd;
        private int index;

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
            Surname.SetResourceReference(TextBlock.TextProperty, "Text_Surname");
            Grid.SetRow(Surname, 0);
            Grid.SetColumn(Surname, 0);

            // Лэйбл "Имя", назначение текста, строки и колонки в Grid
            Name = new TextBlock();
            Name.SetResourceReference(TextBlock.TextProperty, "Text_Name");
			Grid.SetRow(Name, 1);
            Grid.SetColumn(Name, 0);

            // Лэйбл "Отчество", назначение текста, строки и колонки в Grid
            Lastname = new TextBlock();
            Lastname.SetResourceReference(TextBlock.TextProperty, "Text_Lastname");
            Grid.SetRow(Lastname, 2);
            Grid.SetColumn(Lastname, 0);

            // Лэйбл "Пол", назначение текста, строки и колонки в Grid
            Sex = new TextBlock();
            Sex.SetResourceReference(TextBlock.TextProperty, "Text_Sex");
            Grid.SetRow(Sex, 3);
            Grid.SetColumn(Sex, 0);

            // Лэйбл "Дата рождения", назначение текста, строки и колонки в Grid
            Birthday = new TextBlock();
            Birthday.SetResourceReference(TextBlock.TextProperty, "Text_Birthday");
            Grid.SetRow(Birthday, 4);
            Grid.SetColumn(Birthday, 0);

            // Лэйбл "Логин", назначение текста, строки и колонки в Grid
            Login = new TextBlock();
            Login.SetResourceReference(TextBlock.TextProperty, "Text_Login");
            Grid.SetRow(Login, 5);
            Grid.SetColumn(Login, 0);

            // Лэйбл "Пароль", назначение текста, строки и колонки в Grid
            Password = new TextBlock();
            Password.SetResourceReference(TextBlock.TextProperty, "Text_Password");
            Grid.SetRow(Password, 6);
            Grid.SetColumn(Password, 0);

            // Лэйбл "Уровень доступа", назначение текста, строки и колонки в Grid
            Access = new TextBlock();
            Access.SetResourceReference(TextBlock.TextProperty, "Text_Access");
            Grid.SetRow(Access, 7);
            Grid.SetColumn(Access, 0);

            // Лэйбл "Денежный счёт", назначение текста, строки и колонки в Grid
            Bill = new TextBlock();
            Bill.SetResourceReference(TextBlock.TextProperty, "Text_Bill");
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
            ComboBoxItem male = new ComboBoxItem();
            male.SetResourceReference(ComboBoxItem.ContentProperty, "Text_Male");

            ComboBoxItem female = new ComboBoxItem();
            female.SetResourceReference(ComboBoxItem.ContentProperty, "Text_Female");

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
            //string admin = (string)App.Current.Resources["Text_Admin"];
            //string user = (string)App.Current.Resources["Text_User"];

            ComboBoxItem admin = new ComboBoxItem();
            admin.SetResourceReference(ComboBoxItem.ContentProperty, "Text_Admin");

            ComboBoxItem user = new ComboBoxItem();
            user.SetResourceReference(ComboBoxItem.ContentProperty, "Text_User");

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

        // Метод, открывающий подключение в БД, когда данная таблица видна пользователю
        public void ConnectAndUpdate()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.UsersMs.Load();

            // Инициализация списка элементов
            SourceList = dbContext.UsersMs.Local.ToBindingList();
            TableV.Current_DataGrid.ItemsSource = SourceList;
        }

        // Метод, срабатывающий при нажатии на кнопку "Запрос"
        public void Request()
        {
            throw new System.NotImplementedException();
        }

        // Метод, срабатывающий при нажатии на кнопку "Добавить" и "Изменить"
        public void AddEdit(bool isAdd)
        {
            // Сохранить состояние, добавление это или изменение
            this.isAdd = isAdd;

            // Создаём диалоговое окно
            DialogV dialogV = new DialogV();
            Grid dialogGrid = dialogV.Dialog_Grid;

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия
            Button button = dialogV.Button_Execute;

            // Если это добавление
            if (isAdd)
            {
                button.Content = App.Current.Resources["Text_Add"];
                index = -1;
            }
            // Если это изменение
            else
            {
                // если ничего не выбрано в датагриде то ошибка
                // если выбрано больше 1 элемента то тоже ошибка
                if (TableV.Current_DataGrid.SelectedItems.Count < 1)
                {
                    MessageBox.Show("Выберите элемент для изменения!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (TableV.Current_DataGrid.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Можно выбрать для изменения не более ОДНОГО элемента за раз!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // индекс текущей выбранной строки в DataGrid
                // кастыль, но куда без кастылей?
                index = TableV.Current_DataGrid.SelectedIndex;

                // Если всё ок, вставляем данные для данного юзера в форму, который затем будем менять
                UsersM temp = SourceList[index];

                surname.Text = temp._surname;
                name.Text = temp._name;
                lastname.Text = temp._lastname;
                sex.SelectedIndex = temp._sex;

                DateTime startdate = DateTime.MinValue.AddMilliseconds(temp._birthday);
                birthday.Text = startdate.ToString("dd.MM.yyyy");

                login.Text = temp._login;
                password.Text = temp._pass;
                access.SelectedIndex = temp._access;
                bill.Text = temp._bill.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            // назначаем метод для события кнопки
            // метод сразу обрабатывает и добавление и изменение, типа оптимизация
            button.Click += new RoutedEventHandler(ExecuteAddEdit);

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

            // Показываем диалоговое окно
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

            // Очищаем поля
            surname.Text = null;
            name.Text = null;
            lastname.Text = null;
            sex.SelectedIndex = 0;
            birthday.Text = null;
            login.Text = null;
            password.Text = null;
            access.SelectedIndex = 0;
            bill.Text = null;
        }

        // Метод, срабатывающий при нажатии на кнопку в окне добавления или изменения юзера
        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Дата указана некорректно!\nУкажите дату в виде: \"dd.MM.yyyy\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверки TextBox на null и пустую строку
            if (login.Text == null || login.Text == "")
            {
                MessageBox.Show("Укажите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else
            {
                // Если юзер с данным логином уже есть в таблице, то вывести ошибку
                foreach (var item in SourceList)
                    if (item._login == login.Text)
                    {
                        // Если сейчас добавление, то просто вывести ошибку
                        if (isAdd)
                        {
                            MessageBox.Show("Пользователь с данным логином уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        } 
                        // Если сейчас изменение, то в случае совпадения индексов ошибку не выводить
                        else
                        {
                            // Вывести ошибку, если элементы таблицы имеют разные индексы,
                            // т.е. такой логин уже есть у кого то другого в таблице
                            if (item._user_id != SourceList[index]._user_id)
                            {
                                MessageBox.Show("Пользователь с данным логином уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }
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
            temp._birthday = dateTime.Subtract(DateTime.MinValue).TotalMilliseconds;

            temp._login = login.Text;
            temp._pass = password.Text;
            temp._access = access.SelectedIndex;
            temp._bill = float.Parse(bill.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.UsersMs.Local.Add(temp);

                // Очищаем поля
                surname.Text = null;
                name.Text = null;
                lastname.Text = null;
                sex.SelectedIndex = 0;
                birthday.Text = null;
                login.Text = null;
                password.Text = null;
                access.SelectedIndex = 0;
                bill.Text = null;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._user_id;
                var user = dbContext.UsersMs.Local
                    .Single(o => o._user_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                user._surname = temp._surname;
                user._name = temp._name;
                user._lastname = temp._lastname;
                user._sex = temp._sex;
                temp._birthday = temp._birthday;
                user._login = temp._login;
                user._pass = temp._pass;
                user._access = temp._access;
                user._bill = temp._bill;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(user).State = EntityState.Modified;
            }

            // Сохранение контекста БД
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.UsersMs.Local.ToBindingList();

            // Пинаем DataGrid, ибо он тупой и по другому не понимает
            TableV.Current_DataGrid.Items.Refresh();
        }

        // Метод, срабатывающий при нажатии на кнопку "Удалить"
        public void Delete()
        {
            // если ничего не выбрано в датагриде то ошибка
            if (TableV.Current_DataGrid.SelectedItems.Count < 1)
            {
                MessageBox.Show("Выберите элементы для удаления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Пока есть элементы, которые нужно удалить, то крутится цикл
            while (TableV.Current_DataGrid.SelectedItems.Count > 0)
            {
                // индекс текущей выбранной строки в DataGrid
                // кастыль, но куда без кастылей?
                index = TableV.Current_DataGrid.SelectedIndex;

                // Прежде чем удалить элемент из данной таблицы,
                // нужно удалить его из зависимых таблиц (см. Физическую модель)
                // иначе будет ошибка и приложение крашнится

                // Получение элемента, который будем удалять
                UsersM deleteEntity = SourceList[index];
                
                // Получение всех записей из зависимой таблицы с билетами,
                // которые содержат нашего юзера
                var tickets = dbContext.TicketsMs
                    .Where(o => o._user_id == deleteEntity._user_id);

                // Если в зависимых таблицах есть записи с нашим юзером,
                // то произвести чистим зависимую таблицу
                if (tickets != null)
                    dbContext.TicketsMs.RemoveRange(tickets);

                // После того, как подчистили зависимые таблицы,
                // выбираем сам объект для удаления из КОНТЕКСТА БД
                var contextDeleteEntity = dbContext.UsersMs.Local
                    .Single(o => o._user_id == deleteEntity._user_id);

                // Удаляем контекстного юзера из контекста
                dbContext.UsersMs.Local.Remove(contextDeleteEntity);

                // Удаляем НЕконтекстного юзера из SourceList
                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
