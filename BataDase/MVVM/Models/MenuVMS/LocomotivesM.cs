using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class LocomotivesM
    {
        [Key]
        public int _locomotive_id { get; set; }
        public string _model { get; set; }
        public string _type { get; set; }
        public float _weight { get; set; }
        public float _pmw { get; set; }
        public float _avgspeed0 { get; set; }
        public float _avgspeed100 { get; set; }
    }
}
