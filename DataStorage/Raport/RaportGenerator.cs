using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using DataStorage;
using System.Diagnostics;

namespace DataStorage.Raport
{
    public class ReportGenerator
    {
        private static string ReportDirectory => @"C:\Users\Mariu\OneDrive\Pulpit\CustomerCRM\CustomerCRM\DataStorage\Raport\ReportArchive\";

        public void GenerateReport()
        {
            Console.WriteLine("Wybierz rodzaj raportu:");
            Console.WriteLine("1. Indywidualne raporty");
            Console.WriteLine("2. Raport całkowity");
            Console.WriteLine("3. Raport błędów");
            Console.WriteLine("4. Raport sukcesów");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        GenerateIndividualReports();
                        break;
                    case 2:
                        GenerateTotalReport();
                        break;
                    case 3:
                        GenerateErrorReport();
                        break;
                    case 4:
                        GenerateSuccessReport();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }

        public static void GenerateIndividualReports()
        {
            Console.WriteLine("Wybierz rodzaj indywidualnego raportu:");
            Console.WriteLine("1. Administratorzy");
            Console.WriteLine("2. Klienci");
            Console.WriteLine("3. Dostawcy");
            Console.WriteLine("4. Magazyny");
            Console.WriteLine("5. Koszyki zakupowe");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                string reportName = "";
                string filePath = "";

                switch (choice)
                {
                    case 1:
                        reportName = "Administratorzy";
                        filePath = FileLocations.GetAdminFilePath();
                        break;
                    case 2:
                        reportName = "Klienci";
                        filePath = FileLocations.GetCustomerFilePath();
                        break;
                    case 3:
                        reportName = "Dostawcy";
                        filePath = FileLocations.GetSupplierFilePath();
                        break;
                    case 4:
                        reportName = "Magazyny";
                        filePath = FileLocations.GetWarehouseFilePath();
                        break;
                    case 5:
                        reportName = "Koszyki zakupowe";
                        filePath = FileLocations.GetShoppingCartFilePath();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        return;
                }
                Console.Clear();
                Console.WriteLine($"Generowanie raportu: {reportName}...");
                GenerateAndSaveReport(reportName, filePath);
                Console.WriteLine($"Raport {reportName} został wygenerowany i zapisany.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }


        public static void GenerateTotalReport()
        {
            Console.WriteLine("Generowanie raportu całkowitego...");

            var adminData = ReadFile(FileLocations.GetAdminFilePath());
            var customerData = ReadFile(FileLocations.GetCustomerFilePath());
            var supplierData = ReadFile(FileLocations.GetSupplierFilePath());
            var warehouseData = ReadFile(FileLocations.GetWarehouseFilePath());
            var shoppingCartData = ReadFile(FileLocations.GetShoppingCartFilePath());

            var reportData = new
            {
                Administratorzy = adminData,
                Klienci = customerData,
                Dostawcy = supplierData,
                Magazyny = warehouseData,
                KoszykiZakupowe = shoppingCartData
            };

            SaveReportToFile(reportData, ReportDirectory + "RaportCalkowity.json");

            Console.WriteLine("Raport całkowity został wygenerowany i zapisany.");
        }

        public static void GenerateErrorReport()
        {
            Console.WriteLine("Generowanie raportu błędów...");

            var errorData = ReadFile(FileLocations.GetLogErrorFilePath());

            var reportData = new
            {
                Bledy = errorData
            };

            SaveReportToFile(reportData, ReportDirectory + "RaportBledow.json");

            Console.WriteLine("Raport błędów został wygenerowany i zapisany.");
        }

        public static void GenerateSuccessReport()
        {
            Console.WriteLine("Generowanie raportu sukcesów...");

            var successData = ReadFile(FileLocations.GetLogSuccessFilePath());

            var reportData = new
            {
                Sukcesy = successData
            };

            SaveReportToFile(reportData, ReportDirectory + "RaportSukcesow.json");

            Console.WriteLine("Raport sukcesów został wygenerowany i zapisany.");
        }

        private static void GenerateAndSaveReport(string dataName, string filePath)
        {
            var data = ReadFile(filePath);
            var reportData = new { Data = dataName, Values = data };
            SaveReportToFile(reportData, ReportDirectory + $"{dataName}_raport.json");
        }

        private static List<string> ReadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return new List<string>(File.ReadAllLines(filePath));
                }
                else
                {
                    Console.WriteLine($"Plik nie istnieje: {filePath}");
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu pliku: {filePath}. Szczegóły: {ex.Message}");
                return new List<string>();
            }
        }

        private static void SaveReportToFile(object data, string fileName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(fileName, json);
                Console.WriteLine($"Raport został zapisany do pliku: {fileName}");

                Process.Start(new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu raportu do pliku: {fileName}. Szczegóły: {ex.Message}");
            }
        }

    }
}
