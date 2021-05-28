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

        private TextBlock RouteName, StartCity, FinishCity;
        private TextBox routeName;
        private ComboBox startCity, finishCity;
		private bool isAdd;
		private int index;

		public RoutesVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.CitiesMs.Load();

            cities = dbContext.CitiesMs.Local.ToBindingList();

            // Margin
            Thickness temp = new Thickness(5);

            RouteName = new TextBlock();
            RouteName.Text = App.Current.Resources["Text_RouteName"] + ":";
            Grid.SetRow(RouteName, 0);
            Grid.SetColumn(RouteName, 0);

            StartCity = new TextBlock();
            StartCity.Text = App.Current.Resources["Text_Start"] + ":";
            Grid.SetRow(StartCity, 1);
            Grid.SetColumn(StartCity, 0);

            FinishCity = new TextBlock();
            FinishCity.Text = App.Current.Resources["Text_Finish"] + ":";
            Grid.SetRow(FinishCity, 2);
            Grid.SetColumn(FinishCity, 0);

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

            SourceList = dbContext.RoutesMs.Local.ToBindingList();
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
            dbContext = AppDBContext.GetInstance();
            dbContext.CitiesMs.Load();
            cities = dbContext.CitiesMs.Local.ToBindingList();

            dbContext.RoutesMs.Load();
            SourceList = dbContext.RoutesMs.Local.ToBindingList();
            TableV.Current_DataGrid.ItemsSource = SourceList;

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
                RoutesM temp = SourceList[index];

                routeName.Text = temp._route_name;
                startCity.SelectedItem = temp.CitiesM_Start._city_name;
                finishCity.SelectedItem = temp.CitiesM_Finish._city_name;

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(RouteName);
            dialogGrid.Children.Add(StartCity);
            dialogGrid.Children.Add(FinishCity);

            dialogGrid.Children.Add(routeName);
            dialogGrid.Children.Add(startCity);
            dialogGrid.Children.Add(finishCity);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(RouteName);
            dialogGrid.Children.Remove(StartCity);
            dialogGrid.Children.Remove(FinishCity);

            dialogGrid.Children.Remove(routeName);
            dialogGrid.Children.Remove(startCity);
            dialogGrid.Children.Remove(finishCity);

            routeName.Text = null;
            startCity.SelectedIndex = 0;
            finishCity.SelectedIndex = 0;
        }

        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (routeName.Text == null)
            {
                MessageBox.Show("Укажите название маршрута!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (startCity.Text == null)
            {
                MessageBox.Show("Укажите пункт отправления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (finishCity.Text == null)
            {
                MessageBox.Show("Укажите пункт назначения!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RoutesM temp = new RoutesM();
            temp._route_name = routeName.Text;
            temp.CitiesM_Start._city_name = startCity.Text;
            temp.CitiesM_Finish._city_name = finishCity.Text;

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
                route.CitiesM_Start._city_name = temp.CitiesM_Start._city_name;
                route.CitiesM_Finish._city_name = temp.CitiesM_Finish._city_name;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(route).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.RoutesMs.Local.ToBindingList();
        }

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

                // Удаляем поезд из расписания
                var schedules = dbContext.SchedulesMs
                    .Where(o => o._route_id == deleteEntity._route_id);

                if (schedules != null)
                    dbContext.SchedulesMs.RemoveRange(schedules);

                var contextDeleteEntity = dbContext.SchedulesMs.Local
                    .Single(o => o._route_id == deleteEntity._route_id);

                dbContext.SchedulesMs.Local.Remove(contextDeleteEntity);

                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
