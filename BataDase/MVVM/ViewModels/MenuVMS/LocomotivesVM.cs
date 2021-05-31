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
    public class LocomotivesVM : ObjectModel
    {
        private bool isAdd;
        private int index;

        public BindingList<LocomotivesM> SourceList { get; set; }
        private AppDBContext dbContext;

        private TextBlock Model, Type, Weight, MaxTrailerWeight, AvgSpeed0, AvgSpeed100;
        private TextBox model, weight, maxTrailerWeight, avgspeed0, avgspeed100;
        private ComboBox type;

        // Verified
        public LocomotivesVM()
        {
			// Инициализация контекста БД
			dbContext = AppDBContext.GetInstance();
			dbContext.LocomotivesMs.Load();

			// Margin
			Thickness temp = new Thickness(5);

			// Назначение свойств пачке контролов
			// Лэйбл "Модель", назначение текста, строки и колонки в Grid
			Model = new TextBlock();
            Model.SetResourceReference(TextBlock.TextProperty, "Text_Model");
			Grid.SetRow(Model, 0);
			Grid.SetColumn(Model, 0);

			// Лэйбл "Тип", назначение текста, строки и колонки в Grid
			Type = new TextBlock();
            Type.SetResourceReference(TextBlock.TextProperty, "Text_Type");
			Grid.SetRow(Type, 1);
			Grid.SetColumn(Type, 0);

			// Лэйбл "Вес", назначение текста, строки и колонки в Grid
			Weight = new TextBlock();
            Weight.SetResourceReference(TextBlock.TextProperty, "Text_Weight");
			Grid.SetRow(Weight, 2);
			Grid.SetColumn(Weight, 0);

			// Лэйбл "Разрешённая ММ", назначение текста, строки и колонки в Grid
			MaxTrailerWeight = new TextBlock();
            MaxTrailerWeight.SetResourceReference(TextBlock.TextProperty, "Text_MaxTrailerWeight");
			Grid.SetRow(MaxTrailerWeight, 3);
			Grid.SetColumn(MaxTrailerWeight, 0);

			// Лэйбл "Средняя скорость 0", назначение текста, строки и колонки в Grid
			AvgSpeed0 = new TextBlock();
            AvgSpeed0.SetResourceReference(TextBlock.TextProperty, "Text_AvgSpeed0");
			Grid.SetRow(AvgSpeed0, 4);
			Grid.SetColumn(AvgSpeed0, 0);

			// Лэйбл "Средняя скорость 100", назначение текста, строки и колонки в Grid
			AvgSpeed100 = new TextBlock();
            AvgSpeed100.SetResourceReference(TextBlock.TextProperty, "Text_AvgSpeed100");
            AvgSpeed100.TextWrapping = 0;
            Grid.SetRow(AvgSpeed100, 5);
			Grid.SetColumn(AvgSpeed100, 0);

			// Текстбокс "Модель", назначение отсутпа (Margin), строки и колонки в Grid
			model = new TextBox();
			model.Margin = temp;
			Grid.SetRow(model, 0);
			Grid.SetColumn(model, 1);

			// Текстбокс "Вес", назначение отсутпа (Margin), строки и колонки в Grid
			weight = new TextBox();
			weight.Margin = temp;
			Grid.SetRow(weight, 2);
			Grid.SetColumn(weight, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			maxTrailerWeight = new TextBox();
			maxTrailerWeight.Margin = temp;
			Grid.SetRow(maxTrailerWeight, 3);
			Grid.SetColumn(maxTrailerWeight, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			avgspeed0 = new TextBox();
			avgspeed0.Margin = temp;
			Grid.SetRow(avgspeed0, 4);
			Grid.SetColumn(avgspeed0, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			avgspeed100 = new TextBox();
			avgspeed100.Margin = temp;
			Grid.SetRow(avgspeed100, 5);
			Grid.SetColumn(avgspeed100, 1);

            // Комбобокс "Тип", назначение отсутпа (Margin), строки и колонки в Grid
            type = new ComboBox();
            type.Margin = temp;
            Grid.SetRow(type, 1);
            Grid.SetColumn(type, 1);

            // Вытягивание полов из ресурсов
            string steamLoco = (string)App.Current.Resources["Text_SteamLoco"];
			string electroLoco = (string)App.Current.Resources["Text_ElectroLoco"];
            string heatLoco = (string)App.Current.Resources["Text_HeatLoco"];

            // Назначение элементов комбобокс
            type.Items.Insert(0, heatLoco);
			type.Items.Insert(1, electroLoco);
            type.Items.Insert(2, steamLoco);
            type.SelectedIndex = 0;

			// Инициализация списка элементов БД
			SourceList = dbContext.LocomotivesMs.Local.ToBindingList();
		}

        // Verified
        public void ConnectAndUpdate()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.LocomotivesMs.Load();

            SourceList = dbContext.LocomotivesMs.Local.ToBindingList();
            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();

            type.Items.Clear();

            // Вытягивание полов из ресурсов
            string steamLoco = (string)App.Current.Resources["Text_SteamLoco"];
            string electroLoco = (string)App.Current.Resources["Text_ElectroLoco"];
            string heatLoco = (string)App.Current.Resources["Text_HeatLoco"];

            // Назначение элементов комбобокс
            type.Items.Insert(0, heatLoco);
            type.Items.Insert(1, electroLoco);
            type.Items.Insert(2, steamLoco);
            type.SelectedIndex = 0;
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
                LocomotivesM temp = SourceList[index];

                model.Text = temp._model;
                type.SelectedIndex = temp._type;
                weight.Text = temp._weight.ToString();
                maxTrailerWeight.Text = temp._max_trailer_weight.ToString();
                avgspeed0.Text = temp._avgspeed0.ToString();
                avgspeed100.Text = temp._avgspeed100.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(Model);
            dialogGrid.Children.Add(Type);
            dialogGrid.Children.Add(Weight);
            dialogGrid.Children.Add(MaxTrailerWeight);
            dialogGrid.Children.Add(AvgSpeed0);
            dialogGrid.Children.Add(AvgSpeed100);

            dialogGrid.Children.Add(model);
            dialogGrid.Children.Add(type);
            dialogGrid.Children.Add(weight);
            dialogGrid.Children.Add(maxTrailerWeight);
            dialogGrid.Children.Add(avgspeed0);
            dialogGrid.Children.Add(avgspeed100);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия
            
            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(model);
            dialogGrid.Children.Remove(type);
            dialogGrid.Children.Remove(weight);
            dialogGrid.Children.Remove(maxTrailerWeight);
            dialogGrid.Children.Remove(avgspeed0);
            dialogGrid.Children.Remove(avgspeed100);

            dialogGrid.Children.Remove(Model);
            dialogGrid.Children.Remove(Type);
            dialogGrid.Children.Remove(Weight);
            dialogGrid.Children.Remove(MaxTrailerWeight);
            dialogGrid.Children.Remove(AvgSpeed0);
            dialogGrid.Children.Remove(AvgSpeed100);

            model.Text = null;
            type.SelectedIndex = 0;
            weight.Text = null;
            maxTrailerWeight.Text = null;
            avgspeed0.Text = null;
            avgspeed100.Text = null;
        }

        // Verified
        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            // Проверки TextBox на null и пустую строку
            if (model.Text == null || model.Text == "")
            {
                MessageBox.Show("Укажите модель!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (type.Text == null || type.Text == "")
            {
                MessageBox.Show("Укажите тип!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double result;
            // Проверки TextBox на null и пустую строку
            if (weight.Text == null || weight.Text == "")
            {
                MessageBox.Show("Укажите вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!double.TryParse(weight.Text, out result))
            {
                MessageBox.Show("Некорректная вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (maxTrailerWeight.Text == null || maxTrailerWeight.Text == "")
            {
                MessageBox.Show("Укажите максимально допустимую массу прицепа!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!double.TryParse(weight.Text, out result))
            {
                MessageBox.Show("Некорректная максимально допустимая масса прицепа!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (avgspeed0.Text == null || avgspeed0.Text == "")
            {
                MessageBox.Show("Укажите среднюю скорость при нулевом грузе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!double.TryParse(avgspeed0.Text, out result))
            {
                MessageBox.Show("Некорректная средняя скорость!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (avgspeed100.Text == null || avgspeed100.Text == "")
            {
                MessageBox.Show("Укажите среднюю скорость при максимальном грузе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!double.TryParse(avgspeed100.Text, out result))
            {
                MessageBox.Show("Некорректная средняя скорость!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var unique = (from o in dbContext.LocomotivesMs
                          where o._model == model.Text
                          select o).FirstOrDefault();

            if (unique != null && (isAdd || unique._locomotive_id != SourceList[index]._locomotive_id))
            {
                MessageBox.Show("Данный локомотив уже присутствует в таблице!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LocomotivesM temp = new LocomotivesM();
            temp._model = model.Text;
            temp._type = type.SelectedIndex;
            temp._weight = double.Parse(weight.Text);
            temp._max_trailer_weight = double.Parse(maxTrailerWeight.Text);
            temp._avgspeed0 = double.Parse(avgspeed0.Text);
            temp._avgspeed100 = double.Parse(avgspeed100.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.LocomotivesMs.Local.Add(temp);

                // Очищаем поля
                model.Text = null;
                type.SelectedIndex = 0;
                weight.Text = null;
                maxTrailerWeight.Text = null;
                avgspeed0.Text = null;
                avgspeed100.Text = null;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._locomotive_id;
                var loco = dbContext.LocomotivesMs.Local
                    .Single(o => o._locomotive_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                loco._model = temp._model;
                loco._type = temp._type;
                loco._weight = temp._weight;
                loco._max_trailer_weight = temp._max_trailer_weight;
                loco._avgspeed0 = temp._avgspeed0;
                loco._avgspeed100 = temp._avgspeed100;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(loco).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.LocomotivesMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }

        // verified
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

                LocomotivesM deleteEntity = SourceList[index];

                var trains = (from o in dbContext.TrainsMs
                              where o._locomotive_id == deleteEntity._locomotive_id
                              select o);

                if (trains != null)
                {
                    foreach (var train in trains)
                    {
                        var schedules = (from o in dbContext.SchedulesMs
                                         where o._train_id == train._train_id
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
                    }

                    dbContext.TrainsMs.RemoveRange(trains);
                }

                dbContext.LocomotivesMs.Local.Remove(deleteEntity);

                // Удаляем НЕконтекстного юзера из SourceList
                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }
    }
}
