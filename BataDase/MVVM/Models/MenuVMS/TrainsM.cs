using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TrainsM
    {
        [Key]
        public int _train_id { get; set; }
        public int _trainNumber { get; set; }
        public int _headNumber { get; set; }
        public int _bodyNumber { get; set; }
        public int _route_id { get; set; }
    }
}
