using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;

namespace asp.net_MVC.Models
{
    public enum PetTypes
    {
        Cat, Dog, Rabbit, Snake, Ferret, Lizard, Others
    }

    public class Pet
    {
        public int petId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Pet Name")]
        public string petName { get; set; }

        [Display(Name = "Pet Type")]
        [DisplayFormat(NullDisplayText = "Select the Pet Type")]
        public PetTypes? petTypes { get; set; }
        [DataType(DataType.Date)]
        //The DisplayFormat attribute is used to explicitly specify the date format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Missing Date")]
        public DateTime missingDate { get; set; }
        [DataType(DataType.Currency)]
        public string Description { get; set; }
        public string Postcode { get; set; }
        public decimal Reward { get; set; }
    }

    public class PetDBContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
    }

}