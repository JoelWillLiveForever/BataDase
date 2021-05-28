using BataDase.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.Views.ManageV
{
    public partial class AccountV : UserControl
    {
        public AccountV()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CurrentUserId = -1;
            Properties.Settings.Default.IsAdmin = true;
            Properties.Settings.Default.Visibility = "Visible";

            MainVM.LoginVM_RelayCommand.Execute(null);
        }
    }
}
