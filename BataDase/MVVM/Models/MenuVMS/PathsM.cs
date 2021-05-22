using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class PathsM
    {
        [Key]
        //[DisplayName("Hidden")]
        public int _path_id { get; set; }

        //[DisplayName("Text_IsPath")]
        public int _firstCity_id { get; set; }

        //[DisplayName("Hidden")]
        public int _secondCity_id { get; set; }
    }
}
