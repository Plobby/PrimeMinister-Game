using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
	/*
     * Enum for a type of question
     */
	enum QuestionType
	{
		SERVED_FIRST,   // A question where the player is asked which of the Prime Ministers mentioned serves first
		SERVING_AT,     // A question where the player is asked who is serving at a particular date
		UNSET           // A question type holder for when no question type has been decided    
	}
}
