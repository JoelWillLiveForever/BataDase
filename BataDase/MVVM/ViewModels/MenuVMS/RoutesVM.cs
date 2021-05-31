using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.Views;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class RoutesVM : ObjectModel
    {
        public BindingList<RoutesM> SourceList { get; set; }
        public BindingList<CitiesM> cities { get; set; }
        private AppDBContext dbContext;

        private TextBlock RouteName, StartCity, FinishCity, Distance;
        private TextBox routeName, distance;
        private ComboBox startCity, finishCity;
		private bool isAdd;
		private int index;

        // Verified
		public RoutesVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();

            dbContext.CitiesMs.Load();
            cities = dbContext.CitiesMs.Local.ToBindingList();

            dbContext.RoutesMs.Load();

            // Margin
            Thickness temp = new Thickness(5);

            RouteName = new TextBlock();
            RouteName.SetResourceReference(TextBlock.TextProperty, "Text_RouteName");
            Grid.SetRow(RouteName, 0);
            Grid.SetColumn(RouteName, 0);

            StartCity = new TextBlock();
            StartCity.SetResourceReference(TextBlock.TextProperty, "Text_Start");
            Grid.SetRow(StartCity, 1);
            Grid.SetColumn(StartCity, 0);

            FinishCity = new TextBlock();
            FinishCity.SetResourceReference(TextBlock.TextProperty, "Text_Finish");
            Grid.SetRow(FinishCity, 2);
            Grid.SetColumn(FinishCity, 0);

            Distance = new TextBlock();
            Distance.SetResourceReference(TextBlock.TextProperty, "Text_Distance");
            Grid.SetRow(Distance, 3);
            Grid.SetColumn(Distance, 0);

            routeName = new TextBox();
            routeName.Margin = temp;
            Grid.SetRow(routeName, 0);
            Grid.SetColumn(routeName, 1);

            startCity = new ComboBox();
            startCity.Margin = temp;
            Grid.SetRow(startCity, 1);
            Grid.SetColumn(startCity, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < cities.Count; i++)
            {
                startCity.Items.Insert(i, cities[i]._city_name);
            }

            finishCity = new ComboBox();
            finishCity.Margin = temp;
            Grid.SetRow(finishCity, 2);
            Grid.SetColumn(finishCity, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < cities.Count; i++)
            {
                finishCity.Items.Insert(i, cities[i]._city_name);
            }

            distance = new TextBox();
            distance.Margin = temp;
            Grid.SetRow(distance, 3);
            Grid.SetColumn(distance, 1);

            SourceList = dbContext.RoutesMs.Local.ToBindingList();
        }

        // Verified
        public void ConnectAndUpdate()
        {
            dbContext = AppDBContext.GetInstance();

            dbContext.CitiesMs.Load();
            cities = dbContext.CitiesMs.Local.ToBindingList();

            dbContext.RoutesMs.Load();
            SourceList = dbContext.RoutesMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();

            startCity.Items.Clear();

            for (int i = 0; i < cities.Count; i++)
            {
                startCity.Items.Insert(i, cities[i]._city_name);
            }

            finishCity.Items.Clear();

            for (int i = 0; i < cities.Count; i++)
            {
                finishCity.Items.Insert(i, cities[i]._city_name);
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
                startCity.SelectedIndex = 0;
                finishCity.SelectedIndex = 0;

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
                RoutesM temp = SourceList[index];

                routeName.Text = temp._route_name;

                if (temp.CitiesM_Start != null)
                    if (temp.CitiesM_Start._city_name != null)
                        startCity.SelectedItem = temp.CitiesM_Start._city_name;

                if (temp.CitiesM_Finish != null)
                    if (temp.CitiesM_Finish._city_name != null)
                        finishCity.SelectedItem = temp.CitiesM_Finish._city_name;

                distance.Text = temp._distance.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(RouteName);
            dialogGrid.Children.Add(StartCity);
            dialogGrid.Children.Add(FinishCity);
            dialogGrid.Children.Add(Distance);

            dialogGrid.Children.Add(routeName);
            dialogGrid.Children.Add(startCity);
            dialogGrid.Children.Add(finishCity);
            dialogGrid.Children.Add(distance);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(RouteName);
            dialogGrid.Children.Remove(StartCity);
            dialogGrid.Children.Remove(FinishCity);
            dialogGrid.Children.Remove(Distance);

            dialogGrid.Children.Remove(routeName);
            dialogGrid.Children.Remove(startCity);
            dialogGrid.Children.Remove(finishCity);
            dialogGrid.Children.Remove(distance);

            routeName.Text = null;
            startCity.SelectedIndex = 0;
            finishCity.SelectedIndex = 0;
        }

        // Verified
        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (routeName.Text == null || routeName.Text == "")
            {
                MessageBox.Show("Укажите название маршрута!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (startCity.Items.Count < 2)
            {
                MessageBox.Show("Города в базе данных отсутствуют! Сначала добавьте ХОТЯ БЫ ДВА города!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (finishCity.Items.Count < 2)
            {
                MessageBox.Show("Города в базе данных отсутствуют! Сначала добавьте ХОТЯ БЫ ДВА города!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (startCity.Text == finishCity.Text)
            {
                MessageBox.Show("Нельзя проложить маршрут между одним и тем же городом!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double result;
            if (distance.Text == null || distance.Text == "")
            {
                MessageBox.Show("Укажите дистанцию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!double.TryParse(distance.Text, out result))
            {
                MessageBox.Show("Некорректное значение дистанции!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string sc = startCity.Text;
            var startCity_Id = (from d in dbContext.CitiesMs
                                where d._city_name == sc
                                select d._city_id).FirstOrDefault();

            string fc = finishCity.Text;
            var finishCity_Id = (from d in dbContext.CitiesMs
                                where d._city_name == fc
                                select d._city_id).FirstOrDefault();

            if (sc == null || fc == null)
            {
                MessageBox.Show("Выбранные города отсутствуют в базе данных!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var unique = (from o in dbContext.RoutesMs
                          where o._route_name == routeName.Text
                          select o).FirstOrDefault();

            if (unique != null && (isAdd || unique._route_id != SourceList[index]._route_id))
            {
                MessageBox.Show("Данный маршрут уже присутствует в таблице!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RoutesM temp = new RoutesM();
            temp._route_name = routeName.Text;
            temp._start_city_id = startCity_Id;
            temp._finish_city_id = finishCity_Id;
            temp._distance = double.Parse(distance.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.RoutesMs.Local.Add(temp);

                // Очищаем поля
                routeName.Text = null;
                startCity.SelectedIndex = 0;
                finishCity.SelectedIndex = 0;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._route_id;
                var route = dbContext.RoutesMs.Local
                    .Single(o => o._route_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                route._route_name = temp._route_name;
                route._start_city_id = temp._start_city_id;
                route._finish_city_id = temp._finish_city_id;
                route._distance = temp._distance;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(route).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.RoutesMs.Local.ToBindingList();

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

            while (TableV.Current_DataGrid.SelectedItems.Count > 0)
            {
                index = TableV.Current_DataGrid.SelectedIndex;

                RoutesM deleteEntity = SourceList[index];

                var schedules = (from o in dbContext.SchedulesMs
                                 where o._route_id == deleteEntity._route_id
                                 select o);

                if (schedules != null)
                {
                    foreach (var schedule in schedules)
                    {
                        var tickets = (from o in dbContext.TicketsMs
                                       where o._schedule_id == schedule._schedule_id
                                       select o);

                        if (tickets != null)
                        {
                            dbContext.TicketsMs.RemoveRange(tickets);
                        }
                    }

                    dbContext.SchedulesMs.RemoveRange(schedules);
                }

                dbContext.RoutesMs.Local.Remove(deleteEntity);

                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }
    }
}
