using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_Task2_Decryptor
{
	public class AlphabetHelper
	{
		public static HashSet<char> RuChars { get; } = new HashSet<char>
		{
			'А',
			'а',
			'В',
			'Е',
			'е',
			'К',
			'М',
			'Н',
			'О',
			'о',
			'Р',
			'р',
			'С',
			'с',
			'Т',
			'у',
			'Х',
			'х'
		};

		public static HashSet<char> EnChars { get; } = new HashSet<char>
		{
			'A',
			'a',
			'B',
			'E',
			'e',
			'K',
			'M',
			'H',
			'O',
			'o',
			'P',
			'p',
			'C',
			'c',
			'T',
			'y',
			'X',
			'x',
		};
	}
}
