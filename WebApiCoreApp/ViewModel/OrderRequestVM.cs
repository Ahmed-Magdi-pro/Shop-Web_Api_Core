using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Models;

namespace WebApiCoreApp.ViewModel
{
    public class OrderRequestVM
    {
       
        public int id { get; set; }
        [Required]
        public int ItemNumbers { get; set; }
        [Required]
        public product product { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string CustomerName { get; set; }
        
    }
}
