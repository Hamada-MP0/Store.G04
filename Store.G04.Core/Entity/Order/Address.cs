using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity.Order
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string lastName , string firstName, string city, string country, string street)
        {
            LastName = lastName;
            FirstName = firstName;
            City = city;
            Country = country;
            Street = street;
        }   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }

      
    }
}
