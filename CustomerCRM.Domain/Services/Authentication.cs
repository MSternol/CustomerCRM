using CustomerCRM.Domain.Models;
using DataStorage.LoggApp;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.Domain.Services
{
    public class Authentication
    {
        public static RegistrationData AuthenticateUser(string filePath, string login, string password, string expectedPosition)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Nie znaleziono użytkownika.");
                    return null;
                }

                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if ((expectedPosition == "Administrator" && parts.Length >= 5) ||
                        (expectedPosition == "Klient" && parts.Length >= 12) ||
                        (expectedPosition == "Dostawca" && parts.Length >= 8))
                    {
                        string username = parts[0].Trim();
                        string userPassword = parts[1].Trim();
                        string position = parts[5];

                        if (username == login && userPassword == password)
                        {
                            Console.Clear();
                            Console.WriteLine($"==={expectedPosition}====" + "\n" + $"Zalogowany: {parts[2]} {parts[3]}");

                            if (expectedPosition == "Administrator")
                            {

                                if (parts.Length >= 6)
                                {
                                    // Tworzenie obiektu ModelRegisterAdmin
                                    return new ModelAdministrator(username, userPassword, parts[2], parts[3], parts[4], position, parts[6], parts[7]);
                                }
                                else
                                {
                                    Console.WriteLine("Administrator data is incomplete.");
                                }
                            }
                            else if (expectedPosition == "Klient")
                            {
                                if (parts.Length >= 12)
                                {
                                    // Tworzenie obiektu ModelRegisterCustomer
                                    return new ModelCustomer(username, userPassword, parts[2], parts[3], parts[4], position, parts[6], parts[7], parts[8], parts[9], parts[10], parts[11]);
                                }
                                else
                                {
                                    Console.WriteLine("Klient data is incomplete.");
                                }
                            }
                            else if (expectedPosition == "Dostawca")
                            {
                                if (parts.Length >= 8)
                                {
                                    return new ModelSupplier(username, userPassword, parts[2], parts[3], parts[4], position, parts[6], parts[7], parts[8]);
                                }
                                else
                                {
                                    Console.WriteLine("Dostawca data is incomplete.");
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"Nieprawidłowy login lub hasło.");
                return null;
            }
            catch (Exception ex)
            {
                string location = "Authentication.cs";
                LogToFileMessage.LogError($"Błąd: {ex.Message}", location);
                return null;
            }
        }
    }
}
