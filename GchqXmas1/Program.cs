using System;
using System.Linq;
using System.Collections.Generic;

namespace GchqXmas1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var puzzle = new Puzzle ();
			puzzle.Solve ();
			puzzle.Save ("gchq1.bmp");
		}
	}
}
