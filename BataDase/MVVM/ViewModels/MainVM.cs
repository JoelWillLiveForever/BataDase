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

        public RelayCommand MyMenuVM_RelayCommand { get; set; }

        public MenuVM MyMenuVM { get; set; }

        public MainVM()
        {
            // Create ViewModels
            MyMenuVM = new MenuVM();

            // Set current ViewModel
            CurrentActionVM = MyMenuVM;

            // Set handles
            MyMenuVM_RelayCommand = new RelayCommand(o =>
            {
                CurrentActionVM = MyMenuVM;
            });
        }
    }
}
