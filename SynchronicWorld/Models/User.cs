using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI.HtmlControls;

namespace SynchronicWorld.Models
{
    public class User
    {
        /* public User()
         {
             Friends = HashSet<User>;
         }*/

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")] 
        public String Name { get; set; }


        [Required(ErrorMessage = "Username is required")] 
        public String UserName { get; set; }

        [Required(ErrorMessage = "Password is required")] 
        [Display(Name = "UserPassword"),DataType(DataType.Password)]
        public String UserPassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public String Role { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }

    }
}