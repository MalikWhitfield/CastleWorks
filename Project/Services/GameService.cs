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
            Game = GameSetup.Setup();
            Console.BackgroundColor = ConsoleColor.Black;
            System.Console.WriteLine("You awake in a dimly lit room...");
            StartGame();
        }

        //Restarts the game 
        public void Reset()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
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
                case "reset":
                    Reset();
                    break;
                case "y":
                    Reset();
                    break;
                case "n":
                    Quit();
                    break;
                case "poop":
                    System.Console.WriteLine("You feel lighter, yet the room is smelly and I don't see any toilet paper in your inventory...");
                    break;

                default:
                    System.Console.WriteLine("That is not a valid command.");
                    break;
            }


        }


        //Stops the application
        public void Quit()
        {
            Playing = false;
            Console.BackgroundColor = ConsoleColor.Black;
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
                    System.Console.WriteLine(h.Description);
                    if (!h.OnChallenge())
                    {
                        while (true)
                        {
                            System.Console.WriteLine("You failed the challenge!! Would you like to play again? ( y / n )");
                            string input = Console.ReadLine();
                            if (input == "y")
                            {
                                Reset();
                            }
                            else
                            {
                                Quit();
                                break;
                            }

                            // if (Game.CurrentRoom.Name == "East Room")
                            // {
                            //     System.Console.WriteLine("Kasandra says: 'WHAT?! How do you have all those cards?! You must be cheating! You lose!");
                            //     System.Console.WriteLine("Play again? ( y / n )");
                            //     string input = Console.ReadLine();
                            //     if (input == "y")
                            //     {
                            //         Reset();
                            //     }
                            //     else
                            //     {
                            //         Quit();
                            //     }
                            // }



                        }
                    }
                }
                Game.CurrentRoom = nextRoom;
                System.Console.WriteLine($"You are in the {Game.CurrentRoom.Name}");
            }
            else
            {
                System.Console.WriteLine("You walk right into a wall.");
            }
            Console.BackgroundColor = ConsoleColor.Black;

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
            Item item = Game.CurrentPlayer.Inventory.Find(Item => Item.Name.ToLower() == itemName);
            if (itemName == "batpoo")
            {
                System.Console.WriteLine("You used the BatPoo?? WHY?! Why would you even pick up this item?");
                System.Console.WriteLine("Just for that, you lost the game, weirdo. Would you like to play again? ( y / n)");
                GetUserInput();
            }

            if (itemName == "trueresume" && Game.CurrentRoom.Name.ToString() == "North Room")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("Jake says: 'Haha! So you've got your True Resume... It looks good, but can you pass the final challenge?");
                System.Console.WriteLine("Zach says: 'Say the magic phrase'");
                string input = Console.ReadLine().ToLower();
                if (input == "go bears")
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    System.Console.WriteLine("Congratulations, you won! Go get that job!");
                    System.Console.WriteLine("Type 'quit' to leave the game or 'reset' to do it all again.");
                }
                else
                {
                    System.Console.WriteLine("Porter says: You made it this far and still couldn't figure what this was all about... you lose");
                    System.Console.WriteLine("Continue playing? ( y / n)");
                    GetUserInput();
                }
            }
            #region //FOR NO USE CARDS
            if (itemName == "bowsercard")
            {
                System.Console.WriteLine("Using this card individually doesn't do much, however it does seem necessary to have in your possession.");
            }
            if (itemName == "oneup")
            {
                System.Console.WriteLine("Using this card individually doesn't do much, however it does seem necessary to have in your possession.");
            }
            if (itemName == "lavabubble")
            {
                System.Console.WriteLine("Using this card individually doesn't do much, however it does seem necessary to have in your possession.");
            }
            if (itemName == "goalpole")
            {
                System.Console.WriteLine("Using this card individually doesn't do much, however it does seem necessary to have in your possession.");
            }
            #endregion
            if (itemName == "resume" && Game.CurrentRoom.Name.ToString() == "East Room")
            {
                System.Console.WriteLine("Brittany says: Is this a selfie on your resume! You know better than that! You lose!");
                System.Console.WriteLine("Play again? ( y / n) ");
                GetUserInput();
            }
            if (itemName == "resume" && Game.CurrentRoom.Name.ToString() == "North Room")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("Jake says: You think you can beat me with that old, dusty resume?! The job market will destroy you!");
                System.Console.WriteLine("Play again? ( y / n) ");
                GetUserInput();
            }
            if (itemName == "resume" && Game.CurrentRoom.Name.ToString() != "East Room")
            {
                System.Console.WriteLine("Using that does nothing here.");
                GetUserInput();
            }
            if (itemName == "trueresume" && Game.CurrentRoom.Name.ToString() == "East Room")
            {
                System.Console.WriteLine("Brittany says: 'That's a great looking resume! You're gonna do great out there! :)");
                GetUserInput();
            }

        }

        //Print the list of items in the players inventory to the console
        public void Inventory()
        {
            foreach (Item item in Game.CurrentPlayer.Inventory)
            {
                System.Console.WriteLine($"{item.Name} - {item.Description}");
            }
        }

        //Display the CurrentRoom Description, Exits, and Items
        public void Look()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            System.Console.WriteLine($"You are in the {Game.CurrentRoom.Name}");
            System.Console.WriteLine($"{Game.CurrentRoom.Description}");
            System.Console.WriteLine("The items in this room are: ");
            foreach (Item item in Game.CurrentRoom.Items)
            {
                System.Console.WriteLine($"{item.Name}");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        // public bool DeathCheck()
        // {
        //     Console.WriteLine("Game: " + Game);
        //     Console.WriteLine("Player: " + Game.CurrentPlayer);
        //     List<Item> deathItems = new List<Item>();
        //     foreach (var item in Game.CurrentPlayer.Inventory)
        //     {
        //         if (item.Name == "BowserCard" || item.Name == "GoalPole" || item.Name == "OneUp" || item.Name == "LavaBubble")
        //         {
        //             deathItems.Add(item);
        //         }
        //     }

        //     return true;
        // }
    }

}

