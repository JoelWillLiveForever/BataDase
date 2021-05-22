using BataDase.MVVM.Models.MenuVMS;
using System.Collections.Generic;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    class CitiesVM
    {
        public List<CitiesM> SourceList { get; set; }

        public CitiesVM()
        {
            SourceList = new List<CitiesM>();

            for (int i = 0; i < 100; i ++)
            {
                CitiesM temp = new CitiesM();
                temp.City = "Moscow";
                SourceList.Add(temp);
            }
        }
    }
}
