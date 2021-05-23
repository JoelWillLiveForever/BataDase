using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;

namespace BataDase.MVVM.Views
{
    public partial class MenuV : UserControl
    {
        public MenuV()
        {
            InitializeComponent();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = GetPropertyDisplayName(e.PropertyDescriptor);

            if (!string.IsNullOrEmpty(displayName))
            {
                if (displayName == "Hidden")
                    e.Cancel = true;
                else
                {
                    string name = (string)App.Current.TryFindResource(displayName);

                    if (name != null)
                        e.Column.Header = App.Current.FindResource(displayName);
                }
            }
        }

        public static string GetPropertyDisplayName(object descriptor)
        {
            var pd = descriptor as PropertyDescriptor;

            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                var displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;

                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }

            }
            else
            {
                var pi = descriptor as PropertyInfo;

                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        var displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }

            return null;
        }

		private void Button_Settings_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            
		}

		private void Button_Account_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}

		private void Button_FAQ_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}
	}
}
