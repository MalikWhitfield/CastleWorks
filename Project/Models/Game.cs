using System.Collections.Generic;
using capstone.Project.Interfaces;

namespace capstone.Project.Models
{
    public class Game
    {
        public List<IRoom> AllRooms { get; set; }
        public IRoom CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
    }
}