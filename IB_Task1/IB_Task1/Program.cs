using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_Task1
{
	class Program
	{
		/*
		 * 1) C:\Univer\IB\IB_Task1\Files\input.txt
		 * 2) C:\Univer\IB\IB_Task1\Files
		 */

		private const int SIGNATURE_SIZE = 16;

		static void Main(string[] args)
		{
			var signature = GetFileData();
			var directoryPath = GetDirectoryFiles();
			var foundedFiles = GetSimilarFiles(signature, directoryPath);
			ShowFoundedFiles(foundedFiles);
		}

		private static void ShowFoundedFiles(IEnumerable<string> foundedFiles)
		{
			Console.WriteLine("Founded files:");
			foreach (var file in foundedFiles)
			{
				Console.WriteLine($"  {file}");
			}
		}

		private static IEnumerable<string> GetSimilarFiles(byte[] signature, string directoryPath)
		{
			var signatureString = String.Join("-", signature);

			var filePathes = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
			foreach (var filePath in filePathes)
			{
				var fileBytes = String.Join("-", File.ReadAllBytes(filePath));
				if (fileBytes.Contains(signatureString))
				{
					yield return filePath;
				}
			}
		}

		private static string GetDirectoryFiles()
		{
			bool directoryNotExists = true;
			string path = string.Empty;
			while (directoryNotExists)
			{
				Console.Write("Directory path: ");
				path = Console.ReadLine();
				directoryNotExists = !Directory.Exists(path);
				if (directoryNotExists)
				{
					Console.WriteLine("Directory not exists!");
				}
			}

			return path;
		}

		private static byte[] GetFileData()
		{
			bool fileNotExists = true;
			string path = string.Empty;
			while(fileNotExists)
			{
				Console.Write("File path: ");
				path = Console.ReadLine();
				fileNotExists = !File.Exists(path);
				if (fileNotExists)
				{
					Console.WriteLine("File not exists!");
				}
			}

			var bytes = File.ReadAllBytes(path);
			var offset = GetOffset(bytes.Length);
			return bytes.Skip(offset).Take(SIGNATURE_SIZE).ToArray();
		}

		private static int GetOffset(int maxValue)
		{
			int offset = 0;

			do
			{
				Console.Write("Offset: ");
			} while (!int.TryParse(Console.ReadLine(), out offset) || offset > maxValue);


			return offset;
		}
	}
}
