using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class SchedulesM
    {
        [Key]
        public int _schedule_id { get; set; }
        public int _train_id { get; set; }
        public int _route_id { get; set; }
        public double _departure_datetime { get; set; }
        public double _arrival_datetime { get; set; }
        public int _status { get; set; }

        public virtual TrainsM TrainsM { get; set; }
        public virtual RoutesM RoutesM { get; set; }

        public virtual ICollection<TicketsM> TicketsMs { get; set; }

        public SchedulesM()
        {
            TicketsMs = new HashSet<TicketsM>();
        }
    }
}
