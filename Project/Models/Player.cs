using System.Collections.Generic;
using capstone.Project.Interfaces;

namespace capstone.Project.Models
{
    public class Player : IPlayer
    {
        //CONstructor
        public Player(string name)
        {
            PlayerName = name;
            Inventory = new List<Item>();
        }

        public string PlayerName { get; set; }
        public List<Item> Inventory { get; set; }
    }
}