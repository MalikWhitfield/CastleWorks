using System.Collections.Generic;
using capstone.Project.Interfaces;
using capstone.Project.Models;
using capstone.Project.Functions;
using System;

namespace capstone.Project.Services
{
    public class GameService : IGameService
    {
        public Game Game { get; set; }
        public bool Playing { get; set; }

        //Initializes the game, creates rooms, their exits, and add items to rooms
        public void Setup()
        {
            Game = GameSetup.Setup(new Game());
            Help();
            StartGame();
        }

        //Restarts the game 
        public void Reset()
        {
            Playing = true;
            Setup();
            StartGame();
        }

        //Setup and Starts the Game loop
        public void StartGame()
        {
            Playing = true;
            while (Playing)
            {
                // Console.Clear();
                // System.Console.WriteLine(Game.CurrentRoom.Description);
                GetUserInput();
            }
        }

        //Gets the user input and calls the appropriate command
        public void GetUserInput()
        {
            System.Console.WriteLine("What are you going to do?");
            string input = Console.ReadLine();
            string[] inputArr = input.Split(" ");
            string command = inputArr[0];
            string value = "";
            if (inputArr.Length > 1)
            {
                value = inputArr[1];
            }
            // else
            // {
            //     System.Console.WriteLine("That is not a valid input, try again.");
            // }

            switch (command)
            {
                case "quit":
                    Quit();
                    break;
                case "go":
                    Go(value);
                    break;
                case "look":
                    Look();
                    break;
                case "use":
                    UseItem(value);
                    break;
                case "take":
                    TakeItem(value);
                    break;
                case "inventory":
                    Inventory();
                    break;
                case "help":
                    Help();
                    break;
                case "poop":
                    System.Console.WriteLine("You feel lighter, yet the room is smelly and I don't see any toilet paper in your inventory...");
                    break;
                default:
                    System.Console.WriteLine("That is not a valid command.");
                    break;
            }


        }

        #region Console Commands

        //Stops the application
        public void Quit()
        {
            Playing = false;
            Console.Clear();
        }

        //Should display a list of commands to the console
        public void Help()
        {
            System.Console.WriteLine($"Fear not, brave {Game.CurrentPlayer.PlayerName}! To navigate through this dungeon, simply type 'go' and the direction you would like to go to. For example: 'go north'");
            System.Console.WriteLine("To look around the room you are in, type 'look'.");
            System.Console.WriteLine("To take an item from the room that you are in, type 'take' and then the item name. Example: 'take resume'");
            System.Console.WriteLine("To use an item, type 'use' then the name of the item. For example: 'use resume'.");
            System.Console.WriteLine("To quit, type 'quit'.");
            System.Console.WriteLine("To recieve help, type 'help'.");
            System.Console.WriteLine("To check your inventory, type 'inventory'.");
            System.Console.WriteLine("To reset, type 'reset.'");
        }

        //Validate CurrentRoom.Exits contains the desired direction
        //if it does change the CurrentRoom
        public void Go(string direction)
        {
            if (Game.CurrentRoom.Exits.ContainsKey(direction))
            {
                Console.Clear();
                IRoom nextRoom = Game.CurrentRoom.Exits[direction];
                if (nextRoom is ChallengeRoom)
                {
                    ChallengeRoom h = (ChallengeRoom)nextRoom;
                    Console.Clear();
                    System.Console.WriteLine(nextRoom.Description);
                    if (!h.OnChallenge())
                    {
                        Quit();
                    }
                }
                Game.CurrentRoom = Game.CurrentRoom.Exits[direction];
            }
        }

        //When taking an item be sure the item is in the current room 
        //before adding it to the player inventory, Also don't forget to 
        //remove the item from the room it was picked up in
        public void TakeItem(string itemName)
        {
            Item item = Game.CurrentRoom.Items.Find(i =>
            {
                return i.Name.ToLower() == itemName;
            });
            if (item != null)
            {
                Game.CurrentRoom.Items.Remove(item);
                Game.CurrentPlayer.Inventory.Add(item);
                System.Console.WriteLine($"Added {item.Name} to your Inventory.");
                Inventory();
            }
            else
            {
                System.Console.WriteLine("There is nothing to take. Besides oxygen.");
            }
            // System.Console.WriteLine("Your current Inventory");
        }

        //No need to Pass a room since Items can only be used in the CurrentRoom
        //Make sure you validate the item is in the room or player inventory before
        //being able to use the item
        public void UseItem(string itemName)
        {

        }

        //Print the list of items in the players inventory to the console
        public void Inventory()
        {
            foreach (Item item in Game.CurrentPlayer.Inventory)
            {
                System.Console.WriteLine($"{item.Name}");
            }
        }

        //Display the CurrentRoom Description, Exits, and Items
        public void Look()
        {
            System.Console.WriteLine($"You are in the {Game.CurrentRoom.Name}");
            System.Console.WriteLine($"{Game.CurrentRoom.Description}");
            System.Console.WriteLine("The items in this room are: ");
            foreach (Item item in Game.CurrentRoom.Items)
            {
                System.Console.WriteLine($"{item.Name}");
            }
        }

        #endregion
    }
}