using BataDase.Core;
using BataDase.MVVM.ViewModels.MenuVMS;

namespace BataDase.MVVM.ViewModels
{
    public interface ObjectModel
    {
        void Request();
        void AddEdit(bool isAdd);
        void Delete();

        void ConnectAndUpdate();
    }

    public class TableVM : ObservableObject
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

        private CarriagesVM carriagesVM;
        private CitiesVM citiesVM;
        private LocomotivesVM locomotivesVM;
        private RoutesVM routesVM;
        private SchedulesVM schedulesVM;
        private TicketsVM ticketsVM;
        private TrainsVM trainsVM;
        private UsersVM usersVM;


        public RelayCommand RequestCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand BuyCommand { get; set; }

        public TableVM()
        {

            carriagesVM = new CarriagesVM();
            citiesVM = new CitiesVM();
            locomotivesVM = new LocomotivesVM();
            routesVM = new RoutesVM();
            schedulesVM = new SchedulesVM();
            ticketsVM = new TicketsVM();
            trainsVM = new TrainsVM();
            usersVM = new UsersVM();

            CurrentModel = locomotivesVM;

            RequestCommand = new RelayCommand(o =>
            {
                CurrentModel.Request();
            });

            AddCommand = new RelayCommand(o =>
            {
                CurrentModel.AddEdit(true);
            });

            EditCommand = new RelayCommand(o =>
            {
                CurrentModel.AddEdit(false);
            });

            DeleteCommand = new RelayCommand(o =>
            {
                CurrentModel.Delete();
            });

            BuyCommand = new RelayCommand(o =>
            {
                CurrentModel.AddEdit(false);
            });
        }

        public void ChangeModel(string name)
        {
            if (name == "Carriages")
            {
                CurrentModel = carriagesVM;
            }
            else if (name == "Cities")
            {
                CurrentModel = citiesVM;
            }
            else if (name == "Locomotives")
            {
                CurrentModel = locomotivesVM;
            }
            else if (name == "Routes")
            {
                CurrentModel = routesVM;
            }
            else if (name == "Schedules")
            {
                CurrentModel = schedulesVM;
            }
            else if (name == "Tickets")
            {
                CurrentModel = ticketsVM;
            }
            else if (name == "Trains")
            {
                CurrentModel = trainsVM;
            }
            else if (name == "Users")
            {
                CurrentModel = usersVM;
            }

            CurrentModel.ConnectAndUpdate();
        }
    }
}
