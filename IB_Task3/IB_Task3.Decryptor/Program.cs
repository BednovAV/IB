using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IB_Task3.Decryptor
{
	class Program
	{
		static void Main(string[] args)
		{
			var file = ReadFileInfo(); // C:\Univer\IB\IB_Task3\Files.txt
			var fileStr = File.ReadAllText(file.FullName);
			var key = ReadKey();
			var decryptedFile = Decrypt(fileStr, key);
			SaveDecryptedString(file, decryptedFile);
		}

		private static FileInfo ReadFileInfo()
		{
			string path;
			do
			{
				Console.Write("File path: ");
				path = Console.ReadLine();
			} while (!CheckFilePath(path));

			return new FileInfo(path);
		}

		private static bool CheckFilePath(string path)
		{
			if (!File.Exists(path))
			{
				Console.WriteLine("Not existing file!");
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
			foreach (var ch in key)
			{
				if (!DecryptorAlphabetHelper.Alphabet.Contains(ch))
				{
					Console.WriteLine($"Error! Alphabet not contain char: '{ch}'");
					return false;
				}
			}

			return true;
		}

		private static string Decrypt(string message, string key)
		{
			var messageInts = GetIntsByString(message);

			var keyInts = GetIntsByString(key);
			var keysFlow = GetFlow(keyInts, messageInts.Count);

			// calculating int values of decrypting message
			var intValues = new List<int>();
			for (int i = 0; i < messageInts.Count; i++)
			{
				var intValue = (messageInts[i] - keysFlow[i]) % DecryptorAlphabetHelper.Alphabet.Count;
				intValue = intValue < 0 ?
					(intValue + DecryptorAlphabetHelper.Alphabet.Count) % DecryptorAlphabetHelper.Alphabet.Count :
					intValue;

				intValues.Add(intValue);
			}

			// calculating char values of decrypting message
			var charValues = new List<char>();
			foreach (var i in intValues)
			{
				charValues.Add(DecryptorAlphabetHelper.Alphabet[i]);
			}

			return new String(charValues.ToArray());
		}

		private static List<int> GetIntsByString(string str)
		{
			var result = new List<int>();
			foreach (var ch in str)
			{
				result.Add(DecryptorAlphabetHelper.Alphabet.IndexOf(ch));
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

		private static void SaveDecryptedString(FileInfo file, string decryptedFile)
		{
			var decryptedFiles = decryptedFile.Split('|');
			foreach (var fileStr in decryptedFiles)
			{
				var parameters = fileStr.Split('>');

				try
				{
					if (!Path.IsPathRooted(parameters[0]))
						throw new Exception();

					if (!Directory.Exists(Path.GetDirectoryName(parameters[0])))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(parameters[0]));
					}

					File.WriteAllText(parameters[0], parameters[1]);
				}
				catch (Exception)
				{
					Console.WriteLine("Wrong key!");
					return;
				}
			}

			file.Delete();
			Console.WriteLine("Directory succesfuly decrypted!");
		}
	}
}
