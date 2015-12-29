using System;
using System.Linq;
using System.Collections.Generic;

namespace GchqXmas1
{

	public class Row : Vector
	{
		bool?[,] _matrix;

		public Row (bool?[,] matrix, int rowIndex, int[] blockLengths) : base(blockLengths)
		{
			_matrix = matrix;
			Index = rowIndex;
		}

		public int Index { get; private set; }

		public override bool? this[int index] {
			get {
				return _matrix [Index, index];
			}
			set {
				_matrix [Index, index] = value;
			}
		}
	}
	
}
