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
    public class SchedulesVM : ObjectModel
    {
        public BindingList<SchedulesM> SourceList { get; set; }
        public BindingList<RoutesM> routes { get; set; }
        public BindingList<TrainsM> trains { get; set; }
        private AppDBContext dbContext;

        private TextBlock TrainID, RouteName, DepartureDate, ArrivalDate;
        private ComboBox trainID, routeName;
        private TextBox departureDate, arrivalDate;
        private bool isAdd;
        private int index;

        public SchedulesVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.SchedulesMs.Load();
            dbContext.RoutesMs.Load();
            dbContext.TrainsMs.Load();

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
                SchedulesM temp = SourceList[index];
                trainID.SelectedItem = temp._train_id;
                routeName.SelectedItem = temp.RoutesM._route_name; 
                DateTime startdate = DateTime.MinValue.AddMilliseconds(temp._departure_datetime);
                DateTime enddate = DateTime.MinValue.AddMilliseconds(temp._arrival_datetime);
                departureDate.Text = startdate.ToString("dd.MM.yyyy");
                arrivalDate.Text = enddate.ToString("dd.MM.yyyy");

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(TrainID);
            dialogGrid.Children.Add(RouteName);
            dialogGrid.Children.Add(DepartureDate);
            dialogGrid.Children.Add(ArrivalDate);

            dialogGrid.Children.Add(trainID);
            dialogGrid.Children.Add(routeName);
            dialogGrid.Children.Add(departureDate);
            dialogGrid.Children.Add(arrivalDate);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(TrainID);
            dialogGrid.Children.Remove(RouteName);
            dialogGrid.Children.Remove(DepartureDate);
            dialogGrid.Children.Remove(ArrivalDate);

            dialogGrid.Children.Remove(trainID);
            dialogGrid.Children.Remove(routeName);
            dialogGrid.Children.Remove(departureDate);
            dialogGrid.Children.Remove(arrivalDate);

            routeName.SelectedIndex = 0;
            departureDate.Text = null;
            arrivalDate.Text = null;
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

            SchedulesM temp = new SchedulesM();
            temp._train_id = Convert.ToInt32(trainID.SelectedItem);
            temp.RoutesM._route_name = routeName.Text; 
            dateTimeDeparture = DateTime.ParseExact(departureDate.Text, "dd.MM.yyyy", null);
            temp._departure_datetime = dateTimeDeparture.Subtract(DateTime.MinValue).TotalMilliseconds;
            dateTimeArrival = DateTime.ParseExact(departureDate.Text, "dd.MM.yyyy", null);
            temp._arrival_datetime = dateTimeArrival.Subtract(DateTime.MinValue).TotalMilliseconds;

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
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._route_id;
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
