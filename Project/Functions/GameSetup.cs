using capstone.Project.Models;
using capstone.Project.Interfaces;
using System;
using capstone.Services;
using capstone.Project.Services;

namespace capstone.Project.Functions

{
    public static class GameSetup
    {

        public static Game Setup(Game game)
        {
            MinigameService mgs = new MinigameService();
            System.Console.WriteLine("Welcome to Boise CodeWorks, or should I say: 'Welcome to your future, should you survive.'");
            System.Console.Write("What is your name? : ");
            string input = System.Console.ReadLine();
            game.CurrentPlayer = new Player(input);
            #region //ITEMS
            Item bowserCard = new Item("BowserCard", "This card is from an ancient game played to distract the ancestors from class, although it only resulted in turmoil and the disappointment of one named 'Malik'... It changes any level's value to 0, whatever that means.");
            Item goalPole = new Item("GoalPole", "This card is from an ancient game played to distract the ancestors from class, although it only resulted in turmoil and the disappointment of one named 'Malik'... It seems this card brings your level to 10, whatever that is.");
            Item oneUp = new Item("1Up", "This card is from an ancient game played to distract the ancestors from class, although it only resulted in turmoil and the disappointment of one named 'Malik'... It seems this card allows you to steal another player's life. But only jerks would do that. Unless Malik did it, then it's reasonable.");
            Item lavaBubble = new Item("LavaBubble", "This card is from an ancient game played to distract the ancestors from class, although it only resulted in turmoil and the disappointment of one named 'Malik'... It seems as though this card allows its user to destroy the life of another player. But only a mondo jerk would do thta. Unless Malik did it, then that's just a great move.");
            Item batPoo = new Item("BatPoo", "This is just bat poo, although it is smelly, it may help you.");
            Item resume = new Item("Resume", "This is your old resume! It may need to be updated.");
            Item trueResume = new Item("TrueResume", "Your resume has reached its final form. Now you can take on the ugly, frightening, JOB HUNT!!!!");
            #endregion

            #region //ROOM CREATION
            Room southRoom = new Room("South Room", "There is an old, dusty resume on the ground, with your name on it, you should probably pick that up. There is also a sign on the wall written in ancient English. It translates to 'Chicago Bears'");
            Room westRoom = new Room("West Room", "There is no one in this room, however there is a table in the middle, surrounded by a couch and a couple chairs.");
            Room centralRoom = new Room("Central Room", "In this room, you can see Felix in the corner, playing Mario with the usual gang, minus one person. Upon seeing you, Felix takes your resume and puts your picture on it, stating 'It's not a resume without a selfie.'");
            Room eastRoom = new Room("East Room", "In this room you see Kasandra and Brittany. Kasandra somehow has 8 cards in her hand that have castles on them. In her other hand she has four life tokens, clearly cheating. Brittany asks to see your resume.");
            Room northRoom = new Room("North Room", "In this room awaits the 4 horsemen of death and their valiant steed. Mark asks: 'Would you like to play a quick game of Dungeons and Dragons?");
            ChallengeRoom rpsChall = new ChallengeRoom("Rps Hall", "This a just an empty hallway. Well maybe it's not empty, because you are inside? Unless you yourself are empty inside... In which case, may your soul find peace.");
            ChallengeRoom numsChall = new ChallengeRoom("NumGuess Hall", "This hallway is not empty. No space is empty if you are in it. :) ");
            #endregion
            #region //ROOM EXITS AND CONNECTIONS
            rpsChall.OnChallenge += mgs.Rps;
            numsChall.OnChallenge += mgs.NumberGuess;
            southRoom.Exits.Add("north", rpsChall);
            rpsChall.Exits.Add("north", centralRoom);
            numsChall.Exits.Add("west", westRoom);
            centralRoom.Exits.Add("west", numsChall);
            centralRoom.Exits.Add("east", eastRoom);
            centralRoom.Exits.Add("north", northRoom);
            centralRoom.Exits.Add("south", southRoom);
            westRoom.Exits.Add("east", centralRoom);
            eastRoom.Exits.Add("west", centralRoom);
            northRoom.Exits.Add("south", centralRoom);
            #endregion

            #region //Adding ITEMS To ROOMS
            southRoom.Items.Add(batPoo);
            southRoom.Items.Add(resume);
            westRoom.Items.Add(bowserCard);
            westRoom.Items.Add(goalPole);
            westRoom.Items.Add(oneUp);
            westRoom.Items.Add(lavaBubble);
            westRoom.Items.Add(trueResume);
            #endregion
            game.CurrentRoom = southRoom;
            return game;
        }
    }

}