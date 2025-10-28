using System;
using System.IO;
namespace DataStorage
{
    public static class FileLocations
    {
        private static string BaseDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileDirectory");
        private static string LogDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoggMessage");
        public static string GetFilePath(string fileName)
        {
            return Path.Combine(BaseDirectory, fileName);
        }
        public static string GetFilePathLogApp(string fileName)
        {
            return Path.Combine(LogDirectory, fileName);
        }


        public static string GetAdminFilePath()
        {
            return GetFilePath("Administrators.txt");
        }

        public static string GetCustomerFilePath()
        {
            return GetFilePath("Customers.txt");
        }

        public static string GetSupplierFilePath()
        {
            return GetFilePath("Supplier.txt");
        }

        public static string GetWarehouseFilePath()
        {
            return GetFilePath("Warehouse.txt");
        }

        public static string GetShoppingCartFilePath()
        {
            return GetFilePath("ShoppingCart.txt");
        }

        public static string GetLogErrorFilePath()
        {
            return GetFilePathLogApp("LogError.txt");
        }
        public static string GetLogSuccessFilePath()
        {
            return GetFilePathLogApp("LogSuccess.txt");
        }
    }
}