using System;
using Library;

namespace Task_1._1
{
	class Program
	{
		static void Main(string[] args)
		{
			Account EUR_Account = new Account("EUR");
			Account USD_Account = new Account("USD");
			Account UAH_Account = new Account("UAH");
			EUR_Account.Deposit(10, "EUR");
			EUR_Account.Withdraw(3, "UAH");
			UAH_Account.Deposit(121, "USD");
			try
			{
				USD_Account.Withdraw(5, "USD");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			try
			{
				Account PLN_Account = new Account("PLN");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("Account with currency {0} has {1} balance", EUR_Account.Currency_Property, EUR_Account.GetBalance("EUR"));
			Console.WriteLine("Account with currency {0} has {1} balance", USD_Account.Currency_Property, USD_Account.GetBalance("USD"));
			Console.WriteLine("Account with currency {0} has {1} balance", UAH_Account.Currency_Property, UAH_Account.GetBalance("UAH"));
		}
	}
}
