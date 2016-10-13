using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseAIM
{
    class Kid
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public Kid(string lastName, string firstName, string address, string city, string state, string zip, string phone)
        {
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Phone = phone;
        }
    }
}
