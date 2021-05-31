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
    public class TrainsVM : ObjectModel
    {
        public BindingList<TrainsM> SourceList { get; set; }
        public BindingList<LocomotivesM> locos { get; set; }
        public BindingList<CarriagesM> carriages{ get; set; }
        private AppDBContext dbContext;

        private TextBlock LocoModel, CarrModel_1, CarrModel_2, CarrModel_3, CarrModel_4, CarrModel_5, Speed, Seats;
        private ComboBox locoModel, carrModel_1, carrModel_2, carrModel_3, carrModel_4, carrModel_5;
        private TextBox speed, seats;
		private bool isAdd;
		private int index;

        // Verified
		public TrainsVM()
        {
            // Инициализация контекста БД
            dbContext = AppDBContext.GetInstance();

            dbContext.LocomotivesMs.Load();
            locos = dbContext.LocomotivesMs.Local.ToBindingList();

            dbContext.CarriagesMs.Load();
            carriages = dbContext.CarriagesMs.Local.ToBindingList();

            dbContext.TrainsMs.Load();

            // Margin
            Thickness temp = new Thickness(5);

            LocoModel = new TextBlock();
            LocoModel.SetResourceReference(TextBlock.TextProperty, "Text_LocoModel");
            Grid.SetRow(LocoModel, 0);
            Grid.SetColumn(LocoModel, 0);

            CarrModel_1 = new TextBlock();
            CarrModel_1.Text = App.Current.Resources["Text_CarriageModel"] + " #1";
            Grid.SetRow(CarrModel_1, 1);
            Grid.SetColumn(CarrModel_1, 0);

            CarrModel_2 = new TextBlock();
            CarrModel_2.Text = App.Current.Resources["Text_CarriageModel"] + " #2";
            Grid.SetRow(CarrModel_2, 2);
            Grid.SetColumn(CarrModel_2, 0);

            CarrModel_3 = new TextBlock();
            CarrModel_3.Text = App.Current.Resources["Text_CarriageModel"] + " #3";
            Grid.SetRow(CarrModel_3, 3);
            Grid.SetColumn(CarrModel_3, 0);

            CarrModel_4 = new TextBlock();
            CarrModel_4.Text = App.Current.Resources["Text_CarriageModel"] + " #4";
            Grid.SetRow(CarrModel_4, 4);
            Grid.SetColumn(CarrModel_4, 0);

            CarrModel_5 = new TextBlock();
            CarrModel_5.Text = App.Current.Resources["Text_CarriageModel"] + " #5";
            Grid.SetRow(CarrModel_5, 5);
            Grid.SetColumn(CarrModel_5, 0);

            Speed = new TextBlock();
            Speed.SetResourceReference(TextBlock.TextProperty, "Text_AvgSpeed");
            Grid.SetRow(Speed, 6);
            Grid.SetColumn(Speed, 0);

            Seats = new TextBlock();
            Seats.SetResourceReference(TextBlock.TextProperty, "Text_MaxSeats");
            Grid.SetRow(Seats, 7);
            Grid.SetColumn(Seats, 0);

            locoModel = new ComboBox();
            locoModel.Margin = temp;
            Grid.SetRow(locoModel, 0);
            Grid.SetColumn(locoModel, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < locos.Count; i++)
            {
                locoModel.Items.Insert(i, locos[i]._model);
			}

            carrModel_1 = new ComboBox();
            carrModel_1.Margin = temp;
            Grid.SetRow(carrModel_1, 1);
            Grid.SetColumn(carrModel_1, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_1.Items.Insert(i, carriages[i]._model);
            }

            carrModel_2 = new ComboBox();
            carrModel_2.Margin = temp;
            Grid.SetRow(carrModel_2, 2);
            Grid.SetColumn(carrModel_2, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_2.Items.Insert(i, carriages[i]._model);
            }

            carrModel_3 = new ComboBox();
            carrModel_3.Margin = temp;
            Grid.SetRow(carrModel_3, 3);
            Grid.SetColumn(carrModel_3, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_3.Items.Insert(i, carriages[i]._model);
            }

            carrModel_4 = new ComboBox();
            carrModel_4.Margin = temp;
            Grid.SetRow(carrModel_4, 4);
            Grid.SetColumn(carrModel_4, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_4.Items.Insert(i, carriages[i]._model);
            }

            carrModel_5 = new ComboBox();
            carrModel_5.Margin = temp;
            Grid.SetRow(carrModel_5, 5);
            Grid.SetColumn(carrModel_5, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_5.Items.Insert(i, carriages[i]._model);
            }

            speed = new TextBox();
            speed.Margin = temp;
            Grid.SetRow(speed, 6);
            Grid.SetColumn(speed, 1);

            seats = new TextBox();
            seats.Margin = temp;
            Grid.SetRow(seats, 7);
            Grid.SetColumn(seats, 1);

            SourceList = dbContext.TrainsMs.Local.ToBindingList();
        }

        // Verified
        public void ConnectAndUpdate()
        {
            dbContext = AppDBContext.GetInstance();

            dbContext.LocomotivesMs.Load();
            locos = dbContext.LocomotivesMs.Local.ToBindingList();

            dbContext.CarriagesMs.Load();
            carriages = dbContext.CarriagesMs.Local.ToBindingList();

            dbContext.TrainsMs.Load();

            SourceList = dbContext.TrainsMs.Local.ToBindingList();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();

            CarrModel_1.Text = App.Current.Resources["Text_CarriageModel"] + " #1";
            CarrModel_2.Text = App.Current.Resources["Text_CarriageModel"] + " #2";
            CarrModel_3.Text = App.Current.Resources["Text_CarriageModel"] + " #3";
            CarrModel_4.Text = App.Current.Resources["Text_CarriageModel"] + " #4";
            CarrModel_5.Text = App.Current.Resources["Text_CarriageModel"] + " #5";

            locoModel.Items.Clear();

            for (int i = 0; i < locos.Count; i++)
            {
                locoModel.Items.Insert(i, locos[i]._model);
            }

            carrModel_1.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_1.Items.Insert(i, carriages[i]._model);
            }

            carrModel_2.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_2.Items.Insert(i, carriages[i]._model);
            }

            carrModel_3.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_3.Items.Insert(i, carriages[i]._model);
            }

            carrModel_4.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_4.Items.Insert(i, carriages[i]._model);
            }

            carrModel_5.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel_5.Items.Insert(i, carriages[i]._model);
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
                locoModel.SelectedIndex = 0;

                carrModel_1.SelectedIndex = 0;
                carrModel_2.SelectedIndex = 0;
                carrModel_3.SelectedIndex = 0;
                carrModel_4.SelectedIndex = 0;
                carrModel_5.SelectedIndex = 0;

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
				TrainsM temp = SourceList[index];

                locoModel.SelectedIndex = 0;

                carrModel_1.SelectedIndex = 0;
                carrModel_2.SelectedIndex = 0;
                carrModel_3.SelectedIndex = 0;
                carrModel_4.SelectedIndex = 0;
                carrModel_5.SelectedIndex = 0;

                speed.Text = temp._train_avgspeed.ToString();
                seats.Text = temp._reserved_seats.ToString();

                button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(LocoModel);
            dialogGrid.Children.Add(CarrModel_1);
            dialogGrid.Children.Add(CarrModel_2);
            dialogGrid.Children.Add(CarrModel_3);
            dialogGrid.Children.Add(CarrModel_4);
            dialogGrid.Children.Add(CarrModel_5);
            dialogGrid.Children.Add(Speed);
            dialogGrid.Children.Add(Seats);

            dialogGrid.Children.Add(locoModel);
            dialogGrid.Children.Add(carrModel_1);
            dialogGrid.Children.Add(carrModel_2);
            dialogGrid.Children.Add(carrModel_3);
            dialogGrid.Children.Add(carrModel_4);
            dialogGrid.Children.Add(carrModel_5);
            dialogGrid.Children.Add(speed);
            dialogGrid.Children.Add(seats);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(LocoModel);
            dialogGrid.Children.Remove(CarrModel_1);
            dialogGrid.Children.Remove(CarrModel_2);
            dialogGrid.Children.Remove(CarrModel_3);
            dialogGrid.Children.Remove(CarrModel_4);
            dialogGrid.Children.Remove(CarrModel_5);
            dialogGrid.Children.Remove(Speed);
            dialogGrid.Children.Remove(Seats);

            dialogGrid.Children.Remove(locoModel);
            dialogGrid.Children.Remove(carrModel_1);
            dialogGrid.Children.Remove(carrModel_2);
            dialogGrid.Children.Remove(carrModel_3);
            dialogGrid.Children.Remove(carrModel_4);
            dialogGrid.Children.Remove(carrModel_5);
            dialogGrid.Children.Remove(speed);
            dialogGrid.Children.Remove(seats);

            locoModel.SelectedIndex = 0;
            carrModel_1.SelectedIndex = 0;
            carrModel_2.SelectedIndex = 0;
            carrModel_3.SelectedIndex = 0;
            carrModel_4.SelectedIndex = 0;
            carrModel_5.SelectedIndex = 0;
            speed.Text = null;
            seats.Text = null;
        }

        // Verified
        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (locoModel.Items.Count < 1)
            {
                MessageBox.Show("Локомотивы в базе данных отсутствуют! Сначала добавьте ХОТЯ БЫ ОДИН локомотив!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            double result1;
            // Проверки TextBox на null и пустую строку
            if (speed.Text == null || speed.Text == "")
            {
                MessageBox.Show("Укажите скорость!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!double.TryParse(speed.Text, out result1))
            {
                MessageBox.Show("Некорректная скорость!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int result2;
            // Проверки TextBox на null и пустую строку
            if (seats.Text == null || seats.Text == "")
            {
                seats.Text = "0";
            }
            else if (!int.TryParse(seats.Text, out result2))
            {
                MessageBox.Show("Некорректное количество мест!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Вот эту парашу надо делать через запросы походу
            TrainsM temp = new TrainsM();

            var locomotive = (from o in dbContext.LocomotivesMs
                              where o._model == locoModel.Text
                              select o).FirstOrDefault();

            temp._locomotive_id = locomotive._locomotive_id;

            // carriage 1
            var carriage_1 = (from o in dbContext.CarriagesMs
                              where o._model == carrModel_1.Text
                              select o).FirstOrDefault();

            if (carriage_1 != null)
                temp._first_carriage_id = carriage_1._carriage_id;
            else
                temp._first_carriage_id = -1;

            // carriage 2
            var carriage_2 = (from o in dbContext.CarriagesMs
                              where o._model == carrModel_2.Text
                              select o).FirstOrDefault();

            if (carriage_2 != null)
                temp._second_carriage_id = carriage_2._carriage_id;
            else
                temp._second_carriage_id = -1;

            // carriage 3
            var carriage_3 = (from o in dbContext.CarriagesMs
                              where o._model == carrModel_3.Text
                              select o).FirstOrDefault();

            if (carriage_3 != null)
                temp._third_carriage_id = carriage_3._carriage_id;
            else
                temp._third_carriage_id = -1;

            // carriage 4
            var carriage_4 = (from o in dbContext.CarriagesMs
                              where o._model == carrModel_4.Text
                              select o).FirstOrDefault();

            if (carriage_4 != null)
                temp._fourth_carriage_id = carriage_4._carriage_id;
            else
                temp._fourth_carriage_id = -1;

            // carriage 5
            var carriage_5 = (from o in dbContext.CarriagesMs
                              where o._model == carrModel_5.Text
                              select o).FirstOrDefault();

            if (carriage_5 != null)
                temp._fifth_carriage_id = carriage_5._carriage_id;
            else
                temp._fifth_carriage_id = -1;

            temp._train_avgspeed = double.Parse(speed.Text);
            temp._reserved_seats = int.Parse(seats.Text);

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.TrainsMs.Local.Add(temp);

                // Очищаем поля
                locoModel.SelectedIndex = 0;
                carrModel_1.SelectedIndex = 0;
                carrModel_2.SelectedIndex = 0;
                carrModel_3.SelectedIndex = 0;
                carrModel_4.SelectedIndex = 0;
                carrModel_5.SelectedIndex = 0;
                seats.Text = null;
                speed.Text = null;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._train_id;
                var train = dbContext.TrainsMs.Local
                    .Single(o => o._train_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                train._locomotive_id = temp._locomotive_id;

                train._first_carriage_id = temp._first_carriage_id;
                train._second_carriage_id = temp._second_carriage_id;
                train._third_carriage_id = temp._third_carriage_id;
                train._fourth_carriage_id = temp._fourth_carriage_id;
                train._fifth_carriage_id = temp._fifth_carriage_id;

                train._reserved_seats = temp._reserved_seats;
                train._train_avgspeed = temp._train_avgspeed;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(train).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.TrainsMs.Local.ToBindingList();

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

                TrainsM deleteEntity = SourceList[index];

                var schedules = (from o in dbContext.SchedulesMs
                                 where o._train_id == deleteEntity._train_id
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

                dbContext.TrainsMs.Local.Remove(deleteEntity);
                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();

            TableV.Current_DataGrid.ItemsSource = SourceList;
            TableV.Current_DataGrid.Items.Refresh();
        }
    }
}
