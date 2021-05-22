using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TicketsVM
    {
        public BindingList<TicketsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TicketsVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.TicketsMs.Load();

            SourceList = dbContext.TicketsMs.Local.ToBindingList();
        }
    }
}
