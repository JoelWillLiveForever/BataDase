using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class BodiesM
    {
        [Key]
        public int _body_id { get; set; }
        public int _bodyNumber { get; set; }
        public int _carriage_id { get; set; }
    }
}
