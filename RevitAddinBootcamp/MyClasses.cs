using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBootcamp
{
    // 4. create neighbourhood class
    public class Neighbourhood
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<Building> BuildingList { get; set; }
        public Neighbourhood(string _name, string _city, string _state, List<Building> _buildingList)
        {
            Name = _name;
            City = _city;
            State = _state;
            BuildingList = _buildingList;
        }

        public int GetBuildingCount()
        { 
            return BuildingList.Count; 
        }
    }

    // 1. create class
    public class Building
    {
        public string Name { get; set; }
        public string address { get; set; }
        public int NumberOffFloors { get; set; }
        public Double Area { get; set; }

        //3. Add constructor to class
        public Building(string _name, string _address, int _numberOffFloors, Double _area)
        {
            Name = _name;
            address = _address;
            NumberOffFloors = _numberOffFloors;
            Area = _area;
        }
    }
}
