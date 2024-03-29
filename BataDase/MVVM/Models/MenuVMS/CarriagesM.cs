﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class CarriagesM
    {
        [Key]
        [DisplayName("Id")]
		public int _carriage_id { get; set; }
        [DisplayName("Text_Model")]
		public string _model { get; set; }
        [DisplayName("Text_Type")]
		public int _type { get; set; }
        [DisplayName("Text_Weight")]
		public double _weight { get; set; }
        [DisplayName("Text_MaxLoadWeight")]
		public double _max_load_weight { get; set; }
        [DisplayName("Text_MaxSeats")]
		public int _max_seats { get; set; }
        [DisplayName("Text_Class")]
		public int _class { get; set; }

        [DisplayName("Hidden")]
        public virtual ICollection<TrainsM> TrainsMs_First { get; set; }
        [DisplayName("Hidden")]
        public virtual ICollection<TrainsM> TrainsMs_Second { get; set; }
        [DisplayName("Hidden")]
        public virtual ICollection<TrainsM> TrainsMs_Third { get; set; }
        [DisplayName("Hidden")]
        public virtual ICollection<TrainsM> TrainsMs_Fourth { get; set; }
        [DisplayName("Hidden")]
        public virtual ICollection<TrainsM> TrainsMs_Fifth { get; set; }

        public CarriagesM()
        {
            TrainsMs_First = new HashSet<TrainsM>();
            TrainsMs_Second = new HashSet<TrainsM>();
            TrainsMs_Third = new HashSet<TrainsM>();
            TrainsMs_Fourth = new HashSet<TrainsM>();
            TrainsMs_Fifth = new HashSet<TrainsM>();
        }
    }
}
