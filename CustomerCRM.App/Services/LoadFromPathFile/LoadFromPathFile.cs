using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.Warehouse.Interface;
using DataStorage.LoggApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Services.LoadFromPathFile
{
    public class AdministratorLoader
    {
        public static ModelAdministrator LoadFromPathFileAdministrator(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');

                        if (parts.Length >= 8)
                        {
                            string username = parts[0].Trim();
                            string password = parts[1].Trim();
                            string email = parts[2].Trim();
                            string id = parts[3].Trim();
                            string role = parts[4].Trim();
                            string firstName = parts[5].Trim();
                            string lastName = parts[6].Trim();
                            string position = parts[7].Trim();

                            if (role == "Administrator")
                            {
                                return new ModelAdministrator(username, password, firstName, lastName, email, position, id, role);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje.");
                    LogToFileMessage.LogError("Plik nie istnieje.", "AdministratorLoader.LoadFromPathFileAdministrator");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Błąd odczytu pliku: {ex.Message}");
                LogToFileMessage.LogError($"Błąd odczytu pliku: {ex.Message}", "AdministratorLoader.LoadFromPathFileAdministrator");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił nieoczekiwany błąd: {ex.Message}");
                LogToFileMessage.LogError($"Wystąpił nieoczekiwany błąd: {ex.Message}", "AdministratorLoader.LoadFromPathFileAdministrator");
            }
            return null;
        }
    }

    public class CustomerLoader
    {
        public static ModelCustomer LoadFromPathFileCustomer(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');

                        if (parts.Length >= 12)
                        {
                            string username = parts[0].Trim();
                            string password = parts[1].Trim();
                            string firstName = parts[2].Trim();
                            string lastName = parts[3].Trim();
                            string country = parts[4].Trim();
                            string role = parts[5].Trim();
                            string city = parts[6].Trim();
                            string address = parts[7].Trim();
                            string postalCode = parts[8].Trim();
                            string phoneNumber = parts[9].Trim();
                            string email = parts[10].Trim();
                            string id = parts[11].Trim();

                            if (role == "Klient")
                            {
                                return new ModelCustomer(username, password, firstName, lastName, country, city, address, postalCode, phoneNumber, email, id, role);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje.");
                    LogToFileMessage.LogError("Plik nie istnieje.", "CustomerLoader.LoadFromPathFileCustomer");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Błąd odczytu pliku: {ex.Message}");
                LogToFileMessage.LogError($"Błąd odczytu pliku: {ex.Message}", "CustomerLoader.LoadFromPathFileCustomer");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił nieoczekiwany błąd: {ex.Message}");
                LogToFileMessage.LogError($"Wystąpił nieoczekiwany błąd: {ex.Message}", "CustomerLoader.LoadFromPathFileCustomer");
            }

            return null;
        }
    }

    public class SupplierLoader
    {
        public static ModelSupplier LoadFromPathFileSupplier(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');

                        if (parts.Length >= 9)
                        {
                            string username = parts[0].Trim();
                            string password = parts[1].Trim();
                            string email = parts[2].Trim();
                            string id = parts[3].Trim();
                            string role = parts[4].Trim();
                            string firstName = parts[5].Trim();
                            string lastName = parts[6].Trim();
                            string phoneNumber = parts[7].Trim();
                            string business = parts[8].Trim();

                            if (role == "Dostawca")
                            {
                                return new ModelSupplier(username, password, firstName, lastName, email, phoneNumber, business, id, role);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje.");
                    LogToFileMessage.LogError("Plik nie istnieje.", "SupplierLoader.LoadFromPathFileSupplier");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Błąd odczytu pliku: {ex.Message}");
                LogToFileMessage.LogError($"Błąd odczytu pliku: {ex.Message}", "SupplierLoader.LoadFromPathFileSupplier");
            }

            return null;
        }
    }

    public class WarehouseLoader
    {
        public void LoadFromFile(string warehouseFilePath, IStorable storable)
        {
            try
            {
                if (File.Exists(warehouseFilePath))
                {
                    using (StreamReader reader = new StreamReader(warehouseFilePath))
                    {
                        int id = 0;
                        string name = "";
                        string category = "";
                        int quantity = 0;
                        decimal price = 0;

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(':');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                switch (key)
                                {
                                    case "ID":
                                        int.TryParse(value, out id);
                                        break;
                                    case "Name":
                                        name = value;
                                        break;
                                    case "Category":
                                        category = value;
                                        break;
                                    case "Quantity":
                                        quantity = int.Parse(value.Replace("szt", "").Trim(), CultureInfo.InvariantCulture);
                                        break;
                                    case "Price":
                                        price = decimal.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
                                        break;
                                }
                            }
                            else if (string.IsNullOrWhiteSpace(line))
                            {
                                IProduct product = storable.CreateProduct(id, name, category, quantity, price);
                                if (product != null)
                                {
                                    storable.Products.Add(product);
                                    if (id >= storable.NextProductId)
                                    {
                                        storable.NextProductId = id + 1;
                                    }
                                }

                                id = 0;
                                name = "";
                                category = "";
                                quantity = 0;
                                price = 0;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje.");
                    // Obsługa błędu braku pliku
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas wczytywania pliku: {ex.Message}");
                // Obsługa błędu wczytywania pliku
            }
        }
    }
}
