using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.ViewModel;

namespace WebApiCoreApp.Models
{
    public class order
    {
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }

        [Required]
        public int ItemNumbers { get; set; }
        [Required]
        public int TotallCost { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public int userId { get; set; }
        public RegisterVM user { get; set; }
        public virtual ICollection<product> Products { get; set; }
        public order()
        {
            Products = new Collection<product>();
        }
    }
}
