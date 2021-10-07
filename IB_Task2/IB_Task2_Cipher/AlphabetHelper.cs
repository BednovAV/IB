using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_Task2_Cipher
{
	public class AlphabetHelper
	{
		public static Dictionary<char, char> EnByRuMap { get; } = new Dictionary<char, char>
		{
			{'А', 'A'},
			{'а', 'a'},
			{'В', 'B'},
			{'Е', 'E'},
			{'е', 'e'},
			{'К', 'K'},
			{'М', 'M'},
			{'Н', 'H'},
			{'О', 'O'},
			{'о', 'o'},
			{'Р', 'P'},
			{'р', 'p'},
			{'С', 'C'},
			{'с', 'c'},
			{'Т', 'T'},
			{'у', 'y'},
			{'Х', 'X'},
			{'х', 'x'},
		};
	}
}
