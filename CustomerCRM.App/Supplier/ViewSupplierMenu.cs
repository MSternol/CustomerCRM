using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.Domain.Models;


namespace CustomerCRM.App.Supplier
{
    internal class ViewSupplierMenu
    {
        private ModelCustomer user;
        private bool isRunning = true;

        public bool ViewMenu(RegistrationData registrationData)
        {
            while (isRunning)
            {
                Console.WriteLine("1.Zarządzanie Produktami");
                Console.WriteLine("2.Ustawienia");
                Console.WriteLine("0.Wyjście");
                var operation = Console.ReadLine();
                isRunning = ViewMenuSupplier(operation);
            }

            return isRunning;
        }

        private bool ViewMenuSupplier(string operation)
        {
            switch (operation)
            {
                case "1":
                    Console.Clear();
                    WarehouseApp.WarehouseViewApp manageInventory = new WarehouseApp.WarehouseViewApp(user);
                    manageInventory.WarehouseView();
                    break;

                case "2":
                    Console.Clear();
                    SettingsSupplier settingsSupplier = new SettingsSupplier();
                    settingsSupplier.Settings();
                    break;

                case "0":
                    Console.Clear();
                    return false; // Przerywa pętlę w przypadku wyboru "0. Wyjście"

                default:
                    Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie!!");
                    break;
            }

            return true; // Kontynuuje pętlę, chyba że wybrano "0. Wyjście"
        }
    }
}
