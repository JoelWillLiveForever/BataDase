using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class CarriagesM
    {
        [Key]
        [DisplayName("Hidden")]
        public int _carriage_id { get; set; }

        [DisplayName("Text_Model")]
        public string _model { get; set; }

        [DisplayName("Text_Type")]
        public string _type { get; set; }

        [DisplayName("Text_Weight")]
        public float _weight { get; set; }

        [DisplayName("Text_CargoWeight")]
        public float _cargo_weight { get; set; }

        [DisplayName("Text_PMW")]
        public float _pmw { get; set; }

        [DisplayName("Text_MaxSeats")]
        public int _max_seats { get; set; }
    }
}
