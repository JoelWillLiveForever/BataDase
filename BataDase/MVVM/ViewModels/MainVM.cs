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

        public RelayCommand MyUserVMRelayCommand { get; set; }

        public UserVM MyUserVM { get; set; }

        public MainVM()
        {
            // Create ViewModels
            MyUserVM = new UserVM();

            // Set current ViewModel
            CurrentActionVM = MyUserVM;

            // Set handles
            MyUserVMRelayCommand = new RelayCommand(o =>
            {
                CurrentActionVM = MyUserVM;
            });
        }
    }
}
