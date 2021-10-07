using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_Task2_Cipher
{
	class Program
	{
		private const string FILES_DIRECTORY = "C:\\Univer\\IB\\IB_Task2\\files\\";
		private const string CONTAINER_NAME = "container.txt";
		private const string CONTAINER_SOURCE_NAME = "source.txt";
		private const string INPUT_NAME = "input.txt";
		static void Main(string[] args)
		{
			var bits = new BitArray(File.ReadAllBytes(FILES_DIRECTORY + INPUT_NAME));

			var sourceText = File.ReadAllText(FILES_DIRECTORY + CONTAINER_SOURCE_NAME, Encoding.GetEncoding(1251));

			var encryptedMessage = new List<char>();
			int currBit = 0;
			for (int i = 0; i < sourceText.Length; i++)
			{
				if (AlphabetHelper.EnByRuMap.ContainsKey(sourceText[i]) && currBit != bits.Length)
				{
					encryptedMessage.Add(bits[currBit++] ?
						AlphabetHelper.EnByRuMap[sourceText[i]] :
						sourceText[i]);
				}
				else
				{
					encryptedMessage.Add(sourceText[i]);
				}
			}

			File.WriteAllText(FILES_DIRECTORY + CONTAINER_NAME, new String(encryptedMessage.ToArray()), Encoding.GetEncoding(1251));
		}
	}
}
