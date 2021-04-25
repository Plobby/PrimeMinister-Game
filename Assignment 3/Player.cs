using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
	/*
	 * Player class, intended to hold the score for the current player
	 */
	class Player
	{
		// Property for the players current score
		public int Score { get; set; }

		// Default constructor for a player instance
		public Player()
		{
			// Set the score to 0
			Score = 0;
		}

	}
}
