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
	public class CarriagesVM : ObjectModel
	{
		private bool isAdd;
		private int index;

		public BindingList<CarriagesM> SourceList { get; set; }
		private AppDBContext dbContext;

		private TextBlock Model, Type, Weight, PMW, CargoWeight, MaxSeats;
		private TextBox model, weight, pmw, cargoWeight, maxSeats;
		private ComboBox type;

		public CarriagesVM()
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
			CargoWeight = new TextBlock();
			CargoWeight.Text = App.Current.Resources["Text_AvgSpeed0"] + ":";
			Grid.SetRow(CargoWeight, 4);
			Grid.SetColumn(CargoWeight, 0);

			// Лэйбл "Средняя скорость 100", назначение текста, строки и колонки в Grid
			MaxSeats = new TextBlock();
			MaxSeats.Text = App.Current.Resources["Text_AvgSpeed100"] + ":";
			MaxSeats.TextWrapping = 0;
			Grid.SetRow(MaxSeats, 5);
			Grid.SetColumn(MaxSeats, 0);

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
			cargoWeight = new TextBox();
			cargoWeight.Margin = temp;
			Grid.SetRow(cargoWeight, 4);
			Grid.SetColumn(cargoWeight, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			maxSeats = new TextBox();
			maxSeats.Margin = temp;
			Grid.SetRow(maxSeats, 5);
			Grid.SetColumn(maxSeats, 1);

			// Комбобокс "Тип", назначение отсутпа (Margin), строки и колонки в Grid
			type = new ComboBox();
			type.Margin = temp;
			Grid.SetRow(type, 1);
			Grid.SetColumn(type, 1);

			// Вытягивание полов из ресурсов
			string cargoCarriage = (string)App.Current.Resources["Text_CargoCarriage"];
			string passCarriage = (string)App.Current.Resources["Text_PassengerCarriage"];
			string restCarriage = (string)App.Current.Resources["Text_RestauranCarriage"];

			// Назначение элементов комбобокс
			type.Items.Insert(0, passCarriage);
			type.Items.Insert(1, cargoCarriage);
			type.Items.Insert(2, restCarriage);
			type.SelectedIndex = 0;

			// Инициализация списка элементов БД
			SourceList = dbContext.CarriagesMs.Local.ToBindingList();
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
			dbContext.CarriagesMs.Load();
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
				CarriagesM temp = SourceList[index];

				model.Text = temp._model;
				type.SelectedItem = temp._type;
				weight.Text = temp._weight.ToString();
				pmw.Text = temp._pmw.ToString();
				cargoWeight.Text = temp._cargo_weight.ToString();
				maxSeats.Text = temp._max_seats.ToString();

				button.Content = App.Current.Resources["Text_Edit"];
			}

			button.Click += new RoutedEventHandler(ExecuteAddEdit);

			// Вешаем элементы в Grid
			dialogGrid.Children.Add(Model);
			dialogGrid.Children.Add(Type);
			dialogGrid.Children.Add(Weight);
			dialogGrid.Children.Add(PMW);
			dialogGrid.Children.Add(CargoWeight);
			dialogGrid.Children.Add(MaxSeats);

			dialogGrid.Children.Add(model);
			dialogGrid.Children.Add(type);
			dialogGrid.Children.Add(weight);
			dialogGrid.Children.Add(pmw);
			dialogGrid.Children.Add(cargoWeight);
			dialogGrid.Children.Add(maxSeats);

			// Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

			dialogV.ShowDialog();

			// Очищаем Grid
			dialogGrid.Children.Remove(model);
			dialogGrid.Children.Remove(type);
			dialogGrid.Children.Remove(weight);
			dialogGrid.Children.Remove(pmw);
			dialogGrid.Children.Remove(cargoWeight);
			dialogGrid.Children.Remove(maxSeats);

			dialogGrid.Children.Remove(Model);
			dialogGrid.Children.Remove(Type);
			dialogGrid.Children.Remove(Weight);
			dialogGrid.Children.Remove(PMW);
			dialogGrid.Children.Remove(CargoWeight);
			dialogGrid.Children.Remove(MaxSeats);

			model.Text = null;
			type.SelectedIndex = 0;
			weight.Text = null;
			pmw.Text = null;
			cargoWeight.Text = null;
			maxSeats.Text = null;
		}

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

			float resultFloat;
			int resultInt;
			// Проверки TextBox на null и пустую строку
			if (weight.Text == null || weight.Text == "")
			{
				MessageBox.Show("Укажите вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (!float.TryParse(weight.Text, out resultFloat))
			{
				MessageBox.Show("Некорректная вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (pmw.Text == null || pmw.Text == "")
			{
				MessageBox.Show("Укажите максимально разрешённую массу!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (!float.TryParse(weight.Text, out resultFloat))
			{
				MessageBox.Show("Некорректная максимально разрешённая масса!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (cargoWeight.Text == null || cargoWeight.Text == "")
			{
				MessageBox.Show("Укажите массу груза!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (!float.TryParse(cargoWeight.Text, out resultFloat))
			{
				MessageBox.Show("Некорректная масса груза!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (maxSeats.Text == null || maxSeats.Text == "")
			{
				MessageBox.Show("Укажите количество мест!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (!int.TryParse(maxSeats.Text, out resultInt))
			{
				MessageBox.Show("Некорректное количество мест!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			CarriagesM temp = new CarriagesM();
			temp._model = model.Text;
			temp._type = type.SelectedItem.ToString();
			temp._weight = float.Parse(weight.Text);
			temp._pmw = float.Parse(pmw.Text);
			temp._cargo_weight = float.Parse(cargoWeight.Text);
			temp._max_seats = int.Parse(maxSeats.Text);

			// Сохранение нового юзера в БД
			if (isAdd)
			{
				// Добавляем объект в БД
				dbContext.CarriagesMs.Local.Add(temp);

				// Очищаем поля
				model.Text = null;
				type.SelectedIndex = 0;
				weight.Text = null;
				pmw.Text = null;
				cargoWeight.Text = null;
				maxSeats.Text = null;
			}
			else
			{
				// Получаем объект из БД по айди, который будем изменять
				int id = SourceList[index]._carriage_id;
				var loco = dbContext.CarriagesMs.Local
					.Single(o => o._carriage_id == id);

				// Изменяем, просто изменяя поля на поля объекта temp
				loco._model = temp._model;
				loco._type = temp._type;
				loco._weight = temp._weight;
				loco._pmw = temp._pmw;
				loco._cargo_weight = temp._cargo_weight;
				loco._max_seats = temp._max_seats;

				// Говорим контексту БД, что данный объект был изменен
				dbContext.Entry(loco).State = EntityState.Modified;
			}
			dbContext.SaveChanges();

			// Обновление списка
			SourceList = dbContext.CarriagesMs.Local.ToBindingList();

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

				CarriagesM deleteEntity = SourceList[index];

				var tickets = dbContext.TicketsMs
					.Where(o => o._user_id == deleteEntity._carriage_id);

				if (tickets != null)
					dbContext.TicketsMs.RemoveRange(tickets);

				var contextDeleteEntity = dbContext.CarriagesMs.Local
					.Single(o => o._carriage_id == deleteEntity._carriage_id);

				dbContext.CarriagesMs.Local.Remove(contextDeleteEntity);

				// Удаляем НЕконтекстного юзера из SourceList
				SourceList.Remove(deleteEntity);
			}

			// Сохраняем контекст БД
			dbContext.SaveChanges();
		}
    }
}
