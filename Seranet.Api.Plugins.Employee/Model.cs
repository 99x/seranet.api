using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seranet.Api.Plugins.Employee
{
    public class Employee
    {
        public Employee() {
            this.Addresses = new List<Address>();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NIC { get; set; }
        public string DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string TwitterId { get; set; }
        public string FacebookId { get; set; }
        public string LinkedInId { get; set; }
        public List<Address>  Addresses { get; set; }
    }

    public enum AddressType { 
        Permanent,
        Residential,
        Preferential
    }


    public class Coordinate {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Address {

        public Address(AddressType addressType) {
            this.Type = addressType;
            this.Coordinate = new Coordinate();
        }
        public AddressType Type { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string  Country { get; set; }
        public Coordinate Coordinate { get; set; }
        public string Phone { get; set; }
    }


}
