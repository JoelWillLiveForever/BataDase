using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TicketsM
    {
        [Key]
        public int _ticket_id { get; set; }
        public int _number { get; set; }
        public string _series { get; set; }
        public int _seatnum { get; set; }
        public long _date_of_departure { get; set; }
        public long _date_of_arrival { get; set; }
        public float _price { get; set; }
        public int _user_id { get; set; }
        public int _carriage_id { get; set; }
        public int _firstCity_id { get; set; }
        public int _secondCity_id { get; set; }
    }
}
