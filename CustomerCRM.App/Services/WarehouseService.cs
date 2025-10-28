using System;
using System.Collections.Generic;
using System.Linq;
using CustomerCRM.Domain.Services.SaveFileToPath;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using CustomerCRM.Domain.Services.Warehouse.Interface;
using DataStorage;
using CustomerCRM.Domain.Services.Warehouse;
using CustomerCRM.App.Services.LoadFromPathFile;

namespace CustomerCRM.Domain.Services.Warehouse
{
    public class WarehouseService : IStorable
    {
        private List<IProduct> products = new List<IProduct>();

        private int nextProductId;
        private readonly string warehouseFilePath;

        public WarehouseService()
        {
            warehouseFilePath = FileLocations.GetWarehouseFilePath();
            LoadFromFile();
        }

        public List<IProduct> Products => products;

        public int NextProductId
        {
            get => nextProductId;
            set => nextProductId = value;
        }

        public void AddProduct(IProduct product)
        {
            product.Id = NextProductId++;
            products.Add(product);
            SaveToFile();
        }

        public void RemoveProduct(int productId)
        {
            IProduct productToRemove = products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                SaveToFile();
            }
        }

        public void ModifyProductQuantity(int productId, int newQuantity)
        {
            IProduct productToModify = products.FirstOrDefault(p => p.Id == productId);
            if (productToModify != null)
            {
                productToModify.Quantity = newQuantity;
                SaveToFile();
            }
        }

        public void ModifyProductPrice(int productId, decimal newPrice)
        {
            IProduct productToModify = products.FirstOrDefault(p => p.Id == productId);
            if (productToModify != null)
            {
                productToModify.Price = newPrice;
                SaveToFile();
            }
        }

        public List<IProduct> GetProductsByName(string productName)
        {
            return products.Where(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<IProduct> GetProductsByCategory(string category)
        {
            return products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IProduct GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.Id == productId);
        }

        public void ListProductsDisplay()
        {
            Console.WriteLine("Lista produktów w magazynie:");

            foreach (var product in products)
            {
                Console.WriteLine(product.GetProductInfo());
            }
        }

        public IProduct CreateProduct(int id, string name, string category, int quantity, decimal price)
        {
            switch (category)
            {
                case "Owoce":
                    return new FruitProduct { Id = id, Name = name, Category = category, Quantity = quantity, Price = price };

                case "Warzywa":
                    return new VegetableProduct { Id = id, Name = name, Category = category, Quantity = quantity, Price = price };

                case "Elektronika":
                    return new ElectronicProduct { Id = id, Name = name, Category = category, Quantity = quantity, Price = price };

                case "Chemia":
                    return new ChemicalProduct { Id = id, Name = name, Category = category, Quantity = quantity, Price = price };

                default:
                    return null;
            }
        }

        public void SaveToFile()
        {
            SavingService savingService = new SavingService(products);
            savingService.SaveToFile(warehouseFilePath);
        }


        public void LoadFromFile()
        {
            WarehouseLoader loader = new WarehouseLoader();
            loader.LoadFromFile(warehouseFilePath, this);
        }
    }
}

