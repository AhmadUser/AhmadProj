using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Telephone { get; set; }
        public int AddressID { get; set; }
        public Customer()
        {

        }

        public Customer(string firstName, string lastName, string nickName, string telephone, int addreddID)
        {
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            Telephone = telephone;
            AddressID = addreddID;
        }

  

 
    }
}
