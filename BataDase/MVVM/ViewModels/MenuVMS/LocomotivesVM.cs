using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.Views;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;


namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class LocomotivesVM : ObjectModel
    {
        public BindingList<LocomotivesM> SourceList { get; set; }
        private AppDBContext dbContext;

        private Label Model, Type, Weight, PMW, AvgSpeed0, AvgSpeed100;
        private TextBox model, weight, pmw, avgspeed0, avgspeed100;
        private ComboBox type;

        public LocomotivesVM()
        {
			// Инициализация контекста БД
			dbContext = AppDBContext.GetInstance();
			dbContext.UsersMs.Load();

			// Margin
			Thickness temp = new Thickness(5);

			// Назначение свойств пачке контролов
			// Лэйбл "Модель", назначение текста, строки и колонки в Grid
			Model = new Label();
			Model.Content = App.Current.Resources["Text_Model"] + ":";
			Grid.SetRow(Model, 0);
			Grid.SetColumn(Model, 0);

			// Лэйбл "Тип", назначение текста, строки и колонки в Grid
			Type = new Label();
			Type.Content = App.Current.Resources["Text_Type"] + ":";
			Grid.SetRow(Type, 1);
			Grid.SetColumn(Type, 0);

			// Лэйбл "Вес", назначение текста, строки и колонки в Grid
			Weight = new Label();
			Weight.Content = App.Current.Resources["Text_Weight"] + ":";
			Grid.SetRow(Weight, 2);
			Grid.SetColumn(Weight, 0);

			// Лэйбл "Разрешённая ММ", назначение текста, строки и колонки в Grid
			PMW = new Label();
			PMW.Content = App.Current.Resources["Text_PMW"] + ":";
			Grid.SetRow(PMW, 3);
			Grid.SetColumn(PMW, 0);

			// Лэйбл "Средняя скорость 0", назначение текста, строки и колонки в Grid
			AvgSpeed0 = new Label();
			AvgSpeed0.Content = App.Current.Resources["Text_AvgS"] + ":";
			Grid.SetRow(AvgSpeed0, 4);
			Grid.SetColumn(AvgSpeed0, 0);

			// Лэйбл "Средняя скорость 100", назначение текста, строки и колонки в Grid
			AvgSpeed100 = new Label();
			AvgSpeed100.Content = App.Current.Resources["Text_Login"] + ":";
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
			Grid.SetRow(weight, 1);
			Grid.SetColumn(weight, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			pmw = new TextBox();
			pmw.Margin = temp;
			Grid.SetRow(pmw, 2);
			Grid.SetColumn(pmw, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			avgspeed0 = new TextBox();
			avgspeed0.Margin = temp;
			Grid.SetRow(avgspeed0, 2);
			Grid.SetColumn(avgspeed0, 1);

			// Текстбокс "Разрешённая ММ", назначение отсутпа (Margin), строки и колонки в Grid
			avgspeed100 = new TextBox();
			avgspeed100.Margin = temp;
			Grid.SetRow(avgspeed100, 2);
			Grid.SetColumn(avgspeed100, 1);

			// Комбобокс "Тип", назначение отсутпа (Margin), строки и колонки в Grid
			// А также, назначение элементов комбобокс: "Мужской" и "Женский"
			type = new ComboBox();
			type.Margin = temp;
			Grid.SetRow(type, 3);
			Grid.SetColumn(type, 1);

			// Вытягивание полов из ресурсов
			string cargoLoco = (string)App.Current.Resources["Text_CargoCarriage"];
			string passengerLoco = (string)App.Current.Resources["Text_PassengerCarriage"];

			// Назначение элементов комбобокс
			type.Items.Insert(0, passengerLoco);
			type.Items.Insert(1, cargoLoco);
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

        public void Add()
        {
            throw new System.NotImplementedException();
        }

        public void Edit()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
