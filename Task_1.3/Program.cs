using System;
using Library;
namespace Task_1._3
{
	class Program
	{
		static Account[] accounts = new Account[20]; 

		public static void GetAccount(int Id)
		{
			int count = 0;
			int index = BinarySearch(Id, out count);

			if (index == -1)
			{
				Console.WriteLine("There is no account {0} in the list",Id);
			} else
			{
				Console.WriteLine("{0} was found at index {1} by {2} tries", Id, index, count);
			}
		}
		public static int BinarySearch (int Id, out int count)
		{
			int left = 0;
			int right = accounts.Length;
			int middle = 0;
			count = 0;

			while (left <= right)
			{
				count++;
				middle = (left + right) / 2;

				if (Id == accounts[middle].Id_Property)
				{
					int index = middle;
					return index;
				} else if (Id < accounts[middle].Id_Property)
				{
					right = middle - 1;
				} else
				{
					left = middle + 1;
				}
			}
			return -1;
		}
		static void Main(string[] args)
		{
			for (int i = 0; i < accounts.Length; i++)
			{
				accounts[i] = new Account("USD");
			}

			Account.GetSortedAccounts(accounts);
			Console.WriteLine(accounts[1].Id_Property);//Id for checking

			Console.Write("Enter id of account (from 100 000 to 99 999 999): ");
			int userEnter = int.Parse(Console.ReadLine());

			if (userEnter < 100000 || userEnter > 99999999)
			{
				throw new ArgumentOutOfRangeException();
			}

			GetAccount(userEnter);
		}
	}
}
