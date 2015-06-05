using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynchronicWorld.Models
{
    public class Contribution
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public String ContributionName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ContributionTypeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }

        public virtual ContributionType ContributionType { get; set; }
    }
}