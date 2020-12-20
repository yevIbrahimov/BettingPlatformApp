using System;
using System.Collections.Generic;

namespace Library
{
	public class Account
	{
		private int Id;
		//private static int IdUniq = 100000; //service field to check id uniq 

		private string Currency;
		private decimal Amount;

		public string Currency_Property
		{
			get => Currency;
		}
		public int Id_Property
		{
			get => Id;
			set => Id = value;
		}
		public decimal Amount_Property
		{
			get => Amount;
			set => Amount = value;
		}
		//TASK 1.1
		public Account(string currency)
		{
			if (currency.ToUpper() != "USD" & currency.ToUpper() != "EUR" & currency.ToUpper() != "UAH") throw new NotSupportedException();

			Id = new Random().Next(100000,100000000);
		

			this.Currency = currency;
			this.Amount = 0;
		}
		public void Deposit(decimal amount, string currency)
		{
			if (currency.ToUpper() != "USD" & currency.ToUpper() != "EUR" & currency.ToUpper() != "UAH") throw new NotSupportedException();
			if (amount < 0 || amount > decimal.MaxValue) throw new Exception();

			Amount += Convert(amount, currency);
		}
		public decimal Convert(decimal amount, string currency) // method to convert money
		{
			if(Currency == "USD")
			{
				switch (currency)
				{
					case "EUR": amount = amount * (decimal)1.19; break;
					case "UAH": amount = amount / (decimal)28.36; break;
				}
			}
			else if(Currency == "UAH")
			{
				switch (currency)
				{
					case "USD": amount = amount * (decimal)28.36; break;
					case "EUR": amount = amount * (decimal)33.63; break;
				}
			}
			else
			{
				switch (currency)
				{
					case "USD": amount = amount / (decimal)1.19; break;
					case "UAH": amount = amount / (decimal)33.63; break;
				}
			}
			return amount;
		}
		public void Withdraw(decimal amount, string currency)
		{
			if (currency.ToUpper() != "USD" & currency.ToUpper() != "EUR" & currency.ToUpper() != "UAH") throw new NotSupportedException();
			amount = Convert(amount, currency);
			if (amount < 0 || amount > Amount) throw new InvalidOperationException();
			Amount -= amount;
		}
		public decimal GetBalance(string currency)
		{
			return Convert(Amount, currency);
		}
		//TASK1.2
		public static Account[] GetSortedAccounts(Account[] accounts)
		{
			int swap;
			for (int i = 0; i < accounts.Length - 1; i++)
			{
				for (int j = i + 1; j < accounts.Length; j++)
				{
					if (accounts[i].Id_Property > accounts[j].Id_Property)
					{
						swap = accounts[i].Id_Property;
						accounts[i].Id_Property = accounts[j].Id_Property;
						accounts[j].Id_Property = swap;
					}
				}
			}
			return accounts;
		}

		//Task1.4
		private static void Swap(ref Account elem_1, ref Account elem_2)
		{
			Account swap = elem_1;
			elem_1 = elem_2;
			elem_2 = swap;
		}
		private static int Partition(Account[] accounts, int minIndex, int maxIndex)
		{
			int pivot = minIndex - 1;
			for (int i = minIndex; i < maxIndex; i++)
			{
				if (accounts[i].Id_Property < accounts[maxIndex].Id_Property)
				{
					pivot++;
					Swap(ref accounts[pivot], ref accounts[i]);
				}
			}
			pivot++;
			
			Swap( ref accounts[pivot], ref accounts[maxIndex]);
			return pivot;
		}
		public static Account[] GetSortedAccountsByQuickSort(Account[] accounts, int minIndex, int maxIndex)
		{
			if (minIndex >= maxIndex)
			{
				return accounts;
			}

			int pivotIndex = Partition(accounts, minIndex, maxIndex);
			GetSortedAccountsByQuickSort(accounts, minIndex, pivotIndex - 1);
			GetSortedAccountsByQuickSort(accounts, pivotIndex + 1, maxIndex);

			return accounts;
		} 
	}
	//Task_1.5
	public class Player
	{
		private int Id;
		private string FirstName;
		private string LastName;
		private string Email;
		private string Password;
		private Account Account;

		public string FirstName_Property
		{
			get => FirstName;
		}
		public string LastName_Property
		{
			get => LastName;
		}
		public string Email_Property
		{
			get => Email;
		}
		public string Password_Property
		{
			get => Password;
		}
		public Account Account_Property
		{
			get => Account;
		}


		public Player(string firstName, string lastName, string email, string password, string currency)
		{
			Id = new Random().Next(100000, 100000000);
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Email = email;
			this.Password = password;
			this.Account = new Account(currency);
		}
		public bool IsPasswordValid(string password)
		{
			if(password == this.Password)
			{
				return true;
			}
			return false;
		}
		public void Deposit(decimal amount, string currency)
		{
			if (currency.ToUpper() != "USD" & currency.ToUpper() != "EUR" & currency.ToUpper() != "UAH") throw new NotSupportedException();
			if (amount < 0 || amount > decimal.MaxValue) throw new Exception();

			Account.Amount_Property += Account.Convert(amount, currency);
		}
		public void Withdraw(decimal amount, string currency)
		{
			amount = Account.Convert(amount, currency);
			if (amount < 0 || amount > Account.Amount_Property) throw new InvalidOperationException();
			Account.Amount_Property -= amount;
		}
	}
	public class BettingPlatformEmulator
	{
		private List<Player> Players = new List<Player>();
		private Player ActivePlayer;
		private Account Account;
		BetService betService = new BetService();
		public BettingPlatformEmulator()
		{
			Account = new Account("USD");
		}
		public void Start()
		{
			try
			{
				if(ActivePlayer == null)
				{
					Console.WriteLine("Choose action\n1. Register\n2. Login\n3. Stop");

					string answer = Console.ReadLine();
					if (answer.ToUpper() == "LOGIN")
					{
						Login();
					}
					else if (answer.ToUpper() == "REGISTER")
					{
						Register();
						Console.WriteLine("Succesfully register, login please");
						Login();
					}
					else if (answer.ToUpper() == "STOP")
					{
						Exit();
					}
					else Console.WriteLine("Invalid comand");
				}
				else
				{
					Console.WriteLine("Choose action\n1. Deposit\n2. Withdraw\n3. Logout");

					string answer = Console.ReadLine();
					if (answer.ToUpper() == "DEPOSIT")
					{
						Deposit();
					}
					else if (answer.ToUpper() == "WITHDRAW")
					{
						Withdraw();
					}
					else if (answer.ToUpper() == "LOGOUT")
					{
						Logout();
					}
					else Console.WriteLine("Invalid comand");
				}
				Start();
			}
			catch(Exception e)
			{
				//Console.WriteLine(e.Message);
				Start();
			}
		}
		public void Exit()
		{
			Environment.Exit(0);
		}
		public void Register()
		{
			string name, lastname, email, password, currency;
			Console.Write("Enter your name, please:");
			name = Console.ReadLine();
			Console.Write("Enter your lastname, please:");
			lastname = Console.ReadLine();
			Console.Write("Enter your email, please:");
			email = Console.ReadLine();
			Console.Write("Enter your password, please:");
			password = Console.ReadLine();
			Console.Write("Enter your currency, please:");
			currency = Console.ReadLine();

			while (currency.ToUpper() != "USD" & currency.ToUpper() != "EUR" & currency.ToUpper() != "UAH") 
			{
				Console.Write("We do not support those currency, please enter corect:");
				currency = Console.ReadLine();
			} 		
			Players.Add(new Player(name, lastname, email, password, currency));

		}
		public void Login()
		{
			string lastname, password;
			Console.Write("Enter your lastname, please:");
			lastname = Console.ReadLine();
			Console.Write("Enter your password, please:");
			password = Console.ReadLine();
			foreach (var player in Players)
			{
				if (player.LastName_Property == lastname && player.Password_Property == password) ActivePlayer = player;
				else Console.WriteLine("No such user");
			}
		}
		public void Logout()
		{
			ActivePlayer = null;
			Console.WriteLine("You was succesfully logout");
		}
		public void Deposit()
		{
			Console.Write("Enter amount: ");
			decimal amount = decimal.Parse(Console.ReadLine());
			Console.Write("Enter currency: ");
			string currency = Console.ReadLine();

			if (currency.ToUpper() != "USD" & currency.ToUpper() != "EUR" & currency.ToUpper() != "UAH") throw new NotSupportedException();
			if (amount < 0 || amount > decimal.MaxValue) throw new Exception();

			ActivePlayer.Deposit(amount, currency);
			Account.Deposit(amount, currency);
		}
		public void Withdraw()
		{
			Console.Write("Enter amount: ");
			decimal amount = decimal.Parse(Console.ReadLine());
			Console.Write("Enter currency: ");
			string currency = Console.ReadLine();

			if (Account.Convert(amount, currency) > ActivePlayer.Account_Property.GetBalance(currency))
			{
				Console.WriteLine("There is insufficient funds on your account");
			}
			else if (Account.Convert(amount, currency) > Account.GetBalance(currency))
			{
				Console.WriteLine("There is some problem on the platform side. Please try it later");
			}

			ActivePlayer.Withdraw(amount, currency);



		}

		public void Start_task2_2()
		{

			try
			{
				if (ActivePlayer == null)
				{
					Console.WriteLine("Choose action\n1. Register\n2. Login\n3. Stop");

					string answer = Console.ReadLine();
					if (answer.ToUpper() == "LOGIN")
					{
						Login();
					}
					else if (answer.ToUpper() == "REGISTER")
					{
						Register();
						Console.WriteLine("Succesfully register, login please");
						Login();
					}
					else if (answer.ToUpper() == "STOP")
					{
						Exit();
					}
					else Console.WriteLine("Invalid comand");
				}
				else
				{
					Console.WriteLine("Choose action\n1. Deposit\n2. Withdraw\n3. GetOdds\n4. Bet\n5. Logout");

					string answer = Console.ReadLine();
					if (answer.ToUpper() == "DEPOSIT")
					{
						Deposit();
					}
					else if (answer.ToUpper() == "WITHDRAW")
					{
						Withdraw();
					}
					else if(answer.ToUpper() == "GETODDS")
					{
						Console.WriteLine("Odd = {0}", betService.Odd_Property);
					}
					else if (answer.ToUpper() == "BET")
					{
						Console.Write("Enter your bet: ");
						decimal bet = decimal.Parse(Console.ReadLine());
						ActivePlayer.Account_Property.Withdraw(bet, "USD");
						decimal betWin = betService.Bet(bet);
						if (betWin>0)
						{
							ActivePlayer.Account_Property.Deposit(betWin + bet, "USD");

							Console.WriteLine("You win {0}", betWin);

						}
						else
						{
							Console.WriteLine("You lose!");
						}
					}
					else if (answer.ToUpper() == "LOGOUT")
					{
						Logout();
					}
					else Console.WriteLine("Invalid comand");
				}
				Start_task2_2();
			}
			catch (Exception e)
			{
				Start_task2_2();
			}
		}

	}
	public class BetService
	{
		private decimal Odd;
		public decimal Odd_Property
		{
			get => Odd;
			set
			{
				if (value >= (decimal)1.01 || value <= (decimal)25.00) Odd = value;
				else throw new ArgumentOutOfRangeException();
			}
		}
		public BetService()
		{
			Odd = (decimal)(new Random().Next(1, 24) + new Random().NextDouble());
			Math.Round(Odd, 2);
		}
		public float GetOdds()
		{
			Odd = (decimal)(new Random().Next(1, 24) + new Random().NextDouble());
			Odd_Property = Math.Round(Odd, 2);
			return (float)Odd;
		}
		public bool IsWon()
		{
			if (MatchResult() < (decimal)100.0 / Odd) return true;
			else return false;
		}
		private decimal MatchResult()
		{
			decimal result = (decimal)(new Random().Next(4, 99) + new Random().NextDouble());
			return result;
		}
		public decimal Bet(decimal amount)
		{
			if (IsWon())
			{
				return amount * Odd;
			}
			else return 0;
		}
	}

}
