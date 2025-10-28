using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using CustomerCRM.Domain.Services.Warehouse.Interface;
using DataStorage.LoggApp;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.Domain.Services.SaveFileToPath
{
    public class SavingService
    {
        private List<ProductBase> products = new List<ProductBase>();

        public List<IProduct> Products { get; }

        public SavingService(List<ProductBase> products)
        {
            this.products = products;
        }

        public SavingService(List<IProduct> products1)
        {
            Products = products1;
        }

        public static void SaveCustomer(ModelCustomer customer)
        {
            try
            {
                string filePath = FileLocations.GetCustomerFilePath(); // Pobierz ścieżkę z FileLocations

                // Wczytaj dane z istniejącego pliku (jeśli istnieje)
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    if (lines.Length > 0)
                    {
                        string[] existingData = lines[0].Split('|');
                        // Porównaj istniejące dane z nowymi danymi klienta
                        if (existingData.Length == 13 &&
                            existingData[0] == customer.Username &&
                            existingData[1] == customer.Password &&
                            existingData[2] == customer.FirstName &&
                            existingData[3] == customer.LastName &&
                            existingData[4] == customer.Country &&
                            existingData[5] == customer.Role &&
                            existingData[6] == customer.City &&
                            existingData[7] == customer.Address &&
                            existingData[8] == customer.PostalCode &&
                            existingData[9] == customer.PhoneNumber &&
                            existingData[10] == customer.Email &&
                            existingData[11] == customer.ID)
                        {
                            // Dane klienta są takie same, więc nie trzeba niczego nadpisywać
                            Console.WriteLine("Dane klienta są takie same, nie wykonano zapisu.");
                            return;
                        }
                    }
                }

                // Zapisz nowe dane klienta
                string dataToSave = $"{customer.Username}|{customer.Password}|{customer.FirstName}|{customer.LastName}|{customer.Country}|{customer.Role}|{customer.City}|{customer.Address}|{customer.PostalCode}|{customer.PhoneNumber}|{customer.Email}|{customer.ID}|";
                File.WriteAllText(filePath, dataToSave);

                LogToFileMessage.LogSuccess($"Zapisane dane klienta w pliku {filePath}", "SavingService.SaveCustomer");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisu danych Klienta: " + ex.Message);
                LogToFileMessage.LogError($"Błąd zapisu danych Klienta: {ex.Message}", "SavingService.SaveCustomer");
            }
        }



        public static void SaveSupplier(ModelSupplier supplier)
        {
            try
            {
                string filePath = FileLocations.GetSupplierFilePath(); // Pobierz ścieżkę z FileLocations

                // Wczytaj dane z istniejącego pliku (jeśli istnieje)
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    if (lines.Length > 0)
                    {
                        string[] existingData = lines[0].Split('|');
                        // Porównaj istniejące dane z nowymi danymi dostawcy
                        if (existingData.Length == 9 &&
                            existingData[0] == supplier.Username &&
                            existingData[1] == supplier.Password &&
                            existingData[2] == supplier.FirstName &&
                            existingData[3] == supplier.LastName &&
                            existingData[4] == supplier.Business &&
                            existingData[5] == supplier.Role &&
                            existingData[6] == supplier.PhoneNumber &&
                            existingData[7] == supplier.Email &&
                            existingData[8] == supplier.ID)
                        {
                            // Dane dostawcy są takie same, więc nie trzeba niczego nadpisywać
                            Console.WriteLine("Dane dostawcy są takie same, nie wykonano zapisu.");
                            return;
                        }
                    }
                }

                // Zapisz nowe dane dostawcy
                string dataToSave = $"{supplier.Username}|{supplier.Password}|{supplier.FirstName}|{supplier.LastName}|{supplier.Business}|{supplier.Role}|{supplier.PhoneNumber}|{supplier.Email}|{supplier.ID}|";
                File.WriteAllText(filePath, dataToSave);

                LogToFileMessage.LogSuccess($"Zapisane dane dostawcy w pliku {filePath}", "SavingService.SaveSupplier");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisu danych Dostawcy: " + ex.Message);
                LogToFileMessage.LogError($"Błąd zapisu danych Dostawcy: {ex.Message}", "SavingService.SaveSupplier");
            }
        }


        public static void SaveAdmin(ModelAdministrator admin)
        {
            string adminFileLocation = FileLocations.GetAdminFilePath();

            try
            {
                string dataToSave = $"{admin.Username}|{admin.Password}|{admin.FirstName}|{admin.LastName}|{admin.Email}|{admin.Position}|{admin.ID}|";
                string fileName = adminFileLocation;

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(dataToSave);
                }

                LogToFileMessage.LogSuccess($"Zapisano dane administratora w pliku {fileName}", "SavingService.SaveAdmin");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisu danych administratora: " + ex.Message);
                LogToFileMessage.LogError($"Błąd zapisu danych administratora: {ex.Message}", "SavingService.SaveAdmin");
            }
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var product in Products)
                    {
                        writer.WriteLine($"ID:{product.Id}");
                        writer.WriteLine($"Name:{product.Name}");
                        writer.WriteLine($"Category:{product.Category}");
                        writer.WriteLine($"Quantity:{product.Quantity} szt");
                        writer.WriteLine($"Price:{product.Price.ToString("F2", CultureInfo.InvariantCulture)}");
                        writer.WriteLine(); // Pusta linia między produktami
                    }
                }

                LogToFileMessage.LogSuccess($"Zapisano dane magazynu w pliku {filePath}", "SavingService.SaveToFileWarehouse");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisu danych magazynu: " + ex.Message);
                LogToFileMessage.LogError($"Błąd zapisu danych magazynu: {ex.Message}", "SavingService.SaveToFileWarehouse");
            }
        }

    }
}
