using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int Count { get; set; }
        [NotMapped]
        public double Price { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
