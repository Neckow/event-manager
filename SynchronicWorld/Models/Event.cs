using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynchronicWorld.Models
{
    public class Event
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public String EventName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public String Status { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Begin Date is required")]
        public DateTime DateDeDebut { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime DateDeFin { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public String Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TypeId { get; set; }

        public virtual EventType EventType { get; set; }
    }
}