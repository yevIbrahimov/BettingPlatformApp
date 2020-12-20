using System;
using Library;

namespace Task_2._1
{
	class Program
	{
		static void Main(string[] args)
		{
			BetService betService = new BetService();
			for (int i = 0; i < 10; i++)
			{
				betService.GetOdds();
			}
			Console.WriteLine("I’ve bet 100 USD with the odd {0} and I’ve earned {1}", betService.Odd_Property, betService.Bet(100));
			int count = 0;
			do
			{
				betService.GetOdds();
				if(betService.Odd_Property > 12)
				{
					count++;
					Console.WriteLine("I’ve bet 100 USD with the odd {0} and I’ve earned {1}", betService.Odd_Property, betService.Bet(100));
				}
			} 
			while (count<3);
			decimal balance = 10000;
			do
			{
				betService.GetOdds();
				if (betService.Odd_Property > 5) // i choose coef = 5
				{
					if (betService.IsWon())
					{
						balance += 500 * betService.Odd_Property; // i bet 500$

					}
					else
					{
						balance -= 500; // i bet 500$
						if(balance<0)
						{
							balance = 0;
						}
					}
				}
			} 
			while (balance < 150000 && balance > 0);
			Console.WriteLine("My balance is {0}", balance);
		}
	}
}
