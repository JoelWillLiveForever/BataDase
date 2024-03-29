﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public partial class CitiesM
    {
        [Key]
        [DisplayName("Id")]
        public int _city_id { get; set; }
        [DisplayName("Text_City")]
        public string _city_name { get; set; }
        [DisplayName("Text_Latitude")]
        public double _latitude { get; set; }
        [DisplayName("Text_Longitude")]
        public double _longitude { get; set; }

        [DisplayName("Hidden")]
        public virtual ICollection<RoutesM> RoutesMs_Start { get; set; }
        [DisplayName("Hidden")]
        public virtual ICollection<RoutesM> RoutesMs_Finish { get; set; }

        public CitiesM()
        {
            RoutesMs_Start = new HashSet<RoutesM>();
            RoutesMs_Finish = new HashSet<RoutesM>();
        }
    }
}
