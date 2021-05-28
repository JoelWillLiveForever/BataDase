using System.Windows.Controls;

namespace BataDase.MVVM.Views
{
    public partial class MenuV : UserControl
    {
        public MenuV()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button_Account.IsChecked = false;
            Button_Carriages.IsChecked = false;
            Button_Cities.IsChecked = false;
            Button_Info.IsChecked = false;
            Button_Locomotives.IsChecked = false;
            Button_Routes.IsChecked = false;
            Button_Schedules.IsChecked = false;
            Button_Settings.IsChecked = false;
            Button_Tickets.IsChecked = false;
            Button_Trains.IsChecked = false;
            Button_Users.IsChecked = false;

            (sender as RadioButton).IsChecked = true;
        }
    }
}
