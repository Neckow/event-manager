using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynchronicWorld.Models
{
    public class Friend
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int FriendId1 { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int FriendId2 { get; set; }

        public virtual ICollection<User> Users { get; set; } 

    }
}