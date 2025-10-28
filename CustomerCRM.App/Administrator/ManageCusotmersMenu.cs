using CustomerCRM.App.Helpers;
using CustomerCRM.App.Services.LoadFromPathFile;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Administrator
{
    internal class ManageCusotmersMenu
    {
        private List<ModelCustomer> customers = new List<ModelCustomer>();
        private string filePath = FileLocations.GetCustomerFilePath();
        public void ViewManageCustomers()
        {
            ManageCustomers();
        }
        private void ManageCustomers()
        {
            CustomerLoader.LoadFromPathFileCustomer(filePath);
            Console.WriteLine("1. Dodaj klienta");
            Console.WriteLine("2. Usuń klienta");
            Console.WriteLine("3. Wyświetl listę klientów");
            Console.WriteLine("0. Powrót");
            string operation = Console.ReadLine();

            switch (operation)
            {
                case "1":
                    Console.Clear();
                    AddCustomer();
                    break;
                case "2":
                    Console.Clear();
                    RemoveCustomer();
                    break;
                case "3":
                    Console.Clear();
                    DisplayCustomerList();
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie!!");
                    break;
            }
        }

        private void AddCustomer()
        {
            Console.Write("Podaj nazwę użytkownika: ");
            string username = Console.ReadLine();

            if (!ValidationHelper.ValidateUsername(username))
            {
                Console.WriteLine("Nieprawidłowa nazwa użytkownika. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();

            if (!ValidationHelper.ValidatePassword(password))
            {
                Console.WriteLine("Nieprawidłowe hasło. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj imię: ");
            string firstName = Console.ReadLine();

            if (!ValidationHelper.ValidateName(firstName))
            {
                Console.WriteLine("Nieprawidłowe imię. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj nazwisko: ");
            string lastName = Console.ReadLine();

            if (!ValidationHelper.ValidateName(lastName))
            {
                Console.WriteLine("Nieprawidłowe nazwisko. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj kraj: ");
            string country = Console.ReadLine();

            if (string.IsNullOrEmpty(country))
            {
                Console.WriteLine("Nieprawidłowy kraj. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj miasto: ");
            string city = Console.ReadLine();

            if (string.IsNullOrEmpty(city))
            {
                Console.WriteLine("Nieprawidłowe miasto. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj adres: ");
            string address = Console.ReadLine();

            if (!ValidationHelper.ValidateAddress(address))
            {
                Console.WriteLine("Nieprawidłowy adres. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj kod pocztowy (np. 12-345): ");
            string postalCode = Console.ReadLine();

            if (!ValidationHelper.ValidatePostalCode(postalCode))
            {
                Console.WriteLine("Nieprawidłowy kod pocztowy. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj numer telefonu (9 cyfr): ");
            string phoneNumber = Console.ReadLine();

            if (!ValidationHelper.ValidatePhoneNumber(phoneNumber))
            {
                Console.WriteLine("Nieprawidłowy numer telefonu. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj adres email: ");
            string email = Console.ReadLine();

            if (!ValidationHelper.ValidateEmail(email))
            {
                Console.WriteLine("Nieprawidłowy adres email. Spróbuj ponownie.");
                return;
            }

            string id = Guid.NewGuid().ToString();
            string role = "Klient";

            ModelCustomer newCustomer = new ModelCustomer(username, password, firstName, lastName, country, city, address, postalCode, phoneNumber, email, id, role);

            customers.Add(newCustomer);
            SavingService.SaveCustomer(newCustomer);
            Console.WriteLine("Klient został dodany.");
        }


        private void RemoveCustomer()
        {
            Console.Write("Podaj ID klienta do usunięcia: ");
            string customerId = Console.ReadLine();

            ModelCustomer customerToRemove = customers.FirstOrDefault(c => c.ID == customerId);

            if (customerToRemove != null)
            {
                customers.Remove(customerToRemove);
                Console.WriteLine("Klient został usunięty.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym ID.");
            }
            SavingService.SaveCustomer(customerToRemove);
        }

        private void DisplayCustomerList()
        {
            Console.WriteLine("Lista klientów:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.ID}, Nazwa użytkownika: {customer.Username}, Imię: {customer.FirstName}, Nazwisko: {customer.LastName}");
            }
        }

    }
}
