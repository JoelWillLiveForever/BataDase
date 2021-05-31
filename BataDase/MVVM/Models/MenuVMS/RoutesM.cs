using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class RoutesM
    {
        [Key]
        [DisplayName("Id")]
        public int _route_id { get; set; }
        [DisplayName("Text_RouteName")]
        public string _route_name { get; set; }
        [DisplayName("Text_Start_Id")]
        public int _start_city_id { get; set; }
        [DisplayName("Text_Finish_Id")]
        public int _finish_city_id { get; set; }
        [DisplayName("Text_Distance")]
        public double _distance { get; set; }

        [DisplayName("Hidden")]
        public virtual CitiesM CitiesM_Start { get; set; }
        [DisplayName("Hidden")]
        public virtual CitiesM CitiesM_Finish { get; set; }
    }
}
