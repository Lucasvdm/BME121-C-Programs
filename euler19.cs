using System;

static class Program
{
	static void Main()
	{
		int year = 1901;
		int month = 1;
		int dayOfWeek = 2;
		int sundayCount = 0;
		while(year < 2001)
		{
			if(dayOfWeek == 7)
				sundayCount++;
			if(month == 4 || month == 6 || month == 9 || month == 11) //April, June, September, November
			{
				for(int i = 0; i < 30; i++)
				{
					dayOfWeek++;
					if(dayOfWeek == 8)
						dayOfWeek = 1;
					
				}
			}
			else if(month != 2) //Everything else except for February
			{
				for(int i = 0; i < 31; i++)
				{
					dayOfWeek++;
					if(dayOfWeek == 8)
						dayOfWeek = 1;
				}
			}
			else if(year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) //February on a leap year
			{
				for(int i = 0; i < 29; i++)
				{
					dayOfWeek++;
					if(dayOfWeek == 8)
						dayOfWeek = 1;
				}
			}
			else //February on a normal year
			{
				for(int i = 0; i < 28; i++)
				{
					dayOfWeek++;
					if(dayOfWeek == 8)
						dayOfWeek = 1;
				}
			}
			month++;
			if(month == 13)
			{
				month = 1;
				year++;
			}
		}
		Console.WriteLine(sundayCount);
	}
}