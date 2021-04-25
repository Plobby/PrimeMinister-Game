using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

// Application namespace
namespace PMGame
{
	/*
     * Main program class
     */
	class Program
	{
        // Main method
        private static void Main(string[] args)
		{
			// Create a new instance of a game, with a path to the source file
			Game game = new Game("primeMinisters");
			// Boolean to store whether the user wishes to quit or not
			bool quit;
			// Loop while the user has not selected that they with to quit
			do
			{
				// Reset the game
				game.Reset();
				// Variable to store the type of game the user would like to play
				QuestionType type = QuestionType.UNSET;
				// Loop while the player has not decided on a gamemode
				do
				{
					// Clear the console and prompt for user input
					Console.Clear();
					Console.Write("Which game mode would you like to play?\n [1] Which Prime Minister served first?\n [2] Which Prime Minister served on this date?\n\nPlease enter \"1\" or \"2\": ");
					// Read the user input
					char c = Console.ReadKey().KeyChar;
					// Check if the user selected 1 or 2 and assign a game type accordingly
					if ('1'.Equals(c))
						type = QuestionType.SERVED_FIRST;
					else if ('2'.Equals(c))
						type = QuestionType.SERVING_AT;
					// Enter a blank line after user input
					Console.WriteLine();
				} while (QuestionType.UNSET.Equals(type));
				// Start the game
				game.Start(type);
				// Ask the user if they would like to play again
				Console.Write("Press any key to play again or \"Esc\" to quit...");
				// Get the users next key press
				ConsoleKey input = Console.ReadKey().Key;
				// Check if the user has pressed the escape key
				quit = (input == ConsoleKey.Escape);
			} while (!quit);
		}
	}
}
