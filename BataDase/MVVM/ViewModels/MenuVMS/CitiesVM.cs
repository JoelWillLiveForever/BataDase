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
    public class CitiesVM : ObjectModel
    {
        private bool isAdd;
        private int index;

        public BindingList<CitiesM> SourceList { get; set; }
        private AppDBContext dbContext;

        private TextBlock CityName, Latitude, Longitude;
        private TextBox cityName, latitude, longitude;

        public CitiesVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();
            dbContext.LocomotivesMs.Load();

            // Margin
            Thickness temp = new Thickness(5);

            CityName = new TextBlock();
            CityName.Text = App.Current.Resources["Text_City"] + ":";
            Grid.SetRow(CityName, 0);
            Grid.SetColumn(CityName, 0);

            Latitude = new TextBlock();
            Latitude.Text = App.Current.Resources["Text_Latitude"] + ":";
            Grid.SetRow(Latitude, 1);
            Grid.SetColumn(Latitude, 0);

            Longitude = new TextBlock();
            Longitude.Text = App.Current.Resources["Text_Longitude"] + ":";
            Grid.SetRow(Longitude, 2);
            Grid.SetColumn(Longitude, 0);

            cityName = new TextBox();
            cityName.Margin = temp;
            Grid.SetRow(cityName, 0);
            Grid.SetColumn(cityName, 1);

            latitude = new TextBox();
            latitude.Margin = temp;
            Grid.SetRow(latitude, 1);
            Grid.SetColumn(latitude, 1);

            longitude = new TextBox();
            longitude.Margin = temp;
            Grid.SetRow(longitude, 2);
            Grid.SetColumn(longitude, 1);

            SourceList = dbContext.CitiesMs.Local.ToBindingList();
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

            SourceList = dbContext.CitiesMs.Local.ToBindingList();
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
                index = TableV.Current_DataGrid.SelectedIndex;

                CitiesM temp = SourceList[index];

                cityName.Text = temp._city_name;
                latitude.Text = temp._latitude.ToString();
                longitude.Text = temp._longitude.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия
            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(CityName);
            dialogGrid.Children.Add(Latitude);
            dialogGrid.Children.Add(Longitude);

            dialogGrid.Children.Add(cityName);
            dialogGrid.Children.Add(latitude);
            dialogGrid.Children.Add(longitude);

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(CityName);
            dialogGrid.Children.Remove(Latitude);
            dialogGrid.Children.Remove(Longitude);

            dialogGrid.Children.Remove(cityName);
            dialogGrid.Children.Remove(latitude);
            dialogGrid.Children.Remove(longitude);

            // Очищаем поля
            cityName.Text = null;
            latitude.Text = null;
            longitude.Text = null;
        }

        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            // Проверки TextBox на null и пустую строку
            if (cityName.Text == null || cityName.Text == "")
            {
                MessageBox.Show("Укажите город!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            float result;
            if (latitude.Text == null || latitude.Text == "")
            {
                MessageBox.Show("Укажите широту!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(latitude.Text, out result))
            {
                MessageBox.Show("Некорректная широта!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (longitude.Text == null || longitude.Text == "")
            {
                MessageBox.Show("Укажите долготу!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(longitude.Text, out result))
            {
                MessageBox.Show("Некорректная долгота!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CitiesM temp = new CitiesM();
            temp._city_name = cityName.Text;
            temp._latitude = float.Parse(latitude.Text);
            temp._longitude = float.Parse(longitude.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.CitiesMs.Local.Add(temp);

                // Очищаем поля
                cityName.Text = null;
                latitude.Text = null;
                longitude.Text = null;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._city_id;
                var city = dbContext.CitiesMs.Local
                    .Single(o => o._city_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                city._city_name = temp._city_name;
                city._latitude = temp._latitude;
                city._longitude = temp._longitude;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(city).State = EntityState.Modified;
            }

            // Сохранение контекста БД
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.CitiesMs.Local.ToBindingList();

            // Пинаем DataGrid, ибо он тупой и по другому не понимает
            TableV.Current_DataGrid.Items.Refresh();
        }

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
                index = TableV.Current_DataGrid.SelectedIndex;

                CitiesM deleteEntity = SourceList[index];

                var tickets = dbContext.TicketsMs
                    .Where(o => o._user_id == deleteEntity._city_id);

                if (tickets != null)
                    dbContext.TicketsMs.RemoveRange(tickets);

                var contextDeleteEntity = dbContext.CitiesMs.Local
                    .Single(o => o._city_id == deleteEntity._city_id);

                // Удаляем контекстного юзера из контекста
                dbContext.CitiesMs.Local.Remove(contextDeleteEntity);

                // Удаляем НЕконтекстного юзера из SourceList
                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
