using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.Views;
using BataDase.Properties;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class SchedulesVM : ObjectModel
    {
        public BindingList<SchedulesM> SourceList { get; set; }
        public BindingList<RoutesM> routes { get; set; }
        public BindingList<TrainsM> trains { get; set; }
        public BindingList<TicketsM> tickets { get; set; }
        private AppDBContext dbContext;

        private TextBlock TrainID, RouteName, DepartureDate, ArrivalDate, Price, Seatnum, CarriageNum;
        private ComboBox trainID, routeName, seatnum, carriageNum;
        private TextBox departureDate, arrivalDate, price;
        private bool isAdd;
        private int index;

        public SchedulesVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.SchedulesMs.Load();
            dbContext.RoutesMs.Load();
            dbContext.TrainsMs.Load();
            
            if(Settings.Default.IsAdmin == false)
            {
                dbContext.TicketsMs.Load();

                tickets = dbContext.TicketsMs.Local.ToBindingList();
            }

            routes = dbContext.RoutesMs.Local.ToBindingList();
            trains = dbContext.TrainsMs.Local.ToBindingList();

            // Margin
            Thickness temp = new Thickness(5);

            TrainID = new TextBlock();
            TrainID.Text = App.Current.Resources["Text_TrainID"] + ":";
            Grid.SetRow(TrainID, 0);
            Grid.SetColumn(TrainID, 0);

            RouteName = new TextBlock();
            RouteName.Text = App.Current.Resources["Text_RouteName"] + ":";
            Grid.SetRow(RouteName, 1);
            Grid.SetColumn(RouteName, 0);

            DepartureDate = new TextBlock();
            DepartureDate.Text = App.Current.Resources["Text_DepartureDate"] + ":";
            Grid.SetRow(DepartureDate, 2);
            Grid.SetColumn(DepartureDate, 0);

            ArrivalDate = new TextBlock();
            ArrivalDate.Text = App.Current.Resources["Text_ArrivalDate"] + ":";
            Grid.SetRow(ArrivalDate, 3);
            Grid.SetColumn(ArrivalDate, 0);

            Price = new TextBlock();
            Price.Text = App.Current.Resources["Text_Price"] + ":";
            Grid.SetRow(Price, 4);
            Grid.SetColumn(Price, 0);

            trainID = new ComboBox();
            trainID.Margin = temp;
            Grid.SetRow(trainID, 0);
            Grid.SetColumn(trainID, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < trains.Count; i++)
            {
                trainID.Items.Insert(i, trains[i]._train_id);
            }

            routeName = new ComboBox();
            routeName.Margin = temp;
            Grid.SetRow(routeName, 1);
            Grid.SetColumn(routeName, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < routes.Count; i++)
            {
                routeName.Items.Insert(i, routes[i]._route_name);
            }

            departureDate = new TextBox();
            departureDate.Margin = temp;
            Grid.SetRow(departureDate, 2);
            Grid.SetColumn(departureDate, 1);

            arrivalDate = new TextBox();
            arrivalDate.Margin = temp;
            Grid.SetRow(arrivalDate, 3);
            Grid.SetColumn(arrivalDate, 1);

            price = new TextBox();
            price.Margin = temp;
            Grid.SetRow(price, 4);
            Grid.SetColumn(price, 1);

            if (Settings.Default.IsAdmin == false)
            {
                Seatnum = new TextBlock();
                Seatnum.Text = App.Current.Resources["Text_SeatNum"] + ":";
                Grid.SetRow(Seatnum, 5);
                Grid.SetColumn(Seatnum, 0);

                CarriageNum = new TextBlock();
                CarriageNum.Text = App.Current.Resources["Text_CarriageNum"] + ":";
                Grid.SetRow(CarriageNum, 6);
                Grid.SetColumn(CarriageNum, 0);

                seatnum = new ComboBox();
                seatnum.Margin = temp;
                Grid.SetRow(seatnum, 5);
                Grid.SetColumn(seatnum, 1);

                carriageNum = new ComboBox();
                carriageNum.Margin = temp;
                Grid.SetRow(carriageNum, 6);
                Grid.SetColumn(carriageNum, 1);

                // Назначение элементов комбобокс
                for (int i = 0; i < 50; i++)
                {
                    seatnum.Items.Insert(i, i + 1);
                }
                for (int i = 0; i < 5; i++)
                {
                    carriageNum.Items.Insert(i, i + 1);
                }
            }

            SourceList = dbContext.SchedulesMs.Local.ToBindingList();
        }

        public void ConnectAndUpdate()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.RoutesMs.Load();
            routes = dbContext.RoutesMs.Local.ToBindingList();

            dbContext.TrainsMs.Load();
            trains = dbContext.TrainsMs.Local.ToBindingList();

            dbContext.SchedulesMs.Load();
            SourceList = dbContext.SchedulesMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;

            routeName.Items.Clear();

            for (int i = 0; i < routes.Count; i++)
            {
                routeName.Items.Insert(i, routes[i]._route_name);
            }

            trainID.Items.Clear();

            for (int i = 0; i < trains.Count; i++)
            {
                trainID.Items.Insert(i, trains[i]._train_id);
            }
        }

        public void Request()
        {
            MessageBox.Show("Запрос не реализован!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
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
            else
            if (Settings.Default.IsAdmin == false) // Покупка
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
                SchedulesM temp = SourceList[index];
                trainID.SelectedItem = temp._train_id;
                trainID.IsReadOnly = true;
                routeName.SelectedItem = temp.RoutesM._route_name;
                routeName.IsReadOnly = true;
                DateTime startdate = DateTime.MinValue.AddMilliseconds(temp._departure_datetime);
                DateTime enddate = DateTime.MinValue.AddMilliseconds(temp._arrival_datetime);
                departureDate.Text = startdate.ToString("dd.MM.yyyy");
                departureDate.IsReadOnly = true;
                arrivalDate.Text = enddate.ToString("dd.MM.yyyy");
                arrivalDate.IsReadOnly = true;
                price.Text = temp._price.ToString() + App.Current.Resources["Text_Rubles"] + "";
                price.IsReadOnly = true;
                carriageNum.SelectedIndex = 0;
                seatnum.SelectedIndex = 0;

                button.Content = App.Current.Resources["Text_Buy"];
            }
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
                SchedulesM temp = SourceList[index];
                trainID.SelectedItem = temp._train_id;
                routeName.SelectedItem = temp.RoutesM._route_name; 
                DateTime startdate = DateTime.MinValue.AddMilliseconds(temp._departure_datetime);
                DateTime enddate = DateTime.MinValue.AddMilliseconds(temp._arrival_datetime);
                departureDate.Text = startdate.ToString("dd.MM.yyyy");
                arrivalDate.Text = enddate.ToString("dd.MM.yyyy");
                price.Text = temp._price.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            if (Settings.Default.IsAdmin == false)
            {
                button.Click += new RoutedEventHandler(ExecuteBuy);

                // Вешаем элементы в Grid
                dialogGrid.Children.Add(TrainID);
                dialogGrid.Children.Add(RouteName);
                dialogGrid.Children.Add(DepartureDate);
                dialogGrid.Children.Add(ArrivalDate);
                dialogGrid.Children.Add(Price); 
                dialogGrid.Children.Add(Seatnum);
                dialogGrid.Children.Add(CarriageNum);

                dialogGrid.Children.Add(trainID);
                dialogGrid.Children.Add(routeName);
                dialogGrid.Children.Add(departureDate);
                dialogGrid.Children.Add(arrivalDate);
                dialogGrid.Children.Add(price);
                dialogGrid.Children.Add(seatnum);
                dialogGrid.Children.Add(carriageNum);

                // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

                dialogV.ShowDialog();

                // Очищаем Grid
                dialogGrid.Children.Remove(TrainID);
                dialogGrid.Children.Remove(RouteName);
                dialogGrid.Children.Remove(DepartureDate);
                dialogGrid.Children.Remove(ArrivalDate);
                dialogGrid.Children.Remove(Price);
                dialogGrid.Children.Remove(Seatnum);
                dialogGrid.Children.Remove(CarriageNum);

                dialogGrid.Children.Remove(trainID);
                dialogGrid.Children.Remove(routeName);
                dialogGrid.Children.Remove(departureDate);
                dialogGrid.Children.Remove(arrivalDate);
                dialogGrid.Children.Remove(price);
                dialogGrid.Children.Remove(seatnum);
                dialogGrid.Children.Remove(carriageNum);

                routeName.SelectedIndex = 0;
                departureDate.Text = null;
                arrivalDate.Text = null;
                price.Text = null;
                seatnum.SelectedIndex = 0;
                carriageNum.SelectedIndex = 0;
            }
            else
            {
                button.Click += new RoutedEventHandler(ExecuteAddEdit);

                // Вешаем элементы в Grid
                dialogGrid.Children.Add(TrainID);
                dialogGrid.Children.Add(RouteName);
                dialogGrid.Children.Add(DepartureDate);
                dialogGrid.Children.Add(ArrivalDate);
                dialogGrid.Children.Add(Price);

                dialogGrid.Children.Add(trainID);
                dialogGrid.Children.Add(routeName);
                dialogGrid.Children.Add(departureDate);
                dialogGrid.Children.Add(arrivalDate);
                dialogGrid.Children.Add(price);

                // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

                dialogV.ShowDialog();

                // Очищаем Grid
                dialogGrid.Children.Remove(TrainID);
                dialogGrid.Children.Remove(RouteName);
                dialogGrid.Children.Remove(DepartureDate);
                dialogGrid.Children.Remove(ArrivalDate);
                dialogGrid.Children.Remove(Price);

                dialogGrid.Children.Remove(trainID);
                dialogGrid.Children.Remove(routeName);
                dialogGrid.Children.Remove(departureDate);
                dialogGrid.Children.Remove(arrivalDate);
                dialogGrid.Children.Remove(price);

                routeName.SelectedIndex = 0;
                departureDate.Text = null;
                arrivalDate.Text = null;
                price.Text = null;
            }
        }

        public void ExecuteBuy(object sender, RoutedEventArgs e)
        {
            float localPrice = float.Parse(price.Text);
            var user = dbContext.UsersMs.Local
                    .Single(o => o._user_id == Settings.Default.CurrentUserId);

            if (user._bill < localPrice)
            {
                MessageBox.Show(App.Current.Resources["Text_NotEnoughMoney"].ToString() + "", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                user._bill -= localPrice;

                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.SaveChanges();

                // Создание билета после покупки:
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
                TicketsM temp = new TicketsM();
                temp._seatnum = Convert.ToInt32(seatnum.SelectedItem);
                temp._carriage_number = Convert.ToInt32(carriageNum.SelectedItem);
                temp.UsersM._login = user._login;

                // Добавляем объект в БД
                dbContext.TicketsMs.Local.Add(temp);
                dbContext.SaveChanges();

                MessageBox.Show(App.Current.Resources["Text_SuccessBuy"].ToString() + "", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (trainID.Text == null)
            {
                MessageBox.Show("Укажите ID поезда!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (routeName.Text == null)
            {
                MessageBox.Show("Укажите название маршрута!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (departureDate.Text == null || departureDate.Text == "")
            {
                MessageBox.Show("Укажите время отправления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime dateTimeDeparture;
            if (!DateTime.TryParseExact(departureDate.Text, "dd.MM.yyyy", App.Language, System.Globalization.DateTimeStyles.None, out dateTimeDeparture))
            {
                MessageBox.Show("Дата указана некорректно!\nУкажите дату в виде: \"dd.MM.yyyy\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (arrivalDate.Text == null || arrivalDate.Text == "")
            {
                MessageBox.Show("Укажите время прибытия!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime dateTimeArrival;
            if (!DateTime.TryParseExact(arrivalDate.Text, "dd.MM.yyyy", App.Language, System.Globalization.DateTimeStyles.None, out dateTimeArrival))
            {
                MessageBox.Show("Дата указана некорректно!\nУкажите дату в виде: \"dd.MM.yyyy\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            float result;
            if (price.Text == null || price.Text == "")
            {
                price.Text = "0";
            }
            else if (!float.TryParse(price.Text, out result))
            {
                MessageBox.Show("Некорректная цена!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SchedulesM temp = new SchedulesM();
            temp._train_id = Convert.ToInt32(trainID.SelectedItem);
            temp.RoutesM._route_name = routeName.Text; 
            dateTimeDeparture = DateTime.ParseExact(departureDate.Text, "dd.MM.yyyy", null);
            temp._departure_datetime = dateTimeDeparture.Subtract(DateTime.MinValue).TotalMilliseconds;
            dateTimeArrival = DateTime.ParseExact(departureDate.Text, "dd.MM.yyyy", null);
            temp._arrival_datetime = dateTimeArrival.Subtract(DateTime.MinValue).TotalMilliseconds;
            temp._price = float.Parse(price.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.SchedulesMs.Local.Add(temp);

                // Очищаем поля
                trainID.SelectedIndex = 0;
                routeName.SelectedIndex = 0;
                departureDate.Text = null;
                arrivalDate.Text = null;
            }
            else if(Settings.Default.IsAdmin == false)
            {
                // Добавляем объект в БД
                dbContext.SchedulesMs.Local.Add(temp);

                // Очищаем поля
                trainID.SelectedIndex = 0;
                routeName.SelectedIndex = 0;
                departureDate.Text = null;
                arrivalDate.Text = null;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._schedule_id;
                var sched = dbContext.SchedulesMs.Local
                    .Single(o => o._schedule_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                sched._train_id = temp._train_id;
                sched.RoutesM._route_name = temp.RoutesM._route_name;
                sched._departure_datetime = temp._departure_datetime;
                sched._arrival_datetime = temp._arrival_datetime;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(sched).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.SchedulesMs.Local.ToBindingList();
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
                
                SchedulesM deleteEntity = SourceList[index];

                var tickets = dbContext.TicketsMs
                    .Where(o => o._schedule_id == deleteEntity._schedule_id);

                if (tickets != null)
                    dbContext.TicketsMs.RemoveRange(tickets);

                var contextDeleteEntity = dbContext.SchedulesMs.Local
                    .Single(o => o._schedule_id == deleteEntity._schedule_id);

                // Удаляем контекстного юзера из контекста
                dbContext.SchedulesMs.Local.Remove(contextDeleteEntity);

                // Удаляем НЕконтекстного юзера из SourceList
                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
