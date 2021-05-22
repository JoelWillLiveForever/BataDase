using System.ComponentModel;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TicketsM
    {
        [DisplayName("Text_TicketSeries")]
        public string Series { get; set; }

        [DisplayName("Text_TicketNumber")]
        public int Number { get; set; }
    }
}
