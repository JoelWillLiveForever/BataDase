using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class LocomotivesM
    {
        [Key]
        public int _locomotive_id { get; set; }
        public string _model { get; set; }
        public int _type { get; set; }
        public double _weight { get; set; }
        public double _max_trailer_weight { get; set; }
        public double _avgspeed0 { get; set; }
        public double _avgspeed100 { get; set; }

        public virtual ICollection<TrainsM> TrainsMs { get; set; }

        public LocomotivesM()
        {
            TrainsMs = new HashSet<TrainsM>();
        }
    }
}
