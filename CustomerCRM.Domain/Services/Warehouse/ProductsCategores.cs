using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CustomerCRM.Domain.Services.Warehouse.Abstract;


namespace CustomerCRM.Domain.Services.Warehouse
{
    public class FruitProduct : ProductBase
    {
        public override string GetProductInfo()
        {
            return $"{Name} (Owoce) - Quantity: {Quantity}, Price: {Price:C}";
        }
    }
    public class VegetableProduct : ProductBase
    {
        public override string GetProductInfo()
        {
            return $"{Name} (Warzywa) - Quantity: {Quantity}, Price: {Price:C}";
        }
    }
    public class ElectronicProduct : ProductBase
    {
        public override string GetProductInfo()
        {
            return $"{Name} (Elektronika) - Quantity: {Quantity}, Price: {Price:C}";
        }
    }
    public class ChemicalProduct : ProductBase
    {
        public override string GetProductInfo()
        {
            return $"{Name} (Chemia) - Quantity: {Quantity}, Price: {Price:C}";
        }
    }

}
