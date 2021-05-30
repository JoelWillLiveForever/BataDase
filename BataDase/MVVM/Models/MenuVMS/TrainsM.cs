using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TrainsM
    {
        [Key]
        public int _train_id { get; set; }
        public int _locomotive_id { get; set; }
        public int _first_carriage_id { get; set; }
        public int _second_carriage_id { get; set; }
        public int _third_carriage_id { get; set; }
        public int _fourth_carriage_id { get; set; }
        public int _fifth_carriage_id { get; set; }
        public double _train_avgspeed { get; set; }
        public int _reserved_seats { get; set; }

        [DisplayName("Hidden")]
        public virtual LocomotivesM LocomotivesM { get; set; }
        [DisplayName("Hidden")]
        public virtual CarriagesM CarriagesM_First { get; set; }
        [DisplayName("Hidden")]
        public virtual CarriagesM CarriagesM_Second { get; set; }
        [DisplayName("Hidden")]
        public virtual CarriagesM CarriagesM_Third { get; set; }
        [DisplayName("Hidden")]
        public virtual CarriagesM CarriagesM_Fourth { get; set; }
        [DisplayName("Hidden")]
        public virtual CarriagesM CarriagesM_Fifth { get; set; }
    }
}
