using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
	/*
     * A class to parse certain files into the program
     */
	static class Parser
	{

		// A function to fetch a CSV file at a specific location and parse the CSV data, returning the contents
		public static List<PrimeMinister> ParseMinisters(string src)
		{
			// Create a list of Prime Minister objects to return
			List<PrimeMinister> ministers = new List<PrimeMinister>();
			// Append ".csv" to the file, if not already present
			if (!src.EndsWith(".csv"))
				src += ".csv";
			// Read all the data from the source file and store in a string[] of the lines
			string[] lines = File.ReadAllLines(src, Encoding.Default);
			// Iterate all lines and parse the data to a CSV file - skipping the first line as it is a header line
			for (int i = 1; i < lines.Length; i++)
			{
				// Get the string from the array and split into separate components at a comma delimeter
				string[] splitLine = lines[i].Split(',');
				// Parse and store the ID number
				if (!Byte.TryParse(splitLine[0], out byte id))
				{
					// If the ID failed to parse, output an error message and continue to the next loop
					Console.WriteLine("Failed to parse Prime Minister ID at line {0}: \"{1}\"", i, lines[i]);
					continue;
				}
				// Store the name
				string name = splitLine[1];
				// Create an array to parse and store dates
				DateTime[] dates = new DateTime[3];
				// Iterate dates and parse to store in new array
				for (int j = 2; j < splitLine.Length; j++)
				{
					// Parse the date and store in the new array
					dates[j - 2] = Parser.ParseDate(splitLine[j]);
				}
				// Create a Prime Minister from the generated data and append to the list
				ministers.Add(new PrimeMinister(id, name, dates[0], dates[1], dates[2]));
			}
			// Return the compiled list of Prime Ministers
			return ministers;
		}

		/*
         * Method to parse a date from a string and return a DateTime object
         */
		public static DateTime ParseDate(string input)
		{
			// If the date is incumbent, return the current date
			if (input.ToLower().Equals("incumbent"))
				return DateTime.Now;
			// Create a date variable to store the parsed time
			DateTime date = DateTime.Now;
			// Attempt to parse the date using nbsp characters between
			try
			{
				// Parse the date and store in the date variable
				date = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.CurrentCulture);
			}
			catch
			{
				// Output an error message to inform that the date could not be parsed
				Console.WriteLine("Failed to parse date: {0}", input);
			}
			// Return the resultant date
			return date;
		}

	}
}
