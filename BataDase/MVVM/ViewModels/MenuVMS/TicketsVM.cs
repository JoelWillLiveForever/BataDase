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
    public class TicketsVM : ObjectModel
    {
        public BindingList<TicketsM> SourceList { get; set; }
        public BindingList<SchedulesM> scheds { get; set; }
        public BindingList<UsersM> users { get; set; }
        private AppDBContext dbContext;

        private TextBlock CarriageNum, Seatnum, Username;
        private ComboBox seatnum, carriageNum, username;
        private bool isAdd;
        private int index;

        public TicketsVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.TicketsMs.Load();
            dbContext.UsersMs.Load();
            dbContext.SchedulesMs.Load();

            users = dbContext.UsersMs.Local.ToBindingList();
            scheds = dbContext.SchedulesMs.Local.ToBindingList();

            // Margin
            Thickness temp = new Thickness(5);

            CarriageNum = new TextBlock();
            CarriageNum.Text = App.Current.Resources["Text_CarriageNum"] + ":";
            Grid.SetRow(CarriageNum, 0);
            Grid.SetColumn(CarriageNum, 0);

            Seatnum = new TextBlock();
            Seatnum.Text = App.Current.Resources["Text_SeatNum"] + ":";
            Grid.SetRow(Seatnum, 1);
            Grid.SetColumn(Seatnum, 0);

            Username = new TextBlock();
            Username.Text = App.Current.Resources["Text_Login"] + ":";
            Grid.SetRow(Username, 2);
            Grid.SetColumn(Username, 0);

            carriageNum = new ComboBox();
            carriageNum.Margin = temp;
            Grid.SetRow(carriageNum, 0);
            Grid.SetColumn(carriageNum, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < 4; i++)
            {
                carriageNum.Items.Insert(i, i.ToString());
            }

            seatnum = new ComboBox();
            seatnum.Margin = temp;
            Grid.SetRow(seatnum, 1);
            Grid.SetColumn(seatnum, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < 49; i++)
            {
                seatnum.Items.Insert(i, i.ToString());
            }


            username = new ComboBox();
            username.Margin = temp;
            Grid.SetRow(username, 2);
            Grid.SetColumn(username, 1);

            for (int i = 0; i < users.Count; i++)
            {
                username.Items.Insert(i, users[i]._login);
            }

            SourceList = dbContext.TicketsMs.Local.ToBindingList();
        }

        public void Save()
        {
            if (dbContext != null)
                dbContext.SaveChanges();
        }

        public void Close()
        {
            if (dbContext != null) dbContext = null;
        }

        public void Connect()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.UsersMs.Load();

            // Инициализация списка элементов
            SourceList = dbContext.TicketsMs.Local.ToBindingList();
            TableV.Current_DataGrid.ItemsSource = SourceList;
        }

        public void Request()
        {
            throw new System.NotImplementedException();
        }

        public void AddEdit(bool isAdd)
        {
            this.isAdd = isAdd;
            // Показываем диалоговое окно
            DialogV dialogV = new DialogV();
            Grid dialogGrid = dialogV.Dialog_Grid;

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

                //индекс текущей выбранной строки в DataGrid
                //кастыль, но куда без кастылей?
                index = TableV.Current_DataGrid.SelectedIndex;

                //Если всё ок, вставляем данные для данного юзера в форму, который затем будем менять
                TicketsM temp = SourceList[index];

                seatnum.SelectedItem = temp._seatnum;
                carriageNum.SelectedItem = temp._carriage_number;
                username.SelectedItem = temp.UsersM._login;

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(Seatnum);
            dialogGrid.Children.Add(CarriageNum);
            dialogGrid.Children.Add(Username);

            dialogGrid.Children.Add(seatnum);
            dialogGrid.Children.Add(carriageNum);
            dialogGrid.Children.Add(username);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(Seatnum);
            dialogGrid.Children.Remove(CarriageNum);
            dialogGrid.Children.Remove(Username);

            dialogGrid.Children.Remove(seatnum);
            dialogGrid.Children.Remove(carriageNum);
            dialogGrid.Children.Remove(username); ;

            seatnum.SelectedIndex = 0;
            carriageNum.SelectedIndex = 0;
            username.SelectedIndex = 0;
        }

        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (seatnum.Text == null)
            {
                MessageBox.Show("Укажите место!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (carriageNum.Text == null)
            {
                MessageBox.Show("Укажите номер вагона!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (username.Text == null)
            {
                MessageBox.Show("Укажите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TicketsM temp = new TicketsM();
            temp._seatnum = Convert.ToInt32(seatnum.SelectedItem);
            temp._carriage_number = Convert.ToInt32(carriageNum.SelectedItem);
            temp.UsersM._login = username.Text;

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.TicketsMs.Local.Add(temp);

                // Очищаем поля
                seatnum.SelectedIndex = 0;
                carriageNum.SelectedIndex = 0;
                username.SelectedIndex = 0;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._ticket_id;
                var ticket = dbContext.TicketsMs.Local
                    .Single(o => o._ticket_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                ticket._seatnum = temp._seatnum;
                ticket._carriage_number = temp._carriage_number;
                ticket.UsersM._login = temp.UsersM._login;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(ticket).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.TicketsMs.Local.ToBindingList();
        }

        public void Delete()
        {
            if (TableV.Current_DataGrid.SelectedItems.Count < 1)
            {
                MessageBox.Show("Выберите элементы для удаления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Пока есть элементы, которые нужно удалить, то крутится цикл
            while (TableV.Current_DataGrid.SelectedItems.Count > 0)
            {
                index = TableV.Current_DataGrid.SelectedIndex;

                TicketsM deleteEntity = SourceList[index];

                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
