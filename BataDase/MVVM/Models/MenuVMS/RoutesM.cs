using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class RoutesM
    {
        [Key]
        public int _route_id { get; set; }
        public string _route_name { get; set; }
        public int _start_city_id { get; set; }
        public int _finish_city_id { get; set; }
        public double _distance { get; set; }

        [DisplayName("Hidden")]
        public virtual CitiesM CitiesM_Start { get; set; }
        [DisplayName("Hidden")]
        public virtual CitiesM CitiesM_Finish { get; set; }
    }
}
