using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TrainsM
    {
        [Key]
        [DisplayName("Id")]
        public int _train_id { get; set; }
        [DisplayName("Text_Locomotive_Id")]
        public int _locomotive_id { get; set; }
        [DisplayName("Text_Carriage_Id_1")]
        public int _first_carriage_id { get; set; }
        [DisplayName("Text_Carriage_Id_2")]
        public int _second_carriage_id { get; set; }
        [DisplayName("Text_Carriage_Id_3")]
        public int _third_carriage_id { get; set; }
        [DisplayName("Text_Carriage_Id_4")]
        public int _fourth_carriage_id { get; set; }
        [DisplayName("Text_Carriage_Id_5")]
        public int _fifth_carriage_id { get; set; }
        [DisplayName("Text_AvgSpeed")]
        public double _train_avgspeed { get; set; }
        [DisplayName("Text_MaxSeats")]
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
