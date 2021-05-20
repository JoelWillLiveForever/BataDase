using System.Collections.Generic;
using System.Windows.Controls;

namespace BataDase.MVVM.Views
{
    public partial class UserV : UserControl
    {
        private bool isTrue;
        private class Object
        {
            public int Number { get; set; }
        }

        public UserV()
        {
            InitializeComponent();

            List<Object> someList = new List<Object>();

            for (int i = 0; i < 200; i++)
            {
                Object obj = new Object();
                obj.Number = i;

                someList.Add(obj);
            }

            DataGrid_Tables.ItemsSource = someList;
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
