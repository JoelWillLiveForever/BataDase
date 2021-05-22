using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class RoutesM
    {
        [Key]
        public int _route_id { get; set; }
        public string _route_name { get; set; }
    }
}
