using System;

static class Program
{	
	static void Main()
	{
		Random rndm = new Random();
		Console.WriteLine("How many digits would you like the answer to be?");
		int digits = int.Parse(Console.ReadLine());
		string[] answer = new string[digits];
		int[] answerDC = new int[10];
		for(int i = 0; i < digits; i++)
		{		
			answer[i] = "" + rndm.Next(10);
			answerDC[int.Parse(answer[i])]++;
		}
		Console.WriteLine("How many tries would you like to have?");
		int triesLeft = int.Parse(Console.ReadLine());
		bool gameIsRunning = true;
		bool gameWon = false;
		while(gameIsRunning && triesLeft > 0)
		{
			gameIsRunning = false;
			int exactGuesses = 0;
			int correctNumbers = 0;
			Console.WriteLine("");
			Console.WriteLine("--------------------");
			Console.WriteLine("");
			Console.WriteLine("Tries left: " + triesLeft);
			Console.WriteLine("Please enter a(n) {0}-digit long guess.", digits);
			string str;
			bool validResponse;
			do
			{
				str = Console.ReadLine();
				bool containsInvalidChar = false;
				for(int i = 0; i < str.Length; i++)
				{
					string character = str.Substring(i, 1);
					switch(character)
					{
						case "0":
						case "1":
						case "2":
						case "3":
						case "4":
						case "5":
						case "6":
						case "7":
						case "8":
						case "9":
							break;
						default:
							containsInvalidChar = true;
							break;
					}
				}
				validResponse = true;
				if(str.Length != digits || containsInvalidChar)
				{
					validResponse = false;
					Console.WriteLine("That is not a valid response, please try again.");
				}
			}
			while(validResponse == false);
			triesLeft--;
			string[] guess = new string[digits];
			for(int i = 0; i < digits; i++)
			{
				guess[i] = str.Substring(i, 1);
			}
			int[] guessDC = new int[10];
			for(int x = 0; x < digits; x++){
				guessDC[int.Parse(guess[x])]++;
			}
			for(int n = 0; n < digits; n++)
			{
				if(guess[n] != answer[n])
				{
					gameIsRunning = true;
				} 
				else 
				{
					exactGuesses++;
					if(exactGuesses == digits)
					{
						gameWon = true;
					}
				}
			}
			if(gameIsRunning)
			{
				for(int i = 0; i < 10; i++)
				{
					if(guessDC[i] > answerDC[i])
					{
						correctNumbers += answerDC[i];
					}
					else if(guessDC[i] <= answerDC[i])
					{
						correctNumbers += guessDC[i];
					}
				}
				Console.WriteLine("Exact guesses: " + exactGuesses);
				Console.WriteLine("Correct numbers: " + correctNumbers);
			}
		}
		if(gameWon)
		{
			Console.WriteLine("Congratulations, you cracked the code!");
		}
		else
		{
			Console.WriteLine("You lost!  Try again!");
		}
	}
}