using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TicketsM
    {
        [Key]
        public int _ticket_id { get; set; }
        public int _schedule_id { get; set; }
        public int _carriage_number { get; set; }
        public int _seatnum { get; set; }
        public int _user_id { get; set; }

        public virtual SchedulesM SchedulesM { get; set; }
        public virtual UsersM UsersM { get; set; }
    }
}
