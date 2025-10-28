using DataStorage.LoggApp;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using CustomerCRM.Domain.Models;
using CustomerCRM.App.Services;
using CustomerCRM.App.Services.LoadFromPathFile;

namespace CustomerCRM.App.Customer
{
    public class ViewChangePersonal
    {
        private readonly List<ProductBase> products;

        public void ViewChangePersonalData()
        {
            try
            {
                // Uzyskaj ścieżkę do pliku z danymi klienta z FileLocations
                string filePath = FileLocations.GetCustomerFilePath();

                // Załaduj dane klienta z pliku
                ModelCustomer customerData = CustomerLoader.LoadFromPathFileCustomer(filePath);

                if (customerData != null)
                {
                    // Inicjalizuj SettingsService z danymi klienta i ścieżką pliku
                    SettingsService settingsService = new SettingsService(customerData, filePath, products);

                    Console.WriteLine("Wybierz opcję:");
                    Console.WriteLine("1. Zmień nazwę użytkownika");
                    Console.WriteLine("2. Zmień hasło");
                    Console.WriteLine("3. Zmień imię");
                    Console.WriteLine("4. Zmień nazwisko");
                    Console.WriteLine("5. Zmień email");
                    Console.WriteLine("6. Zmień numer telefonu");
                    Console.WriteLine("7. Wyświetl dane personalne");
                    Console.WriteLine("0. Wyjście");

                    settingsService.ChangePersonalData();
                    LogToFileMessage.LogSuccess("Zmiana danych personalnych zakończona pomyślnie.", "ViewChangePersonal.ViewChangePersonalData");
                }
                else
                {
                    Console.WriteLine("Nie udało się wczytać danych personalnych. Sprawdź ścieżkę pliku.");
                    LogToFileMessage.LogError("Nie udało się wczytać danych personalnych. Sprawdź ścieżkę pliku.", "ViewChangePersonal.ViewChangePersonalData");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                string location = "ViewChangePersonal.ViewChangePersonalData";
                LogToFileMessage.LogError($"Błąd: {ex.Message}", location);
            }
        }
    }
}
