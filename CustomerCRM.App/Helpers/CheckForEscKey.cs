using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.App.Registration;

namespace CustomerCRM.App.Helpers
{
    public class CheckForEscKey
    {
        public static string ReadInput(ref bool registrationCancelled)
        {
            string input = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    registrationCancelled = true;
                    Console.WriteLine("\n" + "Przerwano rejestrację");
                    return null;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            } while (true);

            return input;
        }

    }
}
