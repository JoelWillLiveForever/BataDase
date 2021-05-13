using System;
using System.Windows;

namespace BataDase
{

    public partial class App : Application
    {
        private const string URI_THEME_LIGHT = "/Resources/Palettes/LightPalette.xaml";
        private const string URI_THEME_DARK = "/Resources/Palettes/DarkPalette.xaml";

        private ResourceDictionary PaletteDictionary
        {
            get { return Resources.MergedDictionaries[0]; }
        }

        private bool IsDarkPalette = true;

        public void ChangePalette()
        {
            Uri NewUri;

            if (IsDarkPalette)
            {
                NewUri = new Uri(URI_THEME_LIGHT, UriKind.RelativeOrAbsolute);
            }
            else
            {
                NewUri = new Uri(URI_THEME_DARK, UriKind.RelativeOrAbsolute);
            }

            PaletteDictionary.MergedDictionaries.Clear();
            PaletteDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = NewUri });

            IsDarkPalette = !IsDarkPalette;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainV mainV = new MainV();
            mainV.Show();
        }
    }
}