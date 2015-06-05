using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynchronicWorld.Models
{
    public class ContributionType
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public String Name { get; set; }

        public virtual ICollection<Contribution> Contributions { get; set; }
    }
}