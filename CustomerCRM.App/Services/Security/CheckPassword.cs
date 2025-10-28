using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using CustomerCRM.Domain.Models;


namespace CustomerCRM.Domain.Services.Security
{
    public class CheckPassword
    {
        public bool CheckPasswordSecurity(string password, string HashedPassword, string Salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + Salt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                string hashedPasswordToCheck = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedPasswordToCheck == HashedPassword;
            }
        }
    }
}
