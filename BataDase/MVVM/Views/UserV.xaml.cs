using System.Windows.Controls;

namespace BataDase.MVVM.Views
{
    public partial class UserV : UserControl
    {
        private bool isTrue = true;

        public UserV()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isTrue)
                App.Language = App.Languages[0];
            else
                App.Language = App.Languages[2];
            isTrue = !isTrue;
        }
    }
}
