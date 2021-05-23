using BataDase.Core;
using BataDase.MVVM.ViewModels.MenuVMS;

namespace BataDase.MVVM.ViewModels
{
    public interface ObjectModel
    {
        void Request();
        void Add();
        void Edit();
        void Delete();

        void Close();
        void Connect();
    }

    public class MenuVM : ObservableObject
    {
        private ObjectModel _currentModel;
        public ObjectModel CurrentModel
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

        public RelayCommand RequestCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private BodiesVM bodiesVM;
        private CarriagesVM carriagesVM;
        private CitiesVM citiesVM;
        private HeadsVM headsVM;
        private LocomotivesVM locomotivesVM;
        private PathsVM pathsVM;
        private RoutesVM routesVM;
        private TicketsVM ticketsVM;
        private TrainsVM trainsVM;
        private TrajectoriesVM trajectoriesVM;
        private UsersVM usersVM;

        public MenuVM()
        {
            bodiesVM = new BodiesVM();
            carriagesVM = new CarriagesVM();
            citiesVM = new CitiesVM();
            headsVM = new HeadsVM();
            locomotivesVM = new LocomotivesVM();
            pathsVM = new PathsVM();
            routesVM = new RoutesVM();
            ticketsVM = new TicketsVM();
            trainsVM = new TrainsVM();
            trajectoriesVM = new TrajectoriesVM();
            usersVM = new UsersVM();

            CurrentModel = bodiesVM;

            // Повесить команды на MenuButtonClick
            MenuButtonClick = new RelayCommand(ClickExecute);

            RequestCommand = new RelayCommand(o =>
            {
                CurrentModel.Request();
            });

            AddCommand = new RelayCommand(o =>
            {
                CurrentModel.Add();
            });

            EditCommand = new RelayCommand(o =>
            {
                CurrentModel.Edit();
            });

            DeleteCommand = new RelayCommand(o =>
            {
                CurrentModel.Delete();
            });
        }

        public void ClickExecute(object param)
        {
            System.Diagnostics.Debug.WriteLine($"Clicked: {param as string}");
            string name = param as string;

            CurrentModel.Close();

            if (name == "Bodies")
            {
                CurrentModel = bodiesVM;
            } else if (name == "Carriages")
            {
                CurrentModel = carriagesVM;
            } else if (name == "Cities")
            {
                CurrentModel = citiesVM;
			} else if (name == "Heads")
            {
                CurrentModel = headsVM;
            } else if (name == "Locomotives")
            {
                CurrentModel = locomotivesVM;
            } else if (name == "Paths")
            {
                CurrentModel = pathsVM;
            } else if (name == "Routes")
            {
                CurrentModel = routesVM;
            } else if (name == "Tickets")
            {
                CurrentModel = ticketsVM;
            } else if (name == "Trains")
            {
                CurrentModel = trainsVM;
            } else if (name == "Trajectories")
            {
                CurrentModel = trajectoriesVM;
            } else if (name == "Users")
            {
                CurrentModel = usersVM;
            }

            CurrentModel.Connect();
        }
    }
}
