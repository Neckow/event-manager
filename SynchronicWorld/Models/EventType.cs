using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynchronicWorld.Models
{
    public class EventType
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage =  "Type is required")]
        public String Type { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}