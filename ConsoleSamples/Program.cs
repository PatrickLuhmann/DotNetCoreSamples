using ConsoleSamples.Google_Docs_API;
using System;

namespace ConsoleSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to .NET Core Console Samples.");

			bool quit = false;
			while (!quit)
			{
				Console.WriteLine("Please select a sample to run.");

				Console.WriteLine("1. Google Docs API");

				Console.WriteLine("Q. Quit");

				string input = Console.ReadLine();
				switch (input.ToLower())
				{
					case "1":
						var sample = new GoogleDocsAPISample();
						sample.Run();
						break;
					case "q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Input not recognized.");
						break;
				}
			}
		}
	}
}
