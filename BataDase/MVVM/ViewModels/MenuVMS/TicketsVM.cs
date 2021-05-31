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

        private TextBlock CarriageNum, Seatnum, Username, ScheduleNum;
        private TextBox seatnum;
        private ComboBox carriageNum, username, scheduleNum;
        private bool isAdd;
        private int index;
        
        // Verified
        public TicketsVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();

            dbContext.UsersMs.Load();
            users = dbContext.UsersMs.Local.ToBindingList();

            dbContext.SchedulesMs.Load();
            scheds = dbContext.SchedulesMs.Local.ToBindingList();

            dbContext.TicketsMs.Load();

            // Margin
            Thickness temp = new Thickness(5);

            ScheduleNum = new TextBlock();
            ScheduleNum.SetResourceReference(TextBlock.TextProperty, "Text_ScheduleNum");
            Grid.SetRow(ScheduleNum, 0);
            Grid.SetColumn(ScheduleNum, 0);

            CarriageNum = new TextBlock();
            CarriageNum.SetResourceReference(TextBlock.TextProperty, "Text_CarriageNum");
            Grid.SetRow(CarriageNum, 1);
            Grid.SetColumn(CarriageNum, 0);

            Seatnum = new TextBlock();
            Seatnum.SetResourceReference(TextBlock.TextProperty, "Text_SeatNum");
            Grid.SetRow(Seatnum, 2);
            Grid.SetColumn(Seatnum, 0);

            Username = new TextBlock();
            Username.SetResourceReference(TextBlock.TextProperty, "Text_Login");
            Grid.SetRow(Username, 3);
            Grid.SetColumn(Username, 0);

            scheduleNum = new ComboBox();
            scheduleNum.Margin = temp;
            Grid.SetRow(scheduleNum, 0);
            Grid.SetColumn(scheduleNum, 1);

            for (int i = 0; i < scheds.Count; i++)
            {
                scheduleNum.Items.Insert(i, scheds[i]._schedule_id);
            }

            carriageNum = new ComboBox();
            carriageNum.Margin = temp;
            Grid.SetRow(carriageNum, 1);
            Grid.SetColumn(carriageNum, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < 5; i++)
            {
                carriageNum.Items.Insert(i, (i+1).ToString());
            }

            seatnum = new TextBox();
            seatnum.Margin = temp;
            Grid.SetRow(seatnum, 2);
            Grid.SetColumn(seatnum, 1);

            username = new ComboBox();
            username.Margin = temp;
            Grid.SetRow(username, 3);
            Grid.SetColumn(username, 1);

            for (int i = 0; i < users.Count; i++)
            {
                username.Items.Insert(i, users[i]._login);
            }

            SourceList = dbContext.TicketsMs.Local.ToBindingList();
        }

        // Verified
        public void ConnectAndUpdate()
        {
            if (Properties.Settings.Default.IsAdmin)
            {
                TableV.Current_Button_Delete.Visibility = Visibility.Visible;
            }
            else
            {
                TableV.Current_Button_Delete.Visibility = Visibility.Collapsed;
            }

            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();

            dbContext.UsersMs.Load();
            users = dbContext.UsersMs.Local.ToBindingList();

            dbContext.SchedulesMs.Load();
            scheds = dbContext.SchedulesMs.Local.ToBindingList();

            dbContext.TicketsMs.Load();

            // Инициализация списка элементов
            SourceList = dbContext.TicketsMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();

            username.Items.Clear();
            for (int i = 0; i < users.Count; i++)
            {
                username.Items.Insert(i, users[i]._login);
            }

            scheduleNum.Items.Clear();
            for (int i = 0; i < scheds.Count; i++)
            {
                scheduleNum.Items.Insert(i, scheds[i]._schedule_id);
            }
        }

        public void Request()
        {
            MessageBox.Show("Для данной таблицы нет запроса!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Verified
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
                scheduleNum.SelectedIndex = 0;
                carriageNum.SelectedIndex = 0;
                username.SelectedIndex = 0;

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

                scheduleNum.SelectedItem = temp._schedule_id;
                seatnum.Text = temp._seatnum.ToString();
                carriageNum.SelectedItem = temp._carriage_number;
                username.SelectedItem = temp.UsersM._login;

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(ScheduleNum);
            dialogGrid.Children.Add(Seatnum);
            dialogGrid.Children.Add(CarriageNum);
            dialogGrid.Children.Add(Username);

            dialogGrid.Children.Add(scheduleNum);
            dialogGrid.Children.Add(seatnum);
            dialogGrid.Children.Add(carriageNum);
            dialogGrid.Children.Add(username);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(ScheduleNum);
            dialogGrid.Children.Remove(Seatnum);
            dialogGrid.Children.Remove(CarriageNum);
            dialogGrid.Children.Remove(Username);

            dialogGrid.Children.Remove(scheduleNum);
            dialogGrid.Children.Remove(seatnum);
            dialogGrid.Children.Remove(carriageNum);
            dialogGrid.Children.Remove(username); ;

            scheduleNum.SelectedIndex = 0;
            seatnum.Text = null;
            carriageNum.SelectedIndex = 0;
            username.SelectedIndex = 0;
        }

        // Verified
        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (scheduleNum.Items.Count < 1)
            {
                MessageBox.Show("Рейсы отсутствуют в базе данных! Сначала заполните ХОТЯ БЫ ОДИН рейс!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (seatnum.Text == null || seatnum.Text == "")
            {
                MessageBox.Show("Укажите место!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (username.Items.Count < 1)
            {
                MessageBox.Show("Пользователи отсутствуют в базе данных! Сначала заполните ХОТЯ БЫ ОДНОГО пользователя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TicketsM temp = new TicketsM();
            temp._schedule_id = Convert.ToInt32(scheduleNum.SelectedItem);
            temp._seatnum = int.Parse(seatnum.Text);
            temp._carriage_number = Convert.ToInt32(carriageNum.SelectedItem);
            temp._user_id = (from o in dbContext.UsersMs
                             where o._login == username.Text
                             select o._user_id).FirstOrDefault();

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.TicketsMs.Local.Add(temp);

                // Очищаем поля
                scheduleNum.SelectedIndex = 0;
                seatnum.Text = null;
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
                ticket._schedule_id = temp._schedule_id;
                ticket._seatnum = temp._seatnum;
                ticket._carriage_number = temp._carriage_number;
                ticket._user_id = temp._user_id;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(ticket).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.TicketsMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }

        // Verified
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

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }
    }
}
