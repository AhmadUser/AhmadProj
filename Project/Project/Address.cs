using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Address
    {
        public string StreetName { get; set; }
        public string BuildingName { get; set; }
        public int Floor { get; set; }
        public string NearSomePlace { get; set; }
        public string MoreDetails { get; set; }

        public Address(string streetName, string buildingName, int floor, string nearSomePlace, string moreDetails)
        {
            StreetName = streetName;
            BuildingName = buildingName;
            Floor = floor;
            NearSomePlace = nearSomePlace;
            MoreDetails = moreDetails;
        }
    }
}
