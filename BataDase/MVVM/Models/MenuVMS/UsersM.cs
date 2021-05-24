using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{ 
    public class UsersM
    {
        [Key]
        public int _user_id { get; set; }

        public string _surname { get; set; }
        public string _name { get; set; }
        public string _lastname { get; set; }
        public byte _sex { get; set; }
        public long _birthday { get; set; }
        public string _login { get; set; }
        public string _pass { get; set; }
        public byte _access { get; set; }
        public float _bill { get; set; }
    }
}
