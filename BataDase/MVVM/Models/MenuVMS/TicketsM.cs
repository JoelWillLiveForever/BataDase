using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TicketsM
    {
        [Key]
        [DisplayName("Hidden")]
        public int _ticket_id { get; set; }
        [DisplayName("Hidden")]
        public int _schedule_id { get; set; }
        [DisplayName("Text_CarriageNum")]
        public int _carriage_number { get; set; }
        [DisplayName("Text_SeatNum")]
        public int _seatnum { get; set; }
        [DisplayName("Hidden")]
        public int _user_id { get; set; }

        [DisplayName("Hidden")]
        public virtual SchedulesM SchedulesM { get; set; }
        [DisplayName("Hidden")]
        public virtual UsersM UsersM { get; set; }
    }
}
