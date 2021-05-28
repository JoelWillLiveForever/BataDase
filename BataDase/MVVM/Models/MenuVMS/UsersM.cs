﻿using System.Collections.Generic;
using System.ComponentModel;
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
