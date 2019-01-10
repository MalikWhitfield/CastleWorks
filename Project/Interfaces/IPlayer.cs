using System.Collections.Generic;
using capstone.Project.Models;

namespace capstone.Project.Interfaces
{
    public interface IPlayer
    {
        string PlayerName { get; set; }
        List<Item> Inventory { get; set; }
    }
}