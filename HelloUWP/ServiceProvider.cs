using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseAIM
{
    public class ServiceProviderModel
    {
        public string ServiceType { get; }
        public string ProviderName { get; }
        public string Address { get; }
        public string Suite { get; }
        public string City { get; }
        public string State { get; }
        public int Zip { get; }
        public string Phone { get; }
        public string Fax { get; }
        public string Director { get; }
        public string ServicePopulation { get; }
        public string PaymentAccepted { get; }
        public string ServiceDescriptions { get; }
        public string Website { get; }
        public string Division { get; }


        public ServiceProviderModel(string serviceType, string providerName, string address, string suite, string city, string state, 
            int zip, string phone, string fax, string director, string servicePopulation, string paymentAccepted, string serviceDescriptions,
            string website, string division)
        {
            ServiceType = serviceType;
            ProviderName = providerName;
            Address = address;
            Suite = suite;
            City = city;
            State = state;
            Zip = zip;
            Phone = phone;
            Fax = fax;
            Director = director;
            ServicePopulation = servicePopulation;
            PaymentAccepted = paymentAccepted;
            ServiceDescriptions = serviceDescriptions;
            Website = website;
            Division = division;
        }

        } //end class
} //end NameSpace
