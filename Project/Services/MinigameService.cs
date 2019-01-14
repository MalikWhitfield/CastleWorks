using System;
using System.Collections.Generic;
using capstone.Project.Services;

namespace capstone.Services
{
    public class MinigameService
    {
        public bool NumberGuess()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            int computerChoice;
            Console.WriteLine("A strange voice occurs from an unknown souce... But it sounds like... Mark, the Destroyer. Let us play a game, if you want into this room. Win or go back to 1st grade as a grown man. Billy Madison style.");
            Random rnd = new Random();
            computerChoice = rnd.Next(1, 101);
            Console.WriteLine("Guess a number between 1 and 100");
            int guess;
            bool won = false;
            int numberOfGuesses = 0;
            while (!won)
            {
                if (Int32.TryParse(Console.ReadLine(), out guess))
                {
                    if (guess == computerChoice)
                    {
                        Console.WriteLine("You Win!");
                        Console.WriteLine("The computer chose" + " " + computerChoice);
                        won = true;
                    }
                    else if (guess > computerChoice)
                    {
                        Console.WriteLine("Too high. Guess again.");
                        numberOfGuesses += 1;
                    }
                    else if (numberOfGuesses >= 8)
                    {
                        return false;

                    }
                    else
                    {
                        Console.WriteLine("Too low. Guess again.");
                        numberOfGuesses += 1;
                    }
                }

                else
                {
                    Console.WriteLine("Not a number. Guess with a number.");
                }
                if (numberOfGuesses == 8)
                {
                    System.Console.WriteLine("Too many attempts to guess the right number! Who do you work for, Coding Dojo?! You Die!");
                    // capstone.Project.Services.GameService.GetUserInput();
                    return false;
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            return true;
        }
        public bool Rps()
        {
            int computerChoice;
            bool won = false;
            int numGuess = 0;
            Dictionary<string, char> myDictionary = new Dictionary<string, char>();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("A strange voice occurs from the back of your own mind... HOW DID IT GET IN THERE? Who knows...? And the voice sounds like... D$! That voice says.... Let's play Rock Paper Scizz. Win or die. Elementary school rules");
            Console.WriteLine("Choose between rock, paper or scizz.");
            string playerChoice = Console.ReadLine();
            Random rnd = new Random();
            computerChoice = rnd.Next(1, 4);
            while (!won) //whenever you tie you are stuck inside of the while loop forever. i don't know why
            {
                if (
                    computerChoice == 1 && playerChoice == "rock" ||
                    computerChoice == 2 && playerChoice == "paper" ||
                    computerChoice == 3 && playerChoice == "scizz"
                    )
                {
                    Console.WriteLine($"Computer chose {playerChoice}, It is a tie. Play again.");
                    playerChoice = Console.ReadLine();
                    continue;
                }

                if (computerChoice == 1)
                {


                    if (playerChoice == "paper")
                    {
                        Console.WriteLine("The computer chose rock");
                        Console.WriteLine("You win! Winner Winner!");
                        return true;
                    }
                    else if (playerChoice == "scizz")
                    {
                        Console.WriteLine("The computer chose rock");
                        Console.WriteLine("You LOST! Your blades were crushed! Try again.");
                        playerChoice = Console.ReadLine();
                    }
                    else if (playerChoice == "scizz" && numGuess >= 2)
                    {
                        Console.WriteLine("The computer chose rock");
                        Console.WriteLine("You LOSE the entire game. If you can't beat D$ in rock paper scizz, you won't make it throught the game.");
                        System.Console.WriteLine("Play again? ( y / n)");
                        playerChoice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("That's not an option! Do you work for Coding Dojo, You don't understand rock, paper, scizz?");
                    }
                }
                else if (computerChoice == 2)
                {
                    if (playerChoice == "rock")
                    {
                        Console.WriteLine("The computer chose paper");
                        Console.WriteLine("YOU LOSE! You got smothered by paper! Try again!");
                        playerChoice = Console.ReadLine();
                    }
                    else if (playerChoice == "rock" && numGuess >= 3)
                    {
                        Console.WriteLine("The computer chose paper");
                        Console.WriteLine("You LOSE the entire game. If you can't beat D$ in rock paper scizz, you won't make it throught the game.");
                        System.Console.WriteLine("Play again? ( y / n)");
                        playerChoice = Console.ReadLine();
                    }

                    else if (playerChoice == "scizz")
                    {
                        Console.WriteLine("The computer chose paper.");
                        Console.WriteLine("YOU WIN! WINNER!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("That's not an option! Do you work for Coding Dojo, You don't understand rock, paper, scizz?");
                        playerChoice = Console.ReadLine();

                    }
                }
                else if (computerChoice == 3)
                {
                    if (playerChoice == "rock")
                    {
                        Console.WriteLine("The computar chose scizz");
                        Console.WriteLine("YOU WIN! WINNER!");
                        return true;
                    }
                    else if (playerChoice == "paper")
                    {
                        Console.WriteLine("The computer chose scizz.");
                        Console.WriteLine("You got cut open! You Lose! Try Again!");
                        playerChoice = Console.ReadLine();
                    }
                    else if (playerChoice == "paper")
                    {

                        Console.WriteLine("The computer chose scizz");
                        Console.WriteLine("You LOSE the entire game. If you can't beat D$ in rock paper scizz, you won't make it throught the game.");
                        System.Console.WriteLine("Play again? ( y / n)");
                        playerChoice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("That's not an option! Do you work for Coding Dojo, You don't understand rock, paper, scizz?");
                        playerChoice = Console.ReadLine();

                    }
                }
                numGuess++;
                if (numGuess == 2)
                {
                    System.Console.WriteLine("You lost rock, paper, scizz. You lose the game.");
                    System.Console.WriteLine("If you can't beat D$ in a classic game of rock paper scizz, there is no way you can get through this dungeon. You lose.");
                    // capstone.Project.Services.GameService.GetUserInput();
                    return false;

                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            return true;
        }
    }
}