using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IB_Task3.Cypher
{
	class Program
	{
		static void Main(string[] args)
		{
			var directory = ReadDirectory();
			var directoryStr = GetDirectoryString(directory);
			var key = ReadKey();
			var encryptedDirectory = Encrypt(directoryStr, key);
			SaveEncryptedString(directory, encryptedDirectory);
		}

		public static DirectoryInfo ReadDirectory()
		{
			string path;
			do
			{
				Console.Write("Directory path: ");
				path = Console.ReadLine();
			} while (!CheckDirectory(path));

			return new DirectoryInfo(path);
		}

		private static bool CheckDirectory(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Console.WriteLine("Not existing directory!");
				return false;
			}

			var files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
			if (files.Any(f => !f.Contains(".txt")))
			{
				Console.WriteLine("Specified directory should't contain not .txt files!");
				return false;
			}

			return true;
		}

		public static string ReadKey()
		{
			string key;
			do
			{
				Console.Write("Key: ");
				key = Console.ReadLine();
			} while (!CheckKey(key));
			

			return key;
		}

		private static bool CheckKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				Console.WriteLine("Error! Key should't be empty");
				return false;
			}

			foreach (var ch in key)
			{
				if (!CypherAlphabetHelper.Alphabet.Contains(ch))
				{
					Console.WriteLine($"Error! Alphabet not contain char: '{ch}'");
					return false;
				}
			}

			return true;
		}

		private static string GetDirectoryString(DirectoryInfo directory)
		{
			var fileStrs = new List<string>();

			foreach (var file in directory.GetFiles("*.txt", SearchOption.AllDirectories))
			{
				fileStrs.Add($"{file.FullName}>{File.ReadAllText(file.FullName)}");
			}

			return string.Join("|", fileStrs);
		}

		private static string Encrypt(string message, string key)
		{
			var messageInts = GetIntsByString(message);

			var keyInts = GetIntsByString(key);
			var keysFlow = GetFlow(keyInts, messageInts.Count);

			// calculating int values of encrypting message
			var intValues = new List<int>();
			for (int i = 0; i < messageInts.Count; i++)
			{
				var intValue = (messageInts[i] + keysFlow[i]) % CypherAlphabetHelper.Alphabet.Count;
				intValues.Add(intValue);
			}

			// calculating char values of encrypting message
			var charValues = new List<char>();
			foreach (var i in intValues)
			{
				charValues.Add(CypherAlphabetHelper.Alphabet[i]);
			}

			return new String(charValues.ToArray());
		}

		private static List<int> GetIntsByString(string str)
		{
			var result = new List<int>();
			foreach (var ch in str)
			{
				var index = CypherAlphabetHelper.Alphabet.IndexOf(ch);
				if (index is -1)
					throw new Exception($"Error! Alphabet not contain char '{ch}'");
				result.Add(index);
			}

			return result;
		}

		private static List<int> GetFlow(List<int> keyInts, int count)
		{
			var result = new List<int>();
			for (int i = 0; i < count; i++)
			{
				result.Add(keyInts[i % keyInts.Count]);
			}
			return result;
		}

		private static void SaveEncryptedString(DirectoryInfo directory, string encryptedDirectory)
		{
			using (var ts = new TransactionScope())
			{
				File.WriteAllText(Path.Combine(directory.Parent.FullName, $"{directory.Name}.txt"), encryptedDirectory);
				directory.Delete(true);

				ts.Complete();
				Console.WriteLine("Directory succesfuly encrypted!");
			}
		}
	}
}
