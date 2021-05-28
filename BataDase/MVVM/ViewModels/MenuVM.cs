using BataDase.Core;
using BataDase.MVVM.ViewModels.ManageVMS;

namespace BataDase.MVVM.ViewModels
{
    public class MenuVM : ObservableObject
    {
        private string _buttonUpdateVisibility;
        public string ButtonUpdateVisibility
        {
            get { return _buttonUpdateVisibility; }
            set
            {
                _buttonUpdateVisibility = value;
                OnPropertyChanged(nameof(ButtonUpdateVisibility));
            }
        }

        private object _currentActionVM;
        public object CurrentActionVM
        {
            get { return _currentActionVM; }
            set
            {
                _currentActionVM = value;
                OnPropertyChanged("CurrentActionVM");
            }
        }

        // Команда, реагирующая на нажатие кнопки из меню
        public RelayCommand MenuButtonClick { get; set; }
        
        private TableVM tableVM;
        private AccountVM accountVM;
        private SettingsVM settingsVM;
        private InfoVM infoVM;

        public MenuVM()
        {
            tableVM = new TableVM();
            accountVM = new AccountVM();
            settingsVM = new SettingsVM();
            infoVM = new InfoVM();

            CurrentActionVM = tableVM;

            // Повесить команды на MenuButtonClick
            MenuButtonClick = new RelayCommand(ClickExecute);
        }

        public void ClickExecute(object param)
        {
            System.Diagnostics.Debug.WriteLine($"Clicked: {param as string}");
            string name = param as string;

            if (name == "Account")
            {
                CurrentActionVM = accountVM;
            }
            else if (name == "Settings")
            {
                CurrentActionVM = settingsVM;
            }
            else if (name == "Info")
            {
                CurrentActionVM = infoVM;
            } else
            {
                tableVM.ChangeModel(name);
                if (CurrentActionVM != tableVM) CurrentActionVM = tableVM;
            }
        }
    }
}
