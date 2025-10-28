using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerCRM.App.Helpers
{
    public class ValidationHelper
    {
        private static bool registrationCancelled;

        public static bool ValidateUsername(string username)
        {

            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            if (username.Length < 4 || username.Length > 14)
            {
                return false;
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                return false;
            }
            if (!Regex.IsMatch(username, @"^[a-zA-Z]*[0-9]+[a-zA-Z0-9]*$"))
            {
                return false;
            }

            return true;
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < 8 || password.Length > 16)
            {
                return false;
            }

            int specialCharCount = 0;

            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    specialCharCount++;
                    if (specialCharCount > 1)
                    {
                        return false;
                    }
                }
            }

            if (specialCharCount == 0)
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }



        public static bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (char.IsLower(name[0]))
            {
                name = char.ToUpper(name[0]) + name.Substring(1);
            }

            if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$"))
            {
                return false;
            }
            return true;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            if (phoneNumber.Length != 9)
            {
                return false;
            }

            return true;
        }
        public static bool ValidateAddress(string address)
        {
            string[] parts = address.Split(' ');

            if (parts.Length < 2)
            {
                return false;
            }

            if (!IsAlpha(parts[0]))
            {
                return false;
            }
            if (!IsFraction(parts[1]))
            {
                return false;
            }

            return true;
        }
        public static bool IsAlpha(string input)
        {
            return input.All(char.IsLetter);
        }
        public static bool IsFraction(string input)
        {
            string[] fractionParts = input.Split('/');
            if (fractionParts.Length != 2)
            {
                return false;
            }

            return fractionParts[0].All(char.IsDigit) && fractionParts[1].All(char.IsDigit);
        }

        public static bool ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            if (Regex.IsMatch(text, @"[^a-zA-Z\s]"))
            {
                return false;
            }

            return true;
        }
        private static readonly string[] ValidPositions = { "MANAGER", "KIEROWNIK", "SPECJALISTA", "ASYSTENT", "ADMINISTRATOR" };
        private static bool AdministratorExists = false;
        public static bool IsPositionValid(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                return false;
            }

            position = position.ToUpper();

            position = Regex.Replace(position, "[^A-ZĄĆĘŁŃÓŚŹŻ]+", "");

            if (!ValidPositions.Contains(position))
            {
                return false;
            }

            if (position == "ADMINISTRATOR")
            {
                if (AdministratorExists)
                {
                    return false;
                }
                AdministratorExists = true;
            }

            return true;
        }

        public static bool ValidatePostalCode(string postalCode)
        {

            if (string.IsNullOrEmpty(postalCode) || !Regex.IsMatch(postalCode, @"^\d{2}-\d{3}$") || Regex.IsMatch(postalCode, @"[^0-9-]"))
            {
                return false;
            }

            return true;
        }
    }
}
