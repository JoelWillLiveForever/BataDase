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

		private TextBlock Model, Type, Weight, MaxLoadWeight, MaxSeats, CarriageClass;
		private TextBox model, weight, maxLoadWeight, maxSeats;
		private ComboBox type, carriageClass;

		// Verified
		public CarriagesVM()
        {
			// Инициализация контекста БД
			dbContext = AppDBContext.GetInstance();
			dbContext.CarriagesMs.Load();

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
			MaxLoadWeight = new TextBlock();
			MaxLoadWeight.SetResourceReference(TextBlock.TextProperty, "Text_MaxLoadWeight");
			Grid.SetRow(MaxLoadWeight, 3);
			Grid.SetColumn(MaxLoadWeight, 0);

			// Лэйбл "Средняя скорость 100", назначение текста, строки и колонки в Grid
			MaxSeats = new TextBlock();
			MaxSeats.SetResourceReference(TextBlock.TextProperty, "Text_MaxSeats");
			MaxSeats.TextWrapping = 0;
			Grid.SetRow(MaxSeats, 4);
			Grid.SetColumn(MaxSeats, 0);

			// Лэйбл "Средняя скорость 0", назначение текста, строки и колонки в Grid
			CarriageClass = new TextBlock();
			CarriageClass.SetResourceReference(TextBlock.TextProperty, "Text_Class");
			Grid.SetRow(CarriageClass, 5);
			Grid.SetColumn(CarriageClass, 0);

			// Текстбокс "Модель", назначение отсутпа (Margin), строки и колонки в Grid
			model = new TextBox();
			model.Margin = temp;
			Grid.SetRow(model, 0);
			Grid.SetColumn(model, 1);

			// Комбобокс "Тип", назначение отсутпа (Margin), строки и колонки в Grid
			type = new ComboBox();
			type.Margin = temp;
			Grid.SetRow(type, 1);
			Grid.SetColumn(type, 1);

			int lastSelectedIndex = -1;
			type.SelectionChanged += new SelectionChangedEventHandler((sender, e) =>
			{
				if (type.SelectedIndex == 0)
				{
					carriageClass.IsEnabled = true;
					maxSeats.IsEnabled = true;
					carriageClass.SelectedIndex = lastSelectedIndex;
					return;
				}
				// Сохранение выбранного состояния
				lastSelectedIndex = carriageClass.SelectedIndex;
				carriageClass.SelectedIndex = -1;

				carriageClass.IsEnabled = false;
				maxSeats.IsEnabled = false;
			});

            // Текстбокс "Вес", назначение отсутпа (Margin), строки и колонки в Grid
            weight = new TextBox();
            weight.Margin = temp;
            Grid.SetRow(weight, 2);
            Grid.SetColumn(weight, 1);

            // Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
            maxLoadWeight = new TextBox();
            maxLoadWeight.Margin = temp;
            Grid.SetRow(maxLoadWeight, 3);
            Grid.SetColumn(maxLoadWeight, 1);

            // Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
            maxSeats = new TextBox();
            maxSeats.Margin = temp;
            Grid.SetRow(maxSeats, 4);
            Grid.SetColumn(maxSeats, 1);

            // Комбобокс "Тип", назначение отсутпа (Margin), строки и колонки в Grid
            carriageClass = new ComboBox();
            carriageClass.Margin = temp;
            Grid.SetRow(carriageClass, 5);
            Grid.SetColumn(carriageClass, 1);

            // Вытягивание полов из ресурсов
            string cargoCarriage = (string)App.Current.Resources["Text_CargoCarriage"];
            string passCarriage = (string)App.Current.Resources["Text_PassengerCarriage"];
            string restCarriage = (string)App.Current.Resources["Text_RestauranCarriage"];

            // Назначение элементов комбобокс
            type.Items.Insert(0, passCarriage);
            type.Items.Insert(1, restCarriage);
            type.Items.Insert(2, cargoCarriage);
            type.SelectedIndex = 0;

            // Вытягивание полов из ресурсов
            string classCompartment = (string)App.Current.Resources["Text_ClassCompartment"];
            string classEconom = (string)App.Current.Resources["Text_ClassEconom"];
            string classSeated = (string)App.Current.Resources["Text_ClassSeated"];

            // Назначение элементов комбобокс
            carriageClass.Items.Insert(0, classCompartment);
            carriageClass.Items.Insert(1, classEconom);
            carriageClass.Items.Insert(2, classSeated);
            carriageClass.SelectedIndex = 0;

            // Инициализация списка элементов БД
            SourceList = dbContext.CarriagesMs.Local.ToBindingList();
		}

		// Verified
		public void ConnectAndUpdate()
		{
			dbContext = AppDBContext.GetInstance();
			dbContext.CarriagesMs.Load();

			SourceList = dbContext.CarriagesMs.Local.ToBindingList();
			TableV.Current_DataGrid.ItemsSource = SourceList;
			TableV.Current_DataGrid.Items.Refresh();

			type.Items.Clear();

			string cargoCarriage = (string)App.Current.Resources["Text_CargoCarriage"];
			string passCarriage = (string)App.Current.Resources["Text_PassengerCarriage"];
			string restCarriage = (string)App.Current.Resources["Text_RestauranCarriage"];

			// Назначение элементов комбобокс
			type.Items.Insert(0, passCarriage);
			type.Items.Insert(1, restCarriage);
			type.Items.Insert(2, cargoCarriage);
			type.SelectedIndex = 0;

			carriageClass.Items.Clear();

			string classCompartment = (string)App.Current.Resources["Text_ClassCompartment"];
			string classEconom = (string)App.Current.Resources["Text_ClassEconom"];
			string classSeated = (string)App.Current.Resources["Text_ClassSeated"];

			// Назначение элементов комбобокс
			carriageClass.Items.Insert(0, classCompartment);
			carriageClass.Items.Insert(1, classEconom);
			carriageClass.Items.Insert(2, classSeated);
			carriageClass.SelectedIndex = 0;
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
				CarriagesM temp = SourceList[index];

				model.Text = temp._model;
				type.SelectedIndex = temp._type;
				weight.Text = temp._weight.ToString();
				maxLoadWeight.Text = temp._max_load_weight.ToString();

				if (temp._max_seats != -1)
					maxSeats.Text = temp._max_seats.ToString();
				if (temp._class != -1)
					carriageClass.SelectedIndex = temp._class;

				button.Content = App.Current.Resources["Text_Edit"];
			}

			button.Click += new RoutedEventHandler(ExecuteAddEdit);

			// Вешаем элементы в Grid
			dialogGrid.Children.Add(Model);
			dialogGrid.Children.Add(Type);
			dialogGrid.Children.Add(Weight);
			dialogGrid.Children.Add(MaxLoadWeight);
			dialogGrid.Children.Add(MaxSeats);
            dialogGrid.Children.Add(CarriageClass);

            dialogGrid.Children.Add(model);
			dialogGrid.Children.Add(type);
            dialogGrid.Children.Add(weight);
            dialogGrid.Children.Add(maxLoadWeight);
            dialogGrid.Children.Add(maxSeats);
            dialogGrid.Children.Add(carriageClass);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

			// Очищаем Grid
			dialogGrid.Children.Remove(model);
			dialogGrid.Children.Remove(type);
			dialogGrid.Children.Remove(weight);
			dialogGrid.Children.Remove(maxLoadWeight);
			dialogGrid.Children.Remove(maxSeats);
			dialogGrid.Children.Remove(carriageClass);

			dialogGrid.Children.Remove(Model);
			dialogGrid.Children.Remove(Type);
			dialogGrid.Children.Remove(Weight);
			dialogGrid.Children.Remove(MaxLoadWeight);
			dialogGrid.Children.Remove(CarriageClass);
			dialogGrid.Children.Remove(MaxSeats);

			model.Text = null;
			type.SelectedIndex = 0;
			weight.Text = null;
			maxLoadWeight.Text = null;
			maxSeats.Text = null;
			carriageClass.SelectedIndex = 0;

			maxSeats.IsEnabled = true;
			carriageClass.IsEnabled = true;
		}

		// Verified
		public void ExecuteAddEdit(object sender, RoutedEventArgs e)
		{
			// Проверки TextBox на null и пустую строку
			if (model.Text == null || model.Text == "")
			{
				MessageBox.Show("Укажите модель!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			} else
            {
				foreach (var item in SourceList)
                {
					if (item._model == model.Text)
                    {
						// Если сейчас добавление, то просто вывести ошибку
						if (isAdd)
						{
							MessageBox.Show("Указанная модель уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
							return;
						}
						// Если сейчас изменение, то в случае совпадения индексов ошибку не выводить
						else
						{
							// Вывести ошибку, если элементы таблицы имеют разные индексы,
							// т.е. такой логин уже есть у кого то другого в таблице
							if (item._carriage_id != SourceList[index]._carriage_id)
							{
								MessageBox.Show("Указанная модель уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
								return;
							}
						}
					}
                }
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

			if (maxLoadWeight.Text == null || maxLoadWeight.Text == "")
			{
				MessageBox.Show("Укажите максимально разрешённую массу!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (!float.TryParse(weight.Text, out resultFloat))
			{
				MessageBox.Show("Некорректная максимально разрешённая масса!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (type.SelectedIndex == 0)
			{
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
			}

			CarriagesM temp = new CarriagesM();
			temp._model = model.Text;
			temp._type = type.SelectedIndex;
			temp._weight = float.Parse(weight.Text);
			temp._max_load_weight = float.Parse(maxLoadWeight.Text);

			if (type.SelectedIndex == 0)
			{
				temp._max_seats = int.Parse(maxSeats.Text);
				temp._class = carriageClass.SelectedIndex;
			} else
            {
				temp._max_seats = -1;
				temp._class = -1;
			}

			// Сохранение нового юзера в БД
			if (isAdd)
			{
				// Добавляем объект в БД
				dbContext.CarriagesMs.Local.Add(temp);

				// Очищаем поля
				model.Text = null;
				type.SelectedIndex = 0;
				weight.Text = null;
				maxLoadWeight.Text = null;
				maxSeats.Text = null;
				carriageClass.SelectedIndex = 0;
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
				loco._max_load_weight = temp._max_load_weight;
				loco._max_seats = temp._max_seats;
				loco._class = temp._class;

				// Говорим контексту БД, что данный объект был изменен
				dbContext.Entry(loco).State = EntityState.Modified;
			}
			dbContext.SaveChanges();

			// Обновление списка
			SourceList = dbContext.CarriagesMs.Local.ToBindingList();

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

				CarriagesM deleteEntity = SourceList[index];

				var trains = (from o in dbContext.TrainsMs
							  where (o._first_carriage_id == deleteEntity._carriage_id
							  || o._second_carriage_id == deleteEntity._carriage_id
							  || o._third_carriage_id == deleteEntity._carriage_id
							  || o._fourth_carriage_id == deleteEntity._carriage_id
							  || o._fifth_carriage_id == deleteEntity._carriage_id)
							  select o);

				if (trains != null)
                {
					foreach (var train in trains)
                    {
						//1
						if (train._first_carriage_id == deleteEntity._carriage_id)
							train._first_carriage_id = -1;
						//2
						if (train._second_carriage_id == deleteEntity._carriage_id)
							train._second_carriage_id = -1;
						//3
						if (train._third_carriage_id == deleteEntity._carriage_id)
							train._third_carriage_id = -1;
						//4
						if (train._fourth_carriage_id == deleteEntity._carriage_id)
							train._fourth_carriage_id = -1;
						//5
						if (train._fifth_carriage_id == deleteEntity._carriage_id)
							train._fifth_carriage_id = -1;
					}
                }

				dbContext.CarriagesMs.Local.Remove(deleteEntity);

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
