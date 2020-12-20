using System;
using Library;

namespace Task_1._5
{
	class Program
	{
		static void Main(string[] args)
		{
			Player player = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "USD");
			Console.WriteLine("Login with login {0} and password {1} is {2}", player.LastName_Property, player.Password_Property, player.IsPasswordValid(player.Password_Property));
			Console.WriteLine("Login with login {0} and password {1} is {2}", player.LastName_Property, "qwerty", player.IsPasswordValid("qwerty"));
			player.Deposit(100, "USD");
			player.Withdraw(50, "EUR");
			try
			{
				player.Withdraw(100, "USD");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			try
			{
				Player player2 = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "PLN");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
