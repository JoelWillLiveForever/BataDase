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
    public class LocomotivesVM : ObjectModel
    {
        private bool isAdd;
        private int index;

        public BindingList<LocomotivesM> SourceList { get; set; }
        private AppDBContext dbContext;

        private TextBlock Model, Type, Weight, PMW, AvgSpeed0, AvgSpeed100;
        private TextBox model, weight, pmw, avgspeed0, avgspeed100;
        private ComboBox type;

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
			Model.Text = App.Current.Resources["Text_Model"] + ":";
			Grid.SetRow(Model, 0);
			Grid.SetColumn(Model, 0);

			// Лэйбл "Тип", назначение текста, строки и колонки в Grid
			Type = new TextBlock();
			Type.Text = App.Current.Resources["Text_Type"] + ":";
			Grid.SetRow(Type, 1);
			Grid.SetColumn(Type, 0);

			// Лэйбл "Вес", назначение текста, строки и колонки в Grid
			Weight = new TextBlock();
			Weight.Text = App.Current.Resources["Text_Weight"] + ":";
			Grid.SetRow(Weight, 2);
			Grid.SetColumn(Weight, 0);

			// Лэйбл "Разрешённая ММ", назначение текста, строки и колонки в Grid
			PMW = new TextBlock();
			PMW.Text = App.Current.Resources["Text_PMW"] + ":";
			Grid.SetRow(PMW, 3);
			Grid.SetColumn(PMW, 0);

			// Лэйбл "Средняя скорость 0", назначение текста, строки и колонки в Grid
			AvgSpeed0 = new TextBlock();
			AvgSpeed0.Text = App.Current.Resources["Text_AvgSpeed0"] + ":";
			Grid.SetRow(AvgSpeed0, 4);
			Grid.SetColumn(AvgSpeed0, 0);

			// Лэйбл "Средняя скорость 100", назначение текста, строки и колонки в Grid
			AvgSpeed100 = new TextBlock();
			AvgSpeed100.Text = App.Current.Resources["Text_AvgSpeed100"] + ":";
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
			pmw = new TextBox();
			pmw.Margin = temp;
			Grid.SetRow(pmw, 3);
			Grid.SetColumn(pmw, 1);

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
            type.Items.Insert(0, electroLoco);
			type.Items.Insert(1, steamLoco);
            type.Items.Insert(2, heatLoco);
            type.SelectedIndex = 0;

			// Инициализация списка элементов БД
			SourceList = dbContext.LocomotivesMs.Local.ToBindingList();
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
            if (dbContext != null) return;
            dbContext = AppDBContext.GetInstance();
            dbContext.LocomotivesMs.Load();
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
                if (MenuV.Current_DataGrid.SelectedItems.Count < 1)
                {
                    MessageBox.Show("Выберите элемент для изменения!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (MenuV.Current_DataGrid.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Можно выбрать для изменения не более ОДНОГО элемента за раз!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // индекс текущей выбранной строки в DataGrid
                // кастыль, но куда без кастылей?
                index = MenuV.Current_DataGrid.SelectedIndex;

                // Если всё ок, вставляем данные для данного юзера в форму, который затем будем менять
                LocomotivesM temp = SourceList[index];

                model.Text = temp._model;
                type.SelectedItem = temp._type;
                weight.Text = temp._weight.ToString();
                pmw.Text = temp._pmw.ToString();
                avgspeed0.Text = temp._avgspeed0.ToString();
                avgspeed100.Text = temp._avgspeed100.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(Model);
            dialogGrid.Children.Add(Type);
            dialogGrid.Children.Add(Weight);
            dialogGrid.Children.Add(PMW);
            dialogGrid.Children.Add(AvgSpeed0);
            dialogGrid.Children.Add(AvgSpeed100);

            dialogGrid.Children.Add(model);
            dialogGrid.Children.Add(type);
            dialogGrid.Children.Add(weight);
            dialogGrid.Children.Add(pmw);
            dialogGrid.Children.Add(avgspeed0);
            dialogGrid.Children.Add(avgspeed100);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия
            
            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(model);
            dialogGrid.Children.Remove(type);
            dialogGrid.Children.Remove(weight);
            dialogGrid.Children.Remove(pmw);
            dialogGrid.Children.Remove(avgspeed0);
            dialogGrid.Children.Remove(avgspeed100);

            dialogGrid.Children.Remove(Model);
            dialogGrid.Children.Remove(Type);
            dialogGrid.Children.Remove(Weight);
            dialogGrid.Children.Remove(PMW);
            dialogGrid.Children.Remove(AvgSpeed0);
            dialogGrid.Children.Remove(AvgSpeed100);

            model.Text = null;
            type.SelectedIndex = 0;
            weight.Text = null;
            pmw.Text = null;
            avgspeed0.Text = null;
            avgspeed100.Text = null;
        }

        // Метод, срабатывающий при нажатии на кнопку в окне добавления юзера
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

            float result;
            // Проверки TextBox на null и пустую строку
            if (weight.Text == null || weight.Text == "")
            {
                MessageBox.Show("Укажите вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(weight.Text, out result))
            {
                MessageBox.Show("Некорректная вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pmw.Text == null || pmw.Text == "")
            {
                MessageBox.Show("Укажите максимально разрешённую массу!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(weight.Text, out result))
            {
                MessageBox.Show("Некорректная максимально разрешённая масса!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (avgspeed0.Text == null || avgspeed0.Text == "")
            {
                MessageBox.Show("Укажите среднюю скорость при нулевом грузе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(avgspeed0.Text, out result))
            {
                MessageBox.Show("Некорректная средняя скорость!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (avgspeed100.Text == null || avgspeed100.Text == "")
            {
                MessageBox.Show("Укажите среднюю скорость при максимальном грузе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(avgspeed100.Text, out result))
            {
                MessageBox.Show("Некорректная средняя скорость!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LocomotivesM temp = new LocomotivesM();
            temp._model = model.Text;
            temp._type = type.SelectedItem.ToString();
            temp._weight = float.Parse(weight.Text);
            temp._pmw = float.Parse(pmw.Text);
            temp._avgspeed0 = float.Parse(avgspeed0.Text);
            temp._avgspeed100 = float.Parse(avgspeed100.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.LocomotivesMs.Local.Add(temp);

                // Очищаем поля
                model.Text = null;
                type.SelectedIndex = 0;
                weight.Text = null;
                pmw.Text = null;
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
                loco._pmw = temp._pmw;
                loco._avgspeed0 = temp._avgspeed0;
                loco._avgspeed100 = temp._avgspeed100;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(loco).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.LocomotivesMs.Local.ToBindingList();

            //MenuV.MenuV_DataGrid.Items.Refresh();
        }

        public void Delete()
        {
            if (MenuV.Current_DataGrid.SelectedItems.Count < 1)
            {
                MessageBox.Show("Выберите элементы для удаления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            while (MenuV.Current_DataGrid.SelectedItems.Count > 0)
            {
                index = MenuV.Current_DataGrid.SelectedIndex;

                LocomotivesM deleteEntity = SourceList[index];

                var tickets = dbContext.TicketsMs
                    .Where(o => o._user_id == deleteEntity._locomotive_id);

                if (tickets != null)
                    dbContext.TicketsMs.RemoveRange(tickets);

                var contextDeleteEntity = dbContext.LocomotivesMs.Local
                    .Single(o => o._locomotive_id == deleteEntity._locomotive_id);

                dbContext.LocomotivesMs.Local.Remove(contextDeleteEntity);

                // Удаляем НЕконтекстного юзера из SourceList
                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
