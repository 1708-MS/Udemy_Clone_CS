using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
