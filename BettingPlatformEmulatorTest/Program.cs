using System;
using Library;

namespace BettingPlatformEmulatorTest
{
	class Program
	{
		static void Main(string[] args)
		{
			BettingPlatformEmulator ourBetPlatform = new BettingPlatformEmulator();
			ourBetPlatform.Start();
		}
	}
}
