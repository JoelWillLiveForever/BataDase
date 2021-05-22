using BataDase.MVVM.Models.MenuVMS;
using System;
using System.Collections.Generic;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    class TicketsVM
    {
        public List<TicketsM> SourceList { get; set; }

        public TicketsVM()
        {
            SourceList = new List<TicketsM>();

            for (int i = 0; i < 50; i ++)
            {
                TicketsM temp = new TicketsM();
                temp.RowNumber = (i + 1);
                temp.Series = "AB";
                temp.Number = new Random().Next();

                SourceList.Add(temp);
            }
        }
    }
}
