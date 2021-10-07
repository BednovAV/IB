using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_Task2_Decryptor
{
	class Program
	{
		private const string FILES_DIRECTORY = "C:\\Univer\\IB\\IB_Task2\\files\\";
		private const string CONTAINER_NAME = "container.txt";
		private const string OUTPUT_NAME = "output.txt";
		static void Main(string[] args)
		{
			var containerText = File.ReadAllText(FILES_DIRECTORY + CONTAINER_NAME, Encoding.GetEncoding(1251));
			var resultBits = new List<bool>();
			foreach (var ch in containerText)
			{
				if (AlphabetHelper.EnChars.Contains(ch))
				{
					resultBits.Add(true);
				}
				else if (AlphabetHelper.RuChars.Contains(ch))
				{
					resultBits.Add(false);
				}
				if (IsEnd(resultBits))
					break;
			}

			var resultBitsWithoutEndByte = resultBits.Take(resultBits.Count - 8).ToArray();
			var bitArray = new BitArray(resultBitsWithoutEndByte);
			var resultBytes = BitArrayToByteArray(bitArray);
			File.WriteAllBytes(FILES_DIRECTORY + OUTPUT_NAME, resultBytes);
			
		}

		private static bool IsEnd(List<bool> bits)
		{
			if (bits.Count < 8)
			{
				return false;
			}

			var endByte = bits.Skip(bits.Count - 8).ToArray();

			return endByte.Length == 8 && !endByte.Any(b => b != false);
		}

		public static byte[] BitArrayToByteArray(BitArray bits)
		{
			byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
			bits.CopyTo(ret, 0);
			return ret;
		}
	}
}
