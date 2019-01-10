using System.Collections.Generic;
using capstone.Project.Interfaces;

namespace capstone.Project.Models
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, IRoom> Exits { get; set; }
        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, IRoom>();
        }
    }
}