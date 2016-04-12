using System.ComponentModel.DataAnnotations;

namespace asp.net_MVC.Models
{
    public class Contact
    {
        [Key]
        public int contactId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Contact Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Email ")]
        public string email { get; set; }

        [Display(Name = "Phone Number ")]
        public int phone { get; set; }

        [Display(Name = "Company Name ")]
        public string companyName { get; set; }
        [Required]
        [Display(Name = "Subject ")]
        public string subject { get; set; }
        [Required]
        [Display(Name = "Message ")]
        public string message { get; set; }
    }
}
