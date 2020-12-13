using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreApp.ViewModel
{
    public class RegisterVM
    {
        public int id { get; set; }
        [Required(ErrorMessage ="User name is Required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 5)]
        public string Password { get; set; }


        [NotMapped]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        [Compare("Password",ErrorMessage = "password do not match")]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<OrderRequestVM> Orders { get; set; }
        public RegisterVM()
        {
            Orders = new Collection<OrderRequestVM>();
        }
    }
}
