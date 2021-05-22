using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class PathsM
    {
        public int rowNum;
        [Key]
        public int _path_id { get; set; }
        [DisplayName("Text_IsPath")]
        public int _is_path { get; set; }
        [DisplayName("Text_City")]
        public int _city_id { get; set; }
    }
}
