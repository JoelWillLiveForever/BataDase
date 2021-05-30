using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class SchedulesM
    {
        [Key]
        [DisplayName("Hidden")]
        public int _schedule_id { get; set; }
        [DisplayName("Text_TrainID")]
        public int _train_id { get; set; }
        [DisplayName("Hidden")]
        public int _route_id { get; set; }
        [DisplayName("Text_DepartureDate")]
        public double _departure_datetime { get; set; }
        [DisplayName("Text_ArrivalDate")]
        public double _arrival_datetime { get; set; }
        [DisplayName("Text_Status")]
        public int _status { get; set; }
        [DisplayName("Text_Price")]
        public double _price { get; set; }

        [DisplayName("Hidden")]
        public virtual TrainsM TrainsM { get; set; }
        [DisplayName("Hidden")]
        public virtual RoutesM RoutesM { get; set; }
        [DisplayName("Hidden")]
        public virtual ICollection<TicketsM> TicketsMs { get; set; }

        public SchedulesM()
        {
            TicketsMs = new HashSet<TicketsM>();
        }
    }
}
