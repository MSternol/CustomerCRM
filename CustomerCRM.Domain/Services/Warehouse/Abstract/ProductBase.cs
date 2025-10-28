using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.Domain.Services.Warehouse.Interface;

namespace CustomerCRM.Domain.Services.Warehouse.Abstract
{
    public abstract class ProductBase : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public abstract string GetProductInfo();
    }
}
