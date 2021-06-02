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

		private AppDBContext dbContext;

        private TextBlock TrainID, RouteName, DepartureDate, ArrivalDate, Status, Price, Seatnum, CarriageNum;
        private ComboBox trainID, routeName, status, seatnum, carriageNum;
        private TextBox departureDate, arrivalDate, price;
        private bool isAdd;
        private int index;

        // Verified
        public SchedulesVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();

            dbContext.RoutesMs.Load();
            routes = dbContext.RoutesMs.Local.ToBindingList();

            if (Settings.Default.IsAdmin == false)
            {
                dbContext.TicketsMs.Load();
            }

            dbContext.TrainsMs.Load();
            trains = dbContext.TrainsMs.Local.ToBindingList();

            dbContext.SchedulesMs.Load();

            // Margin
            Thickness temp = new Thickness(5);

            TrainID = new TextBlock();
            TrainID.SetResourceReference(TextBlock.TextProperty, "Text_TrainId");
            Grid.SetRow(TrainID, 0);
            Grid.SetColumn(TrainID, 0);

            RouteName = new TextBlock();
            RouteName.SetResourceReference(TextBlock.TextProperty, "Text_RouteName");
            Grid.SetRow(RouteName, 1);
            Grid.SetColumn(RouteName, 0);

            DepartureDate = new TextBlock();
            DepartureDate.SetResourceReference(TextBlock.TextProperty, "Text_DepartureDate");
            Grid.SetRow(DepartureDate, 2);
            Grid.SetColumn(DepartureDate, 0);

            ArrivalDate = new TextBlock();
            ArrivalDate.SetResourceReference(TextBlock.TextProperty, "Text_ArrivalDate");
            Grid.SetRow(ArrivalDate, 3);
            Grid.SetColumn(ArrivalDate, 0);

            Status = new TextBlock();
            Status.SetResourceReference(TextBlock.TextProperty, "Text_Status");
            Grid.SetRow(Status, 4);
            Grid.SetColumn(Status, 0);

            Price = new TextBlock();
            Price.SetResourceReference(TextBlock.TextProperty, "Text_Price");
            Grid.SetRow(Price, 5);
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

            status = new ComboBox();
            status.Margin = temp;
            Grid.SetRow(status, 4);
            Grid.SetColumn(status, 1);

            string active = (string)App.Current.Resources["Text_Active"];
            string onTheWay = (string)App.Current.Resources["Text_OnTheWay"];
            string closed = (string)App.Current.Resources["Text_Closed"];
            
            // Назначение элементов комбобокс
            status.Items.Insert(0, active);
            status.Items.Insert(1, onTheWay);
            status.Items.Insert(2, closed);
            status.SelectedIndex = 0;

            price = new TextBox();
            price.Margin = temp;
            Grid.SetRow(price, 5);
            Grid.SetColumn(price, 1);

            if (Settings.Default.IsAdmin == false)
            {
                Seatnum = new TextBlock();
                // Seatnum.Text = App.Current.Resources["Text_SeatNum"] + ":";
                Seatnum.SetResourceReference(TextBlock.TextProperty, "Text_SeatNum");
                Grid.SetRow(Seatnum, 6);
                Grid.SetColumn(Seatnum, 0);

                CarriageNum = new TextBlock();
                // CarriageNum.Text = App.Current.Resources["Text_CarriageNum"] + ":";
                CarriageNum.SetResourceReference(TextBlock.TextProperty, "Text_CarriageNum");
                Grid.SetRow(CarriageNum, 7);
                Grid.SetColumn(CarriageNum, 0);

                seatnum = new ComboBox();
                seatnum.Margin = temp;
                Grid.SetRow(seatnum, 6);
                Grid.SetColumn(seatnum, 1);

                carriageNum = new ComboBox();
                carriageNum.Margin = temp;
                Grid.SetRow(carriageNum, 7);
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

        // Verified
        public void ConnectAndUpdate()
        {
            TableV.Current_Button_Delete.Visibility = Visibility.Visible;

            if (Properties.Settings.Default.IsAdmin)
            {
                TableV.Current_Button_Delete.Content = (string)App.Current.Resources["Text_Delete"];
            } else
            {
                TableV.Current_Button_Delete.Content = (string)App.Current.Resources["Text_Buy"];
            }

            dbContext = AppDBContext.GetInstance();
            dbContext.RoutesMs.Load();
            routes = dbContext.RoutesMs.Local.ToBindingList();

            dbContext.TrainsMs.Load();
            trains = dbContext.TrainsMs.Local.ToBindingList();

            dbContext.SchedulesMs.Load();
            SourceList = dbContext.SchedulesMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();

            status.Items.Clear();

            string active = (string)App.Current.Resources["Text_Active"];
            string onTheWay = (string)App.Current.Resources["Text_OnTheWay"];
            string closed = (string)App.Current.Resources["Text_Closed"];

            // Назначение элементов комбобокс
            status.Items.Insert(0, active);
            status.Items.Insert(1, onTheWay);
            status.Items.Insert(2, closed);
            status.SelectedIndex = 0;

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

        bool isReset = false;
        public void Request()
        {
            if (!isReset)
            {
                // Создаём диалоговое окно
                DialogV dialogV = new DialogV();
                Grid dialogGrid = dialogV.Dialog_Grid;

                // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия
                Button button = dialogV.Button_Execute;
                button.SetResourceReference(Button.ContentProperty, "Text_Request");
                button.Click += new RoutedEventHandler(ExecuteRequest);

                // Вешаем элементы в Grid
                Grid.SetRow(RouteName, 0);
                Grid.SetColumn(RouteName, 0);
                dialogGrid.Children.Add(RouteName);

                Grid.SetRow(routeName, 0);
                Grid.SetColumn(routeName, 1);
                dialogGrid.Children.Add(routeName);

                // Показываем диалоговое окно
                dialogV.ShowDialog();

                // Очищаем Grid
                dialogGrid.Children.Remove(routeName);
                dialogGrid.Children.Remove(routeName);

                Grid.SetRow(routeName, 5);
                Grid.SetColumn(routeName, 1);
                Grid.SetRow(RouteName, 0);
                Grid.SetColumn(RouteName, 0);

                // Очищаем поля
                routeName.Text = null;
            }
            else
            {
                TableV.Current_Button_Request.SetResourceReference(Button.ContentProperty, "Text_Request");

                TableV.Current_DataGrid.ItemsSource = SourceList;
                TableV.Current_DataGrid.Items.Refresh();

                isReset = false;
            }
        }

        public void ExecuteRequest(object sender, RoutedEventArgs e)
        {
            if (routeName.Text == null)
            {
                MessageBox.Show("Укажите маршрут!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var routeID = (from o in dbContext.RoutesMs
                              where o._route_name == routeName.SelectedItem.ToString()
                              select o._route_id).FirstOrDefault();


            if (routeID.ToString() == null)
            {
                MessageBox.Show("Пути не найдено!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            BindingList<SchedulesM> temp = new BindingList<SchedulesM>();

            for (int i = 0; i < SourceList.Count; i++)
            {
                if (SourceList[i]._route_id == routeID)
                {
                    temp.Add(SourceList[i]);
                }
			}

            TableV.Current_Button_Request.SetResourceReference(Button.ContentProperty, "Text_Reset");

            TableV.Current_DataGrid.ItemsSource = temp;
            TableV.Current_DataGrid.Items.Refresh();

            isReset = true;
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
                button.Content = App.Current.Resources["Text_Add"];
                index = -1;

                trainID.SelectedIndex = 0;
                routeName.SelectedIndex = 0;
                status.SelectedIndex = 0;
            }
            else
            if (Settings.Default.IsAdmin == false) // Покупка
            {
                // если ничего не выбрано в датагриде то ошибка
                // если выбрано больше 1 элемента то тоже ошибка
                if (TableV.Current_DataGrid.SelectedItems.Count < 1)
                {
                    MessageBox.Show("Выберите элемент для покупки!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
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
                price.Text = temp._price.ToString();
                price.IsReadOnly = true;

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

                departureDate.Text = startdate.ToString("dd.MM.yyyy HH:mm");
                arrivalDate.Text = enddate.ToString("dd.MM.yyyy HH:mm");

                status.SelectedIndex = temp._status;

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
                temp._user_id = (from o in dbContext.UsersMs
                                 where o._login == user._login
                                 select o._user_id).FirstOrDefault();

                // Добавляем объект в БД
                dbContext.TicketsMs.Local.Add(temp);
                dbContext.SaveChanges();

                MessageBox.Show(App.Current.Resources["Text_SuccessBuy"].ToString() + "", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Verified
        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (trainID.Items.Count < 1)
            {
                MessageBox.Show("Поезда отсутствуют в базе данных! Сначала добавьте ХОТЯ БЫ ОДИН поезд!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (routeName.Items.Count < 1)
            {
                MessageBox.Show("Маршруты отсутствуют в базе данных! Сначала добавьте ХОТЯ БЫ ОДИН маршрут!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (departureDate.Text == null || departureDate.Text == "")
            {
                MessageBox.Show("Укажите дату и время отправления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime dateTimeDeparture;
            if (!DateTime.TryParseExact(departureDate.Text, "dd.MM.yyyy HH:mm", App.Language, System.Globalization.DateTimeStyles.None, out dateTimeDeparture))
            {
                MessageBox.Show("Дата указана некорректно!\nУкажите дату и время в виде: \"dd.MM.yyyy HH:mm\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (arrivalDate.Text == null || arrivalDate.Text == "")
            {
                MessageBox.Show("Укажите дату и время прибытия!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime dateTimeArrival;
            if (!DateTime.TryParseExact(arrivalDate.Text, "dd.MM.yyyy HH:mm", App.Language, System.Globalization.DateTimeStyles.None, out dateTimeArrival))
            {
                MessageBox.Show("Дата указана некорректно!\nУкажите дату и время в виде: \"dd.MM.yyyy HH:mm\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (price.Text == null || price.Text == "")
            {
                MessageBox.Show("Укажите цену на данный рейс!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double result;
            if (!double.TryParse(price.Text, out result))
            {
                MessageBox.Show("Некорректная цена!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var oneActive = (from o in dbContext.SchedulesMs
                             where (o._train_id == (int)trainID.SelectedItem && o._status == 0)
                             select o).FirstOrDefault();

            if (oneActive != null && (isAdd || oneActive._schedule_id != SourceList[index]._schedule_id))
            {
                MessageBox.Show("Данный поезд уже двигается по другому маршруту! Пожалуйста, выберите другой поезд!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SchedulesM temp = new SchedulesM();
            temp._train_id = Convert.ToInt32(trainID.SelectedItem);

            temp._route_id = (from o in dbContext.RoutesMs
                              where o._route_name == routeName.SelectedItem.ToString()
                              select o._route_id).FirstOrDefault();

            dateTimeDeparture = DateTime.ParseExact(departureDate.Text, "dd.MM.yyyy HH:mm", null);
            temp._departure_datetime = dateTimeDeparture.Subtract(DateTime.MinValue).TotalMilliseconds;

            dateTimeArrival = DateTime.ParseExact(arrivalDate.Text, "dd.MM.yyyy HH:mm", null);
            temp._arrival_datetime = dateTimeArrival.Subtract(DateTime.MinValue).TotalMilliseconds;

            temp._status = status.SelectedIndex;
            temp._price = double.Parse(price.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.SchedulesMs.Local.Add(temp);

                // Очищаем поля
                trainID.SelectedIndex = 0;
                routeName.SelectedIndex = 0;
                status.SelectedIndex = 0;
                departureDate.Text = null;
                arrivalDate.Text = null;
                price.Text = null;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._schedule_id;
                var sched = dbContext.SchedulesMs.Local
                    .Single(o => o._schedule_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                sched._train_id = temp._train_id;
                sched._route_id = temp._route_id;
                sched._departure_datetime = temp._departure_datetime;
                sched._arrival_datetime = temp._arrival_datetime;
                sched._status = temp._status;
                sched._price = temp._price;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(sched).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.SchedulesMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }

        // Verified
        public void Delete()
        {
            if (BataDase.Properties.Settings.Default.IsAdmin)
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

                    var tickets = (from o in dbContext.TicketsMs
                                   where o._schedule_id == deleteEntity._schedule_id
                                   select o);

                    if (tickets != null)
                        dbContext.TicketsMs.RemoveRange(tickets);

                    dbContext.SchedulesMs.Local.Remove(deleteEntity);

                    // Удаляем НЕконтекстного юзера из SourceList
                    SourceList.Remove(deleteEntity);
                }

                // Сохраняем контекст БД
                dbContext.SaveChanges();

                TableV.Current_DataGrid.ItemsSource = SourceList;
                TableV.Current_DataGrid.Items.Refresh();
            } else
            {
                if (TableV.Current_DataGrid.SelectedItems.Count < 1)
                {
                    MessageBox.Show("Выберите рейс, на который хотите купить билет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (TableV.Current_DataGrid.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Можно купить не более ОДНОГО билета за раз!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Покупка билетов тут
                AddEdit(false);
            }
        }
    }
}
