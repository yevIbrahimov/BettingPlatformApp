using System;
using Library;

namespace Task_1._2
{
	class Program
	{
		static void Main(string[] args)
		{
			Account[] accounts = new Account[10000000];
			for (int i = 0; i < accounts.Length; i++)
			{
				accounts[i] = new Account("UAH");
			}
			Account.GetSortedAccounts(accounts);
			Console.WriteLine("First ten accounts are:");
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(accounts[i].Id_Property);
			}
			Console.WriteLine("Last ten accounts are:");
			for (int i = accounts.Length-1; i > accounts.Length - 11; i--)
			{
				Console.WriteLine(accounts[i].Id_Property);
			}
		}
	}
}
