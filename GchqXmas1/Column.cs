using System;
using System.Linq;
using System.Collections.Generic;

namespace GchqXmas1
{

	public class Column : Vector
	{
		bool?[,] _matrix;

		public Column (bool?[,] matrix, int columnIndex, int[] blockLengths) : base(blockLengths)
		{
			_matrix = matrix;
			Index = columnIndex;
		}

		public int Index { get; private set; }

		public override  bool? this[int index] {
			get {
				return _matrix [index, Index];
			}
			set {
				_matrix [index, Index] = value;
			}
		}
	}
}
