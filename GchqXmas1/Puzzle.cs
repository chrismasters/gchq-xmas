using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace GchqXmas1
{
	public class Puzzle 
	{
		bool?[,] _matrix = new bool?[25,25];
		int[][] _rowLengths;
		int[][] _colLengths;
		Row[] _rows;
		Column[] _columns;

		public Puzzle ()
		{
			_matrix [3, 3] = true;
			_matrix [3, 4] = true;
			_matrix [3, 12] = true;
			_matrix [3, 13] = true;
			_matrix [3, 21] = true;

			_matrix [8, 6] = true;
			_matrix [8, 7] = true;
			_matrix [8, 10] = true;
			_matrix [8, 14] = true;
			_matrix [8, 15] = true;
			_matrix [8, 18] = true;

			_matrix [16, 6] = true;
			_matrix [16, 11] = true;
			_matrix [16, 16] = true;
			_matrix [16, 20] = true;

			_matrix [21, 3] = true;
			_matrix [21, 4] = true;
			_matrix [21, 9] = true;
			_matrix [21, 10] = true;
			_matrix [21, 15] = true;
			_matrix [21, 20] = true;
			_matrix [21, 21] = true;

			_rowLengths = new int[][] {
				new int[] { 7, 3, 1, 1, 7 },
				new int[] { 1, 1, 2, 2, 1, 1 },
				new int[] { 1, 3, 1, 3, 1, 1, 3, 1 },
				new int[] { 1, 3, 1, 1, 6, 1, 3, 1 },
				new int[] { 1, 3, 1, 5, 2, 1, 3, 1 },
				new int[] { 1, 1, 2, 1, 1 },
				new int[] { 7, 1, 1, 1, 1, 1, 7 },
				new int[] { 3, 3 },
				new int[] { 1, 2, 3, 1, 1, 3, 1, 1, 2 },
				new int[] { 1, 1, 3, 2, 1, 1 },
				new int[] { 4, 1, 4, 2, 1, 2 },
				new int[] { 1, 1, 1, 1, 1, 4, 1, 3 },
				new int[] { 2, 1, 1, 1, 2, 5 },
				new int[] { 3, 2, 2, 6, 3, 1 },
				new int[] { 1, 9, 1, 1, 2, 1 },
				new int[] { 2, 1, 2, 2, 3, 1 },
				new int[] { 3, 1, 1, 1, 1, 5, 1 },
				new int[] { 1, 2, 2, 5 },
				new int[] { 7, 1, 2, 1, 1, 1, 3 },
				new int[] { 1, 1, 2, 1, 2, 2, 1 },
				new int[] { 1, 3, 1, 4, 5, 1 },
				new int[] { 1, 3, 1, 3, 10, 2 },
				new int[] { 1, 3, 1, 1, 6, 6 },
				new int[] { 1, 1, 2, 1, 1, 2 },
				new int[] { 7, 2, 1, 2, 5 },
			};

			_colLengths = new int[][] {
				new int[] { 7, 2, 1, 1, 7 },
				new int[] { 1, 1, 2, 2, 1, 1 },
				new int[] { 1, 3, 1, 3, 1, 3, 1, 3, 1 },
				new int[] { 1, 3, 1, 1, 5, 1, 3, 1 },
				new int[] { 1, 3, 1, 1, 4, 1, 3, 1 },
				new int[] { 1, 1, 1, 2, 1, 1 },
				new int[] { 7, 1, 1, 1, 1, 1, 7 },
				new int[] { 1, 1, 3 },
				new int[] { 2, 1, 2, 1, 8, 2, 1 },
				new int[] { 2, 2, 1, 2, 1, 1, 1, 2 },
				new int[] { 1, 7, 3, 2, 1 },
				new int[] { 1, 2, 3, 1, 1, 1, 1, 1 },
				new int[] { 4, 1, 1, 2, 6 },
				new int[] { 3, 3, 1, 1, 1, 3, 1 },
				new int[] { 1, 2, 5, 2, 2 },
				new int[] { 2, 2, 1, 1, 1, 1, 1, 2, 1 },
				new int[] { 1, 3, 3, 2, 1, 8, 1 },
				new int[] { 6, 2, 1 },
				new int[] { 7, 1, 4, 1, 1, 3 },
				new int[] { 1, 1, 1, 1, 4 },
				new int[] { 1, 3, 1, 3, 7, 1 },
				new int[] { 1, 3, 1, 1, 1, 2, 1, 1, 4 },
				new int[] { 1, 3, 1, 4, 3, 3 },
				new int[] { 1, 1, 2, 2, 2, 6, 1 },
				new int[] { 7, 1, 3, 2, 1, 1 }
			};

			_rows = Enumerable.Range (0, 25).Select (i => new Row (_matrix, i, _rowLengths [i])).ToArray();
			_columns = Enumerable.Range (0, 25).Select (i => new Column (_matrix, i, _colLengths [i])).ToArray();
		}

		public void Solve ()
		{
			while (true) 
			{
				bool solvedAny = false;
				foreach (var row in _rows) //.OrderBy(_ => _.Ambiguity())) 
				{
					if (row.IsSolved ())
						continue;
					Console.WriteLine ("Row {0} ambiguity = {1}", row.Index, row.Ambiguity ());
					bool solved;
					if (solved = row.TrySolve ()) 
					{
						solvedAny |= solved;
						Console.WriteLine ("Solved row {0}", row.Index);
					}
				}
				foreach (var column in _columns) //.OrderBy(_ => _.Ambiguity()))
				{
					if (column.IsSolved ())
						continue;
					Console.WriteLine ("Column {0} ambiguity = {1}", column.Index, column.Ambiguity ());
					bool solved;
					if (solved = column.TrySolve ()) 
					{
						solvedAny |= solved;
						Console.WriteLine ("Solved column {0}", column.Index);
					}
				}

				for (int i = 0; i < 25; i++) {
					Console.Write ("{0:00} : ", i);
					for (int j = 0; j < 25; j++) {
						var x = _matrix [i, j];
						Console.Write (x.HasValue ? x.Value ? "█" : " " : "?");
					}
					Console.WriteLine ();
				}

				if (!solvedAny) 
				{
					Console.WriteLine ("Iteration solved nothing! Bailing...");

					_rows [3].TrySolve ();
					break;
				}

				if (_rows.All (_ => _.IsSolved ())) 
				{
					Console.WriteLine ("All Done!");
					break;
				}
			}
		}

		public void Save(string filename) {
			var qrCode = new Bitmap(500, 500);
			var qrCodeGraphics = Graphics.FromImage(qrCode);
			qrCodeGraphics.FillRectangle(Brushes.White, new Rectangle(0,0,500,500));
			for (int i = 0; i < 25; i++) {
				for (int j = 0; j < 25; j++) {
					var x = _matrix [i, j];
					if (!x.HasValue) {
						throw new Exception ("Not Solved");
					}
					if (x.Value) {
						var r = new Rectangle(j * 20, i * 20, 20, 20);
						qrCodeGraphics.FillRectangle(Brushes.Black, r);
					}
				}
			}
			qrCode.Save (filename);
		}
	}
}
