using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class HeadsM
    {
        [Key]
        public int _head_id { get; set; }
        public int _headNumber { get; set; }
        public int _locomotive_id { get; set; }
    }
}
