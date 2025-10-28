using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.Domain.Services.Warehouse.Abstract;

namespace CustomerCRM.Domain.Services.Warehouse.Interface
{
    public interface IStorable
    {
        List<IProduct> Products { get; }
        int NextProductId { get; set; }

        void SaveToFile();
        void LoadFromFile();
        IProduct CreateProduct(int id, string name, string category, int quantity, decimal price);
    }
}
