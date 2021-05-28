using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Data;

namespace BataDase
{
    public partial class App : Application
    {
        private const string URI_THEME_LIGHT = "Resources/Palettes/Palette.Light.xaml";
        private const string URI_THEME_DARK = "Resources/Palettes/Palette.xaml";

        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            InitializeComponent();
            App.LanguageChanged += App_LanguageChanged;

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("ru-RU"));  // Нейтральная культура для этого проекта
            m_Languages.Add(new CultureInfo("en-US"));
            m_Languages.Add(new CultureInfo("ja-JP"));

            Language = BataDase.Properties.Settings.Default.DefaultLanguage;
        }

        // Эвент для оповещения всех окон приложения
        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "en-US":
                    case "ja-JP":
                        dict.Source = new Uri(String.Format("Resources/Languages/Lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/Languages/Lang.xaml", UriKind.Relative);
                        break;
                }

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Languages/Lang.")
                                              select d).First();

                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                //4. Вызываем евент для оповещения всех окон.
                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Language = BataDase.Properties.Settings.Default.DefaultLanguage;
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            BataDase.Properties.Settings.Default.DefaultLanguage = Language;
            BataDase.Properties.Settings.Default.Save();
        }

        private static ResourceDictionary PaletteDictionary
        {
            get
            {
                return (from d in Current.Resources.MergedDictionaries
                        where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Palettes/Palette.")
                        select d).First();
            }
        }

        public static void ChangePalette()
        {
            Uri NewUri;

            if (BataDase.Properties.Settings.Default.IsDarkTheme)
            {
                NewUri = new Uri(URI_THEME_LIGHT, UriKind.RelativeOrAbsolute);
            }
            else
            {
                NewUri = new Uri(URI_THEME_DARK, UriKind.RelativeOrAbsolute);
            }

            if (PaletteDictionary != null)
            {
                ResourceDictionary oldDict = PaletteDictionary;

                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);

                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, new ResourceDictionary() { Source = NewUri });

                BataDase.Properties.Settings.Default.IsDarkTheme = !BataDase.Properties.Settings.Default.IsDarkTheme;
                BataDase.Properties.Settings.Default.Save();
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // set saved theme
            if (!BataDase.Properties.Settings.Default.IsDarkTheme)
            {
                Uri NewUri = new Uri(URI_THEME_LIGHT, UriKind.RelativeOrAbsolute);

                if (PaletteDictionary != null)
                {
                    ResourceDictionary oldDict = PaletteDictionary;

                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);

                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, new ResourceDictionary() { Source = NewUri });
                }
            }

            MainV mainV = new MainV();
            mainV.Show();
        }
    }
}