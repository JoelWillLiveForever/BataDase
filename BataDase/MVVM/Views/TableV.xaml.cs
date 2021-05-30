using BataDase.Core;
using BataDase.MVVM.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.Views
{
    public partial class TableV : UserControl
    {
        // Кастыль, но что поделать?
        public static DataGrid Current_DataGrid;

        public static RelayCommand RequestCommand { get; set; }
        public static RelayCommand AddCommand { get; set; }
        public static RelayCommand EditCommand { get; set; }
        public static RelayCommand DeleteCommand { get; set; }
        public static RelayCommand BuyCommand { get; set; }

        public TableV()
        {
            InitializeComponent();
            Current_DataGrid = DataGrid_Tables;
            DataGrid_Tables.MinColumnWidth = 175;
            DataGrid_Tables.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Auto);

            if (Properties.Settings.Default.IsAdmin == false)
            {
                Button_Add.Visibility = (Visibility)2;
                Button_Edit.Visibility = (Visibility)2;
                Button_Request.Visibility = (Visibility)2;

                Button_Delete.Content = App.Current.Resources["Text_Buy"];
                Debug.WriteLine("Penis");
                Button_Delete.Command = new TableVM().BuyCommand;
            }
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
    }
}
