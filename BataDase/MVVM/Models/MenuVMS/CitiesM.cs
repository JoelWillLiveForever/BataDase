using System.ComponentModel;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class CitiesM
    {
        [DisplayName("Text_RowNumber")]
        public int RowNumber { get; set; }

        [DisplayName("Text_City")]
        public string City { get; set; }
    }
}
