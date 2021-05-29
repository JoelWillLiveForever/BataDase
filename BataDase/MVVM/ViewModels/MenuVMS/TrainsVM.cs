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

        private TextBlock LocoModel, CarrModel0, CarrModel1, CarrModel2, CarrModel3, CarrModel4;
        private ComboBox locoModel, carrModel0, carrModel1, carrModel2, carrModel3, carrModel4;
		private bool isAdd;
		private int index;

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
            LocoModel.Text = App.Current.Resources["Text_LocoModel"] + ":";
            Grid.SetRow(LocoModel, 0);
            Grid.SetColumn(LocoModel, 0);

            CarrModel0 = new TextBlock();
            CarrModel0.Text = App.Current.Resources["Text_CarriageModel"] + " #1:";
            Grid.SetRow(CarrModel0, 1);
            Grid.SetColumn(CarrModel0, 0);

            CarrModel1 = new TextBlock();
            CarrModel1.Text = App.Current.Resources["Text_CarriageModel"] + " #2:";
            Grid.SetRow(CarrModel1, 2);
            Grid.SetColumn(CarrModel1, 0);

            CarrModel2 = new TextBlock();
            CarrModel2.Text = App.Current.Resources["Text_CarriageModel"] + " #3:";
            Grid.SetRow(CarrModel2, 3);
            Grid.SetColumn(CarrModel2, 0);

            CarrModel3 = new TextBlock();
            CarrModel3.Text = App.Current.Resources["Text_CarriageModel"] + " #4:";
            Grid.SetRow(CarrModel3, 4);
            Grid.SetColumn(CarrModel3, 0);

            CarrModel4 = new TextBlock();
            CarrModel4.Text = App.Current.Resources["Text_CarriageModel"] + " #5:";
            Grid.SetRow(CarrModel4, 5);
            Grid.SetColumn(CarrModel4, 0);

            locoModel = new ComboBox();
            locoModel.Margin = temp;
            Grid.SetRow(locoModel, 0);
            Grid.SetColumn(locoModel, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < locos.Count; i++)
            {
                locoModel.Items.Insert(i, locos[i]._model);
			}

            carrModel0 = new ComboBox();
            carrModel0.Margin = temp;
            Grid.SetRow(carrModel0, 1);
            Grid.SetColumn(carrModel0, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel0.Items.Insert(i, carriages[i]._model);
            }

            carrModel1 = new ComboBox();
            carrModel1.Margin = temp;
            Grid.SetRow(carrModel1, 2);
            Grid.SetColumn(carrModel1, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel1.Items.Insert(i, carriages[i]._model);
            }

            carrModel2 = new ComboBox();
            carrModel2.Margin = temp;
            Grid.SetRow(carrModel2, 3);
            Grid.SetColumn(carrModel2, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel2.Items.Insert(i, carriages[i]._model);
            }

            carrModel3 = new ComboBox();
            carrModel3.Margin = temp;
            Grid.SetRow(carrModel3, 4);
            Grid.SetColumn(carrModel3, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel3.Items.Insert(i, carriages[i]._model);
            }

            carrModel4 = new ComboBox();
            carrModel4.Margin = temp;
            Grid.SetRow(carrModel4, 5);
            Grid.SetColumn(carrModel4, 1);

            // Назначение элементов комбобокс
            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel4.Items.Insert(i, carriages[i]._model);
            }

            SourceList = dbContext.TrainsMs.Local.ToBindingList();
        }

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

            locoModel.Items.Clear();

            for (int i = 0; i < locos.Count; i++)
            {
                locoModel.Items.Insert(i, locos[i]._model);
            }

            carrModel0.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel0.Items.Insert(i, carriages[i]._model);
            }

            carrModel1.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel1.Items.Insert(i, carriages[i]._model);
            }

            carrModel2.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel2.Items.Insert(i, carriages[i]._model);
            }

            carrModel3.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel3.Items.Insert(i, carriages[i]._model);
            }

            carrModel4.Items.Clear();

            for (int i = 0; i < carriages.Count; i++)
            {
                carrModel4.Items.Insert(i, carriages[i]._model);
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
				TrainsM temp = SourceList[index];

				locoModel.SelectedItem = temp.LocomotivesM._model;
				carrModel0.SelectedItem = temp.CarriagesM_First._model;
                carrModel1.SelectedItem = temp.CarriagesM_Second._model;
				carrModel2.SelectedItem = temp.CarriagesM_Third._model;
                carrModel3.SelectedItem = temp.CarriagesM_Fourth._model;
                carrModel4.SelectedItem = temp.CarriagesM_Fifth._model;

				button.Content = App.Current.Resources["Text_Edit"];
            }

            button.Click += new RoutedEventHandler(ExecuteAddEdit);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(LocoModel);
            dialogGrid.Children.Add(CarrModel0);
            dialogGrid.Children.Add(CarrModel1);
            dialogGrid.Children.Add(CarrModel2);
            dialogGrid.Children.Add(CarrModel3);
            dialogGrid.Children.Add(CarrModel4);

            dialogGrid.Children.Add(locoModel);
            dialogGrid.Children.Add(carrModel0);
            dialogGrid.Children.Add(carrModel1);
            dialogGrid.Children.Add(carrModel2);
            dialogGrid.Children.Add(carrModel3);
            dialogGrid.Children.Add(carrModel4);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(LocoModel);
            dialogGrid.Children.Remove(CarrModel0);
            dialogGrid.Children.Remove(CarrModel1);
            dialogGrid.Children.Remove(CarrModel2);
            dialogGrid.Children.Remove(CarrModel3);
            dialogGrid.Children.Remove(CarrModel4);

            dialogGrid.Children.Remove(locoModel);
            dialogGrid.Children.Remove(carrModel0);
            dialogGrid.Children.Remove(carrModel1);
            dialogGrid.Children.Remove(carrModel2);
            dialogGrid.Children.Remove(carrModel3);
            dialogGrid.Children.Remove(carrModel4);

            locoModel.SelectedIndex = 0;
            carrModel0.SelectedIndex = 0;
            carrModel1.SelectedIndex = 0;
            carrModel2.SelectedIndex = 0;
            carrModel3.SelectedIndex = 0;
            carrModel4.SelectedIndex = 0;
        }

        public void ExecuteAddEdit(object sender, RoutedEventArgs e)
        {
            if (locoModel.Text == null)
            {
                MessageBox.Show("Укажите модель локомотива!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (carrModel0.Text == null)
            {
                MessageBox.Show("Укажите модель вагона №1!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (carrModel1.Text == null)
            {
                MessageBox.Show("Укажите модель вагона №2!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (carrModel2.Text == null)
            {
                MessageBox.Show("Укажите модель вагона №3!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (carrModel3.Text == null)
            {
                MessageBox.Show("Укажите модель вагона №4!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (carrModel4.Text == null)
            {
                MessageBox.Show("Укажите модель вагона №5!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TrainsM temp = new TrainsM();
            temp.LocomotivesM._model = locoModel.Text;
            temp.CarriagesM_First._model = carrModel0.Text;
            temp.CarriagesM_Second._model = carrModel1.Text;
            temp.CarriagesM_Third._model = carrModel2.Text;
            temp.CarriagesM_Fourth._model = carrModel3.Text;
            temp.CarriagesM_Fifth._model = carrModel4.Text;

            // Сохранение нового юзера в БД
            if (isAdd)
            {
                // Добавляем объект в БД
                dbContext.TrainsMs.Local.Add(temp);

                // Очищаем поля
                locoModel.SelectedIndex = 0;
                carrModel0.SelectedIndex = 0;
                carrModel1.SelectedIndex = 0;
                carrModel2.SelectedIndex = 0;
                carrModel3.SelectedIndex = 0;
                carrModel4.SelectedIndex = 0;
            }
            else
            {
                // Получаем объект из БД по айди, который будем изменять
                int id = SourceList[index]._train_id;
                var train = dbContext.TrainsMs.Local
                    .Single(o => o._train_id == id);

                // Изменяем, просто изменяя поля на поля объекта temp
                train.LocomotivesM._model = temp.LocomotivesM._model;
                train.CarriagesM_First._model = temp.CarriagesM_First._model;
                train.CarriagesM_Second._model = temp.CarriagesM_Second._model;
                train.CarriagesM_Third._model = temp.CarriagesM_Third._model;
                train.CarriagesM_Fourth._model = temp.CarriagesM_Fourth._model;
                train.CarriagesM_Fifth._model = temp.CarriagesM_Fifth._model;

                // Говорим контексту БД, что данный объект был изменен
                dbContext.Entry(train).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            // Обновление списка
            SourceList = dbContext.TrainsMs.Local.ToBindingList();
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

                TrainsM deleteEntity = SourceList[index];

                // Удаляем поезд из расписания
                var schedules = dbContext.SchedulesMs
                    .Where(o => o._train_id == deleteEntity._train_id);

                if (schedules != null)
                    dbContext.SchedulesMs.RemoveRange(schedules);

                var contextDeleteEntity = dbContext.SchedulesMs.Local
                    .Single(o => o._train_id == deleteEntity._train_id);

                dbContext.SchedulesMs.Local.Remove(contextDeleteEntity);

                SourceList.Remove(deleteEntity);
            }

            // Сохраняем контекст БД
            dbContext.SaveChanges();
        }
    }
}
