using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
	/*
     * Game class, to hold all the game logic and functionality
     */
	class Game
	{
		// Property for the player - read only
		public Player Player { get; } = new Player();
		// Property for the list of Prime Ministers - read only
		public List<PrimeMinister> Ministers { get; } = new List<PrimeMinister>();
		// Proprty for random number generation
		private Random Rnd { get; } = new Random();

		// Default constructor for a game instance
		public Game(string sourceFile)
		{
			do
			{
				try
				{
					// Load the list of Prime Ministers from the source file specified
					Ministers = Parser.ParseMinisters(sourceFile);
				}
				// File not found exception
				catch (FileNotFoundException ex)
				{
					Console.WriteLine("Could not find the file specified: \"{0}\"", sourceFile);
				}
				// General exception
				catch (Exception ex)
				{
					Console.WriteLine("A problem was encountered while loading the source file:\n {0}", ex.Message);
				}
				// Finally clause - always execute to make sure that the file has some data
				finally
				{
					if (Ministers.Count <= 0)
					{
						Console.Write("\nGame can't run without a valid prime minister source file.\nPlease enter a valid file source: ");
						sourceFile = Console.ReadLine();
					}
				}
			} while (Ministers.Count <= 0);
		}

		// Function to start the game
		public void Start(QuestionType gameType)
		{
			// Iterate 5 questions for "who served first"
			for (int i = 0; i < 5; i++)
			{
				// Update the players score based on whether they got the question correct or not
				Player.Score += NextQuestion(gameType) ? 1 : 0;
			}
			// Clear the console to output the player score
			Console.Clear();
			Console.WriteLine("You scored: {0}/5 points!\nWell done!\n", Player.Score);
		}

		// Function to reset the game and start from the beginning
		public void Reset()
		{
			// Reset the players score
			Player.Score = 0;
		}

		// Function to ask the next question and return if the player got it correct or not
		private bool NextQuestion(QuestionType type)
		{
			// Clear the console from any previous question
			Console.Clear();
			// Check the question type and choose a question
			if (QuestionType.SERVED_FIRST.Equals(type))
			{
				// Create an array to store the 3 chosen Prime Ministers
				PrimeMinister[] selected = new PrimeMinister[3];
				// Loop to select 3 Prime Ministers
				for (int i = 0; i < 3; i++)
				{
					// Create a variable to store the next Minister
					PrimeMinister nextSelected;
					// Loop while the Minister selected is not unique
					do
					{
						// Update the next selected minister
						nextSelected = Ministers[Rnd.Next(Ministers.Count)];
					} while (selected.Any(minister => (minister != null && minister.Name == nextSelected.Name)));
					// Push the next selected Minister into the array position
					selected[i] = nextSelected;
				}
				// Variable to determine if the user input is valid
				char choice = '0';
				// Loop while the users choice is not valid
				do
				{
					// Clear the console
					Console.Clear();
					// Prompt the user with the question
					Console.WriteLine("Which of the following Prime Ministers served first:");
					// Iterate through the Ministers and print them for the user to see
					for (int i = 0; i < 3; i++)
					{
						// Create a new string to store the output line
						string outLine = String.Format(" [{0}] {1}", (i + 1), selected[i].Name);
						// If the minister has multiple terms
						if (Ministers.Count(min => min.Name == selected[i].Name) > 1)
						{
							// Work out which term the selection is - this is done by counting any minister in the array with a matching naming and a lower ID, thus being an earlier term
							int term = Ministers.Count(min => min.Name == selected[i].Name && min.ID <= selected[i].ID);
							// If the term is not the 1st term, append it to the end of the output string
							outLine += String.Format(" ({0} term)", term.ToOrdinalString());
						}
						// Output the determined string
						Console.WriteLine(outLine);
					}
					// Prompt the user for an input
					Console.Write("\nPlease select either \"1\", \"2\" or \"3\": ");
					// Get the users input
					choice = Console.ReadKey().KeyChar;
				} while (!'1'.Equals(choice) && !'2'.Equals(choice) && !'3'.Equals(choice));
				// Write lines to console to space out the answer
				Console.WriteLine("\n");
				// Convert the selected value to an integer
				int userGuess = Int32.Parse(choice.ToString());
				// Get the selected Minister
				PrimeMinister userMinister = selected[userGuess - 1];
				// Get the earliest Minister using an LINQ aggregate function
				PrimeMinister correctMinister = selected.Aggregate((pm1, pm2) => pm1.Start < pm2.Start ? pm1 : pm2);
				// Check if the selections match
				if (userMinister.Equals(correctMinister))
				{
					// Inform the user that they got the question correct, as execution has not cancelled
					Console.WriteLine("That's correct! {0} served first on {1}!\n", userMinister.Name, userMinister.Start.ToLongDateString());
					// Wait for the user to press a key before the next question
					Console.Write("Press any key to continue...");
					Console.ReadKey();
					// Return true, as the user got the question correct
					return true;
				}
				else
				{
					// Inform the user that they got the question wrong
					Console.WriteLine("That's not correct! {0} served on {1}. {2} served before on {3}!\n", userMinister.Name, userMinister.Start.ToLongDateString(), correctMinister.Name, correctMinister.Start.ToLongDateString());
					// Wait for the user to press a key before the next question
					Console.Write("Press any key to continue...");
					Console.ReadKey();
					// Return false, as the user got the question wrong
					return false;
				}
			}
			else
			{
				// Create an array to store the 3 chosen Prime Ministers
				PrimeMinister[] selected = new PrimeMinister[3];
				// Loop to select 3 Prime Ministers
				for (int i = 0; i < 3; i++)
				{
					// Create a variable to store the next Minister
					PrimeMinister nextSelected;
					// Loop while the Minister selected is not unique
					do
					{
						// Update the next selected minister
						nextSelected = Ministers[Rnd.Next(Ministers.Count)];
					} while (selected.Any(minister => (minister != null && minister.Name == nextSelected.Name)));
					// Push the next selected Minister into the array position
					selected[i] = nextSelected;
				}
				// Variable to store the correct minister
				PrimeMinister correctMinister = selected[Rnd.Next(selected.Length)];
				// Generate a random numbers of days to add to the start date
				int addDays = (int)(Rnd.NextDouble() * (correctMinister.End - correctMinister.Start).TotalDays);
				// Create a new date and add the correct number of days
				DateTime guessDate = correctMinister.Start.AddDays(addDays);
				// Variable to determine if the user input is valid
				char choice = '0';
				// Loop while the users choice is not valid
				do
				{
					// Clear the console
					Console.Clear();
					// Prompt the user with the question
					Console.WriteLine("Which of the following Prime Ministers was serving on {0}:", guessDate.ToLongDateString());
					// Iterate through the Ministers and print them for the user to see
					for (int i = 0; i < 3; i++)
						Console.WriteLine(" [{0}] {1}", (i + 1), selected[i].Name);
					// Prompt the user for an input
					Console.Write("Please select either \"1\", \"2\" or \"3\": ");
					// Get the users input
					choice = Console.ReadKey().KeyChar;
				} while (!'1'.Equals(choice) && !'2'.Equals(choice) && !'3'.Equals(choice));
				// Write lines to console to space out the answer
				Console.WriteLine("\n");
				// Convert the selected value to an integer
				int userGuess = Int32.Parse(choice.ToString());
				// Get the selected Minister
				PrimeMinister userMinister = selected[userGuess - 1];
				// Check if the user selected the correct Minister
				if (userMinister.Equals(correctMinister))
				{
					// Inform the user that they got the question correct
					Console.WriteLine("That's correct! {0} served from {1} to {2}!\n", userMinister.Name, userMinister.Start.ToLongDateString(), userMinister.End.ToLongDateString());
					// Wait for the user to press a key before the next question
					Console.Write("Press any key to continue...");
					Console.ReadKey();
					// Return true, as the user got the question correct
					return true;
				}
				else
				{
					// Inform the user that they got the question wrong
					Console.WriteLine("That's not correct! {0} served from {1} to {2}!\nThe correct answer was {3}, who served from {4} to {5}!\n", userMinister.Name, userMinister.Start.ToLongDateString(), userMinister.End.ToLongDateString(), correctMinister.Name, correctMinister.Start.ToLongDateString(), correctMinister.End.ToLongDateString());
					// Wait for the user to press a key before the next question
					Console.Write("Press any key to continue...");
					Console.ReadKey();
					// Return false, as the user got the question wrong
					return false;
				}
			}
		}

	}
}
