using CustomerCRM.App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;
using CustomerCRM.Domain.Services.Warehouse.Abstract;

namespace CustomerCRM.App.Services
{
    public class SettingsService
    {
        private ModelCustomer customerData;
        private string filePath;
        private SavingService savingService;
        private ModelCustomer customer;
        private readonly List<ProductBase> products;

        public SettingsService(ModelCustomer customerData, string filePath, List<ProductBase> products)
        {
            this.customerData = customerData;
            this.filePath = filePath;
            this.savingService = new SavingService(products);
        }

        public void ChangePersonalData()
        {
            string updateOption;
            do
            {
                updateOption = Console.ReadLine();

                switch (updateOption)
                {
                    case "1":
                        ChangeUsername();
                        break;
                    case "2":
                        ChangePassword();
                        break;
                    case "3":
                        ChangeName("Imię", newName => customerData.FirstName = newName);
                        break;
                    case "4":
                        ChangeName("Nazwisko", newName => customerData.LastName = newName);
                        break;
                    case "5":
                        ChangeEmail();
                        break;
                    case "6":
                        ChangePhoneNumber();
                        break;
                    case "7":
                        DisplayPersonalData();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.\n");
                        break;
                }
            } while (updateOption != "0");
        }

        private void ChangeUsername()
        {
            string newUsername;
            do
            {
                Console.Write("Podaj nową nazwę użytkownika: ");
                newUsername = Console.ReadLine();
            } while (!ValidationHelper.ValidateUsername(newUsername));
            customerData.Username = newUsername;

            SavingService.SaveCustomer(customerData);
        }

        private void ChangePassword()
        {
            string newPassword;
            do
            {
                Console.Write("Podaj nowe hasło: ");
                newPassword = Console.ReadLine();
            } while (!ValidationHelper.ValidatePassword(newPassword));
            customerData.Password = newPassword;

            SavingService.SaveCustomer(customerData);
        }

        private void ChangeName(string fieldName, Action<string> updateAction)
        {
            string newName;
            do
            {
                Console.Write($"Podaj nowe {fieldName}: ");
                newName = Console.ReadLine();
            } while (!ValidationHelper.ValidateName(newName));
            updateAction(newName);

            // Po zmianie danych, zapisz je ponownie za pomocą SavingService
            SavingService.SaveCustomer(customerData);
        }

        private void ChangePhoneNumber()
        {
            string newPhone;
            do
            {
                Console.Write("Podaj nowy numer telefonu: ");
                newPhone = Console.ReadLine();
            } while (!ValidationHelper.ValidatePhoneNumber(newPhone));
            customerData.PhoneNumber = newPhone;

            // Po zmianie danych, zapisz je ponownie za pomocą SavingService
            SavingService.SaveCustomer(customerData);
        }

        private void ChangeEmail()
        {
            string newEmail;
            do
            {
                Console.Write("Podaj nowy email: ");
                newEmail = Console.ReadLine();
            } while (!ValidationHelper.ValidateEmail(newEmail));
            customerData.Email = newEmail;

            // Po zmianie danych, zapisz je ponownie za pomocą SavingService
            SavingService.SaveCustomer(customerData);
        }

        private void DisplayPersonalData()
        {
            Console.WriteLine("==== Dane Personalne ====");
            Console.WriteLine($"Role: {customerData.Role}");
            Console.WriteLine($"FirstName: {customerData.FirstName}");
            Console.WriteLine($"LastName: {customerData.LastName}");
            Console.WriteLine($"Email: {customerData.Email}");
            Console.WriteLine($"PhoneNumber: {customerData.PhoneNumber}");
            Console.WriteLine("==== Dane Zamieszkania ====");
            Console.WriteLine($"Country: {customerData.Country}");
            Console.WriteLine($"City: {customerData.City}");
            Console.WriteLine($"Address: {customerData.Address}");
            Console.WriteLine($"PostalCode: {customerData.PostalCode}");
        }

    }
}
