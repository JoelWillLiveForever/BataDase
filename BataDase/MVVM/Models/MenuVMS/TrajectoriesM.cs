using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class TrajectoriesM
    {
        [Key]
        public int _trajectory_id { get; set; }
        public int _path_id { get; set; }
        public int _route_id { get; set; }
    }
}
