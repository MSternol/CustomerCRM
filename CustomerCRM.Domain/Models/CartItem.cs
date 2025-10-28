using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using System.Threading.Tasks;

namespace CustomerCRM.Domain.Models
{
    public class CartItem
    {
        public ProductBase Product { get; set; }
        public int Quantity { get; set; }
    }
}
