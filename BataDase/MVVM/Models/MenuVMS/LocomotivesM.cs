using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class LocomotivesM
    {
        [Key]
        [DisplayName("Text_LocoID")]
        public int _locomotive_id { get; set; }
        [DisplayName("Text_Model")]
        public string _model { get; set; }
        [DisplayName("Text_Type")]
        public int _type { get; set; }
        [DisplayName("Text_Weight")]
        public double _weight { get; set; }
        [DisplayName("Text_MaxTrailerWeight")]
        public double _max_trailer_weight { get; set; }
        [DisplayName("Text_AvgSpeed0")]
        public double _avgspeed0 { get; set; }
        [DisplayName("Text_AvgSpeed100")]
        public double _avgspeed100 { get; set; }

        [DisplayName("Hidden")]
        public virtual ICollection<TrainsM> TrainsMs { get; set; }

        public LocomotivesM()
        {
            TrainsMs = new HashSet<TrainsM>();
        }
    }
}
