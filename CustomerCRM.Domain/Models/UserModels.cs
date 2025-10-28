using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.Domain.Models
{
    public class RegistrationData
    {
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public string ID { get; set; }
        public string Role { get; set; }

        public RegistrationData(string username, string password, string email, string id, string role)
        {
            Username = username;
            Password = password;
            Email = email;
            ID = id;
            Role = role;
        }

        //konstruktor bezparametrowy
        protected RegistrationData()
        {
        }
    }

    public class ModelCustomer : RegistrationData
    {
        public new string Username
        {
            get { return base.Username; }
            set { base.Username = value; }
        }

        public new string Password
        {
            get { return base.Password; }
            set { base.Password = value; }
        }

        public new string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountType { get; set; }

        public ModelCustomer(string username, string password, string firstName, string lastName, string country, string city, string address, string postalCode, string phoneNumber, string email, string id, string role)
            : base(username, password, email, id, role)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Address = address;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            AccountType = role;
        }

        // konstruktor bezparametrowy
        public ModelCustomer() : base()
        {
        }
    }

    public class ModelSupplier : RegistrationData
    {
        public new string Username
        {
            get { return base.Username; }
            set { base.Username = value; }
        }

        public new string Password
        {
            get { return base.Password; }
            set { base.Password = value; }
        }

        public new string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Business { get; set; }

        public ModelSupplier(string username, string password, string firstName, string lastName, string email, string phoneNumber, string business, string id, string role)
            : base(username, password, email, id, role)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Business = business;
        }

        // konstruktor bezparametrowy
        public ModelSupplier() : base()
        {
        }
    }

    public class ModelAdministrator : RegistrationData
    {
        public new string Username
        {
            get { return base.Username; }
            set { base.Username = value; }
        }

        public new string Password
        {
            get { return base.Password; }
            set { base.Password = value; }
        }

        public new string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        public ModelAdministrator(string username, string password, string email, string firstName, string lastName, string position, string id, string role)
            : base(username, password, email, id, role)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = position;
        }

        // konstruktor bezparametrowy
        public ModelAdministrator() : base()
        {
        }
    }
}
