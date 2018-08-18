using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class RegionDataViewModel
    {
        public Dictionary<int,String> Universities { get; set; }
    }

    public class RegionData
    {
        public List<University> Universities { get; set; }
    }

    public class University
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<College> Colleges { get; set; }
    };

    public class College
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Building> Buildings { get; set; }
    }

    public class Building
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; }
    }

    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ParentDataID { get; set; }
        public int DataType { get; set; }
        public bool IsDanger { get; set; }
    }
}