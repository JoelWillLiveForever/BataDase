using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class PathsM
    {
        [Key]
        [DisplayName("Hidden")]
        public int _path_id { get; set; }

        [DisplayName("Text_IsPath")]
        public int _is_path { get; set; }

        [DisplayName("Hidden")]
        public int _city_id { get; set; }
    }
}
