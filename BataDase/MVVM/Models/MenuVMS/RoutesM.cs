using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class RoutesM
    {
        [Key]
        [DisplayName("Hidden")]
        public int _route_id { get; set; }
        [DisplayName("Text_RouteName")]
        public string _route_name { get; set; }
        [DisplayName("Hidden")]
        public int _start_city_id { get; set; }
        [DisplayName("Hidden")]
        public int _finish_city_id { get; set; }
        [DisplayName("Text_Distance")]
        public double _distance { get; set; }

        [DisplayName("Hidden")]
        public virtual CitiesM CitiesM_Start { get; set; }
        [DisplayName("Hidden")]
        public virtual CitiesM CitiesM_Finish { get; set; }
    }
}
