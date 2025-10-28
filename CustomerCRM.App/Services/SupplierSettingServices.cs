using CustomerCRM.App.Helpers;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Services
{
    internal class SupplierSettingServices
    {
        private ModelSupplier supplier;

        public void service()
        {
            ChangeSupplierData(supplier);
        }
        private void ChangeSupplierData(ModelSupplier supplier)
        {
            string updateOption;
            do
            {
                updateOption = Console.ReadLine();

                switch (updateOption)
                {
                    case "1":
                        ChangeUsername(supplier);
                        break;
                    case "2":
                        ChangePassword(supplier);
                        break;
                    case "3":
                        ChangeName("Imię", newName => supplier.FirstName = newName);
                        break;
                    case "4":
                        ChangeName("Nazwisko", newName => supplier.LastName = newName);
                        break;
                    case "5":
                        ChangeBusiness(supplier);
                        break;
                    case "6":
                        ChangePhoneNumber(supplier);
                        break;
                    case "7":
                        ChangeEmail(supplier);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.\n");
                        break;
                }
            } while (updateOption != "0");
        }

        private void ChangeUsername(ModelSupplier supplier)
        {
            string newUsername;
            do
            {
                Console.Write("Podaj nową nazwę użytkownika: ");
                newUsername = Console.ReadLine();
            } while (!ValidationHelper.ValidateUsername(newUsername));
            supplier.Username = newUsername;

            SavingService.SaveSupplier(supplier);
        }

        private void ChangePassword(ModelSupplier supplier)
        {
            string newPassword;
            do
            {
                Console.Write("Podaj nowe hasło: ");
                newPassword = Console.ReadLine();
            } while (!ValidationHelper.ValidatePassword(newPassword));
            supplier.Password = newPassword;

            SavingService.SaveSupplier(supplier);
        }

        private void ChangeBusiness(ModelSupplier supplier)
        {
            string newBusiness;
            do
            {
                Console.Write("Podaj nową nazwę firmy: ");
                newBusiness = Console.ReadLine();
            } while (!ValidationHelper.ValidateText(newBusiness));
            supplier.Business = newBusiness;

            SavingService.SaveSupplier(supplier);
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

            SavingService.SaveSupplier(supplier);
        }

        private void ChangePhoneNumber(ModelSupplier supplier)
        {
            string newPhone;
            do
            {
                Console.Write("Podaj nowy numer telefonu: ");
                newPhone = Console.ReadLine();
            } while (!ValidationHelper.ValidatePhoneNumber(newPhone));
            supplier.PhoneNumber = newPhone;

            SavingService.SaveSupplier(supplier);
        }

        private void ChangeEmail(ModelSupplier supplier)
        {
            string newEmail;
            do
            {
                Console.Write("Podaj nowy email: ");
                newEmail = Console.ReadLine();
            } while (!ValidationHelper.ValidateEmail(newEmail));
            supplier.Email = newEmail;

            SavingService.SaveSupplier(supplier);
        }
    }
}
