using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.Views.ManageV
{
    public partial class SettingsV : UserControl
    {
        public SettingsV()
        {
            InitializeComponent();

            Button_Dark.IsChecked = BataDase.Properties.Settings.Default.IsDarkTheme;
            Button_Light.IsChecked = !Button_Dark.IsChecked;


            if (App.Language.IetfLanguageTag == "ru-RU")
            {
                Debug.WriteLine("Русккий");
                Button_Russian.IsChecked = true;
            } else if (App.Language.IetfLanguageTag == "en-US")
            {
                Debug.WriteLine("Англ");
                Button_English.IsChecked = true;
            } else if (App.Language.IetfLanguageTag == "ja-JP")
            {
                Debug.WriteLine("Япониш");
                Button_Japanese.IsChecked = true;
            }
        }

        private void RadioButton_Theme_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Button_Light)
            {
                if (BataDase.Properties.Settings.Default.IsDarkTheme)
                {
                    Button_Dark.IsChecked = false;
                    Button_Light.IsChecked = true;
                    App.ChangePalette();
                }
            } else
            {
                if (!BataDase.Properties.Settings.Default.IsDarkTheme)
                {
                    Button_Dark.IsChecked = true;
                    Button_Light.IsChecked = false;
                    App.ChangePalette();
                }
            }
        }

        private void Button_Language_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Button_English)
            {
                if (App.Language != App.Languages[1])
                {
                    App.Language = App.Languages[1];
                    Button_English.IsChecked = true;

                    Button_Russian.IsChecked = false;
                    Button_Japanese.IsChecked = false;
                }
            } else if (sender == Button_Russian)
            {
                if (App.Language != App.Languages[0])
                {
                    App.Language = App.Languages[0];
                    Button_Russian.IsChecked = true;

                    Button_English.IsChecked = false;
                    Button_Japanese.IsChecked = false;
                }
            } else if (sender == Button_Japanese)
            {
                if (App.Language != App.Languages[2])
                {
                    App.Language = App.Languages[2];
                    Button_Japanese.IsChecked = true;

                    Button_Russian.IsChecked = false;
                    Button_English.IsChecked = false;
                }
            }
        }
    }
}
