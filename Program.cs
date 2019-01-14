using System;
using capstone.Project.Services;

namespace capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            GameService gameService = new GameService();
            gameService.Setup();

        }
    }
}
