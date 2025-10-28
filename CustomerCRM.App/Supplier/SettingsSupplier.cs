using CustomerCRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Supplier
{
    internal class SettingsSupplier
    {
        public void Settings()
        {
            DisplayPersonalDataMenu();
        }
        private static void DisplayPersonalDataMenu()
        {
            Console.WriteLine("=== Zarządzanie Danymi Osobowymi ===");
            Console.WriteLine("1. Zmień nazwę użytkownika");
            Console.WriteLine("2. Zmień hasło");
            Console.WriteLine("3. Zmień imię");
            Console.WriteLine("4. Zmień nazwisko");
            Console.WriteLine("5. Zmień email");
            Console.WriteLine("6. Zmień numer telefonu");
            Console.WriteLine("7. Wyświetl dane personalne");
            Console.WriteLine("0. Wróć do menu głównego");
            Console.Write("Wybierz opcję: ");
            SupplierSettingServices settingServices = new SupplierSettingServices();
            settingServices.service();
        }
    }
}
