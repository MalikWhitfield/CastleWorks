using System;
using System.Collections.Generic;
using capstone.Project.Interfaces;
using capstone.Services;

namespace capstone.Project.Models
{
    public class ChallengeRoom : IRoom
    {
        public delegate bool Challenge();
        public Challenge OnChallenge;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, IRoom> Exits { get; set; }

        //Creates a slot on the object that can be any given method as long as the signature matches my signature


        public ChallengeRoom(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, IRoom>();
        }

    }
}