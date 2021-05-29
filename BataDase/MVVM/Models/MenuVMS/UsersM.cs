using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BataDase.MVVM.Models.MenuVMS
{
    //// Концепт закрыт.
    //public class UserModel
    //{
    //    [DisplayName("Text_Surname")]
    //    public string Surname { get; set; }

    //    [DisplayName("Text_Name")]
    //    public string Name { get; set; }

    //    [DisplayName("Text_Lastname")]
    //    public string Lastname { get; set; }

    //    [DisplayName("Text_Sex")]
    //    public string Sex { get; set; }

    //    [DisplayName("Text_Birthday")]
    //    public string Birthday { get; set; }

    //    [DisplayName("Text_Login")]
    //    public string Login { get; set; }

    //    [DisplayName("Text_Password")]
    //    public string Password { get; set; }

    //    [DisplayName("Text_Access")]
    //    public string AccessLevel { get; set; }

    //    [DisplayName("Text_Bill")]
    //    public double Bill { get; set; }
    //}

    public class UsersM
    {
        [Key]
        public int _user_id { get; set; }
        public string _surname { get; set; }
        public string _name { get; set; }
        public string _lastname { get; set; }
        public int _sex { get; set; }
        public double _birthday { get; set; }
        public string _login { get; set; }
        public string _pass { get; set; }
        public int _access { get; set; }
        public double _bill { get; set; }

        public virtual ICollection<TicketsM> TicketsMs { get; set; }

        public UsersM()
        {
            TicketsMs = new HashSet<TicketsM>();
        }
    }
}
