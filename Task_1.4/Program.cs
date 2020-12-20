using System;
using Library;

namespace Task_1._4
{
	class Program
	{
		static void Main(string[] args)
		{
			Account[] accounts = new Account[20];
			for (int i = 0; i < accounts.Length; i++)
			{
				accounts[i] = new Account("UAH");
			}

			Account.GetSortedAccountsByQuickSort(accounts, 0, accounts.Length - 1);
			Console.WriteLine();

			Console.WriteLine("First ten accounts are:");
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(accounts[i].Id_Property);
			}
			Console.WriteLine("Last ten accounts are:");
			for (int i = accounts.Length - 1; i > accounts.Length - 11; i--)
			{
				Console.WriteLine(accounts[i].Id_Property);
			}
		}
	}
}
