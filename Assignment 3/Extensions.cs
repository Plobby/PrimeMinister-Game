using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMGame
{
	/*
     * A class to hold extensions methods
     */
	static class Extensions
	{

		// Extension method to int to convert to an ordinal string (1st, 2nd, 3rd, etc)
		public static string ToOrdinalString(this int num)
		{
			// If the number is less than or equal to 0, it has no ordinal equivelant - return the original number
			if (num <= 0)
				return num.ToString();
			// Switch the number modulus 100, to account for the special cases of 11th, 12th and 13th being "th" and not "st", "nd" and "rd" respectively
			switch (num % 100)
			{
				case 11:
				case 12:
				case 13:
					return num + "th";
			}
			// Switch the number modulus 10, to account for 1st, 2nd and 3rd. Any other should be "th"
			switch (num % 10)
			{
				case 1:
					return num + "st";
				case 2:
					return num + "nd";
				case 3:
					return num + "rd";
				default:
					return num + "th";
			}
		}

	}
}
