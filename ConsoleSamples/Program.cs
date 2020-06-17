using ConsoleSamples.Casino_Sample;
using ConsoleSamples.EF_Relationship_Sample;
using ConsoleSamples.Finance_Sample;
using ConsoleSamples.Google_Docs_API;
using Google.Apis.Docs.v1.Data;
using System;
using System.Collections.Generic;

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
				Console.WriteLine("2. EF Relationships");
				Console.WriteLine("3. Finance");
				Console.WriteLine("4. Casino Games");

				Console.WriteLine("Q. Quit");

				string input = Console.ReadLine();
				IConsoleSample sample = null;
				switch (input.ToLower())
				{
					case "1":
						sample = new GoogleDocsAPISample();
						break;
					case "2":
						sample = new RelationshipSample();
						break;
					case "3":
						sample = new FinanceSample();
						break;
					case "4":
						sample = new CasinoSample();
						break;
					case "q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Input not recognized.");
						break;
				}
				if (sample != null)
					sample.Run();
			}
		}
	}

	public interface IConsoleSample
	{
		public void Run();
	}
}
