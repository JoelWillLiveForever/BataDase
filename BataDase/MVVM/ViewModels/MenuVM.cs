using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.ViewModels.MenuVMS;

namespace BataDase.MVVM.ViewModels
{
    public class MenuVM : ObservableObject
    {
        private object _currentModel;
        public object CurrentModel
        {
            get { return _currentModel; }
            set
            {
                _currentModel = value;
                OnPropertyChanged("CurrentModel");
            }
        }

        // Команда, реагирующая на нажатие кнопки из меню
        public RelayCommand MenuButtonClick { get; set; }

        private CitiesVM citiesVM;
        private TicketsVM ticketsVM;
        private CarriagesVM carriagesVM;
        private PathsVM pathsVM;

        public MenuVM()
        {
            citiesVM = new CitiesVM();
            ticketsVM = new TicketsVM();
            carriagesVM = new CarriagesVM();
            pathsVM = new PathsVM();

            CurrentModel = citiesVM;

            // Повесить команды на MenuButtonClick
            MenuButtonClick = new RelayCommand(ClickExecute);
        }

        public void ClickExecute(object param)
        {
            System.Diagnostics.Debug.WriteLine($"Clicked: {param as string}");

            string name = param as string;
            if (name == "Cities")
            {
                CurrentModel = citiesVM;
            } else if (name == "Tickets")
            {
                CurrentModel = ticketsVM;
            }
            else if (name == "Carriages")
            {
                CurrentModel = carriagesVM;
			}
            else if (name == "Paths")
            {
                CurrentModel = pathsVM;
            }
        }
    }
}
