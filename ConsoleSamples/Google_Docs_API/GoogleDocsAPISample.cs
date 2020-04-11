using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using ConsoleSamples.Properties;

namespace ConsoleSamples.Google_Docs_API
{
	public class GoogleDocsAPISample : IConsoleSample
	{
		public void Run()
		{
			Console.WriteLine("Welcome to the Google Docs API Sample.");
			Console.WriteLine();

			#region Quickstart
			DocsQuickstart();
			SheetsQuickstart();
			#endregion

			Console.WriteLine();
			Console.WriteLine("This is the end of the sample.");
		}

		public void DocsQuickstart()
		{
			// Quickstart from https://developers.google.com/docs/api/quickstart/dotnet.

			// If modifying these scopes, delete your previously saved credentials
			// at ~/.credentials/docs.googleapis.com-dotnet-quickstart.json
			// TODO: I don't know what that comment means.

			string[] Scopes = { DocsService.Scope.DocumentsReadonly };
			string ApplicationName = "Google Docs API .NET Quickstart";

			UserCredential credential;

			using (var stream = new MemoryStream(Properties.Resources.credentials))
			{
				Console.WriteLine($"I created a memory stream based on the contents of the resource file: {Resources.credentials.ToString()}.");
//			}
//			using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
//			{
				string credPath = "docs-token.json";

				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);

				// NOTE: Deleting the token.json directory (or maybe just the file inside) will require the
				// app to reauthenticate via the web browser.

				// NOTE: For some reason it doesn't like using the same token.json for both Docs and Sheets.
				// One solution is to give them different names.
			}

			// Create Google Docs API service.
			var service = new DocsService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			// Define request parameters.
			String documentId = "195j9eDD3ccgjQRttHhJPymLJUCOUjs-jmwTrekvdjFE";
			DocumentsResource.GetRequest request = service.Documents.Get(documentId);

			// Prints the title of the requested doc:
			// https://docs.google.com/document/d/195j9eDD3ccgjQRttHhJPymLJUCOUjs-jmwTrekvdjFE/edit
			Document doc = request.Execute();
			Console.WriteLine($"The title of the doc is: {doc.Title}");

			// Try a doc that I own.
			documentId = "1zoKjwGqKvBFHfFK2e1jSe60SrWSyt-0ba7kEyXmaF7k";
			request = service.Documents.Get(documentId);
			doc = request.Execute();
			Console.WriteLine($"The title of the doc is: {doc.Title}");
		}

		public void SheetsQuickstart()
		{
			// Quickstart from https://developers.google.com/sheets/api/quickstart/dotnet

			// If modifying these scopes, delete your previously saved credentials
			// at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
			string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
			string ApplicationName = "Google Sheets API .NET Quickstart";

			UserCredential credential;

			// This is an example of using a file that has been added to the Properties "folder"
			// of the project. The file is added to the Properties subdirectory under the project
			// directory, so it does not need to be elsewhere in the project hierarchy.
			// Note that although it is called an "embedded resource" it does not show up on the
			// Resources page of the project properties.
			var asm = System.Reflection.Assembly.GetExecutingAssembly();
			string asmName = asm.GetName().Name;
			Console.WriteLine($"The name of the assembly is: {asmName}.");
			foreach (var rsc in asm.GetManifestResourceNames())
			{
				Console.WriteLine($"Resource name: {rsc}.");
			}
			Console.WriteLine("About to open the resource file for reading.");
			using (Stream stream = asm.GetManifestResourceStream(asmName + ".Properties.alt_credentials.json"))
			{
				byte[] one = new byte[1];
				int num;
				do
				{
					num = stream.Read(one, 0, 1);
					Console.Write($"{(char)one[0]}");
				} while (num != 0);
			}
			using (Stream stream = asm.GetManifestResourceStream(asmName + ".Properties.alt_credentials.json"))
			{
				Console.WriteLine($"I created a resource stream based on the contents of the resource file.");
						// The file token.json stores the user's access and refresh tokens, and is created
						// automatically when the authorization flow completes for the first time.
						string credPath = "sheets-token.json";
					credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
						GoogleClientSecrets.Load(stream).Secrets,
						Scopes,
						"user",
						CancellationToken.None,
						new FileDataStore(credPath, true)).Result;
					Console.WriteLine("Credential file saved to: " + credPath);
			}

			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			// Define request parameters.
			String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
			String range = "Class Data!A2:E";
			SpreadsheetsResource.ValuesResource.GetRequest request =
					service.Spreadsheets.Values.Get(spreadsheetId, range);

			// Prints the names and majors of students in a sample spreadsheet:
			// https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
			ValueRange response = request.Execute();
			IList<IList<Object>> values = response.Values;
			if (values != null && values.Count > 0)
			{
				Console.WriteLine("Name, Major");
				foreach (var row in values)
				{
					// Print columns A and E, which correspond to indices 0 and 4.
					Console.WriteLine("{0}, {1}", row[0], row[4]);
				}
			}
			else
			{
				Console.WriteLine("No data found.");
			}
			Console.Read();

			Console.WriteLine();
			Console.WriteLine($"Pull some data from my Stock Buy/Sell Ranges spreadsheet.");
			spreadsheetId = "1HdcV-xkh4i7lPhY1bZb1K2-OfOWm2BHkxm1EMwCs19s";
			range = "A1:G50";
			request = service.Spreadsheets.Values.Get(spreadsheetId, range);
			response = request.Execute();
			values = response.Values;
			if (values != null && values.Count > 0)
			{
				Console.WriteLine("Symbol, URL, Current Price, Fair Value, Current Upside, Buy Target, Strong Buy Target");
				foreach (var row in values)
				{
					// Print columns A through G, which correspond to indices 0 through 6.
					Console.WriteLine($"{row[0]}, {row[2]}, {row[3]}, {row[4]}, {row[5]}, {row[6]}");
				}
			}
			else
			{
				Console.WriteLine("No data found.");
			}
			Console.Read();
		}
	}
}
