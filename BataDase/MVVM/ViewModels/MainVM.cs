using BataDase.Core;

namespace BataDase.MVVM.ViewModels
{
    public class MainVM : ObservableObject
    {
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

        public RelayCommand MyLoginVMRelayCommand { get; set; }
        public RelayCommand MyMenuVMRelayCommand { get; set; }

        public LoginVM MyLoginVM { get; set; }
        public MenuVM MyMenuVM { get; set; }

        public MainVM()
        {
            // Create ViewModels
            MyLoginVM = new LoginVM();
            MyMenuVM = new MenuVM();

            // Set current ViewModel
            CurrentActionVM = MyLoginVM;

            // Set handles
            MyLoginVMRelayCommand = new RelayCommand(o =>
            {
                CurrentActionVM = MyLoginVM;
            });

            MyMenuVMRelayCommand = new RelayCommand(o =>
            {
                CurrentActionVM = MyMenuVM;
            });
        }
    }
}
