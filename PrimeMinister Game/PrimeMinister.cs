using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMGame
{
	/*
     * Class for the data for a specific Prime Minister
     */
	class PrimeMinister
	{
		// Property for the ID of the Prime Minister
		public byte ID { get; }
		// Property to store the name of the Prime Minister
		public string Name { get; }
		// Property to store the birth date
		public DateTime Birth { get; }
		// Property to store the start date
		public DateTime Start { get; }
		// Property to store the end date
		public DateTime End { get; }


		// Constructor for a Prime Minister instance
		public PrimeMinister(byte id, string name, DateTime birth, DateTime start, DateTime end)
		{
			// Assign the ID
			ID = id;
			// Assign the instance name
			Name = name;
			// Assign the instance dates
			Birth = birth;
			Start = start;
			End = end;
		}

		// Override method for ToString
		public override string ToString()
		{
			// Return the string formatted with the object properties
			return String.Format($"ID: {ID}\nName: {Name}\nBirth: {Birth.ToShortDateString()}\nStart: {Start.ToShortDateString()}\nEnd: {End.ToShortDateString()}");
		}

	}
}
