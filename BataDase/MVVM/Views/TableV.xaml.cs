using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;

namespace BataDase.MVVM.Views
{
    public partial class TableV : UserControl
    {
        // Кастыль, но что поделать?
        public static DataGrid Current_DataGrid;

        public static Button Current_Button_Add;
        public static Button Current_Button_Edit;
        public static Button Current_Button_Delete;
        public static Button Current_Button_Request;

        public TableV()
        {
            InitializeComponent();

            Current_Button_Add = Button_Add;
            Current_Button_Edit = Button_Edit;
            Current_Button_Delete = Button_Delete;
            Current_Button_Request = Button_Request;

            if (Properties.Settings.Default.IsAdmin)
            {
                Button_Delete.Content = (string)App.Current.Resources["Text_Delete"];
            }
            else
            {
                Button_Delete.Content = (string)App.Current.Resources["Text_Buy"];
            }

            Current_DataGrid = DataGrid_Tables;

            DataGrid_Tables.MinColumnWidth = 175;
            DataGrid_Tables.MaxColumnWidth = 300;
            DataGrid_Tables.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Auto);
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
                    e.Column.Header = name == null ? displayName : System.Windows.Application.Current.FindResource(displayName);
                }
            }

            e.Column.IsReadOnly = true;
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
