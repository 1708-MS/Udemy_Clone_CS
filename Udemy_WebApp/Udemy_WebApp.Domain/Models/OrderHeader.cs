using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class OrderHeader
    {
        public int OrderHeaderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
