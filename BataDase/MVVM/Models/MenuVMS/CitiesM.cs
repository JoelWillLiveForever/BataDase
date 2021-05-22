using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    public class CitiesM
    {
        [Key]
        //[DisplayName("Hidden")]
        public int _city_id { get; set; }

        //[DisplayName("Text_City")]
        public string _city_name { get; set; }

        public float _latitude { get; set; }

        public float _longitude { get; set; }
    }
}
