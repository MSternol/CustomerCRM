using CustomerCRM.App.Helpers;
using CustomerCRM.App.Services.LoadFromPathFile;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerCRM.App.Administrator
{
    internal class ManageSuppliersMenu
    {
        private List<ModelSupplier> suppliers = new List<ModelSupplier>();
        private string filePath = FileLocations.GetSupplierFilePath();

        public void ViewManageSuppliers()
        {
            ManageSuppliers();
        }

        private void ManageSuppliers()
        {
            SupplierLoader.LoadFromPathFileSupplier(filePath);
            Console.WriteLine("1. Dodaj dostawcę");
            Console.WriteLine("2. Usuń dostawcę");
            Console.WriteLine("3. Wyświetl listę dostawców");
            Console.WriteLine("0. Powrót");
            string operation = Console.ReadLine();

            switch (operation)
            {
                case "1":
                    Console.Clear();
                    AddSupplier();
                    break;
                case "2":
                    Console.Clear();
                    RemoveSupplier();
                    break;
                case "3":
                    Console.Clear();
                    DisplaySupplierList();
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie!!");
                    break;
            }
        }

        private void AddSupplier()
        {
            Console.Write("Podaj nazwę użytkownika dostawcy: ");
            string username = Console.ReadLine();

            if (!ValidationHelper.ValidateUsername(username))
            {
                Console.WriteLine("Nieprawidłowa nazwa użytkownika dostawcy. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj hasło dostawcy: ");
            string password = Console.ReadLine();

            if (!ValidationHelper.ValidatePassword(password))
            {
                Console.WriteLine("Nieprawidłowe hasło dostawcy. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj imię dostawcy: ");
            string firstName = Console.ReadLine();

            if (!ValidationHelper.ValidateName(firstName))
            {
                Console.WriteLine("Nieprawidłowe imię dostawcy. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj nazwisko dostawcy: ");
            string lastName = Console.ReadLine();

            if (!ValidationHelper.ValidateName(lastName))
            {
                Console.WriteLine("Nieprawidłowe nazwisko dostawcy. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj numer telefonu dostawcy: ");
            string phoneNumber = Console.ReadLine();

            if (!ValidationHelper.ValidatePhoneNumber(phoneNumber))
            {
                Console.WriteLine("Nieprawidłowy numer telefonu dostawcy. Spróbuj ponownie.");
                return;
            }

            Console.Write("Podaj nazwę firmy dostawcy: ");
            string business = Console.ReadLine();

            if (string.IsNullOrEmpty(business))
            {
                Console.WriteLine("Nieprawidłowa nazwa firmy dostawcy. Spróbuj ponownie.");
                return;
            }

            string id = Guid.NewGuid().ToString();
            string role = "Dostawca";

            ModelSupplier newSupplier = new ModelSupplier(username, password, firstName, lastName, "", phoneNumber, business, id, role);

            suppliers.Add(newSupplier);
            SavingService.SaveSupplier(newSupplier);
            Console.WriteLine("Dostawca został dodany.");
        }

        private void RemoveSupplier()
        {
            Console.Write("Podaj ID dostawcy do usunięcia: ");
            string supplierId = Console.ReadLine();

            ModelSupplier supplierToRemove = suppliers.FirstOrDefault(s => s.ID == supplierId);

            if (supplierToRemove != null)
            {
                suppliers.Remove(supplierToRemove);
                Console.WriteLine("Dostawca został usunięty.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono dostawcy o podanym ID.");
            }

            SavingService.SaveSupplier(supplierToRemove);
        }

        private void DisplaySupplierList()
        {
            Console.WriteLine("Lista dostawców:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.ID}, Nazwa użytkownika: {supplier.Username}, Imię: {supplier.FirstName}, Nazwisko: {supplier.LastName}");
            }
        }
    }
}

