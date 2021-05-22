using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.Collections.Generic;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    class CitiesVM
    {
        public List<CitiesM> SourceList { get; set; }

		private AppDBContext dbContext;

		public CitiesVM()
        {
			SourceList = new List<CitiesM>();

			dbContext = AppDBContext.GetInstance();
			dbContext.Cities.Load();

			var temp = dbContext.Cities.Local.ToBindingList();

			for (int i = 0; i < temp.Count; i++)
			{
				CitiesM tempObj = new CitiesM();
				tempObj._city_name = temp[i]._city_name;
				tempObj._latitude = temp[i]._latitude;
				tempObj._longitude = temp[i]._longitude;
				SourceList.Add(tempObj);
			}
		}
	}
}
