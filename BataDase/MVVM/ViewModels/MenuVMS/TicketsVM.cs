using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.Views;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TicketsVM : ObjectModel
    {
        public BindingList<TicketsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TicketsVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.TicketsMs.Load();

            SourceList = dbContext.TicketsMs.Local.ToBindingList();
        }

        public void Save()
        {
            if (dbContext != null)
                dbContext.SaveChanges();
        }

        public void Close()
        {
            if (dbContext != null) dbContext = null;
        }

        public void Connect()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.TicketsMs.Load();

            SourceList = dbContext.TicketsMs.Local.ToBindingList();
            TableV.Current_DataGrid.ItemsSource = SourceList;
        }

        public void Request()
        {
            throw new System.NotImplementedException();
        }

        public void AddEdit(bool isAdd)
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
