using System;
using System.Linq;
using System.Collections.Generic;

namespace GchqXmas1
{
	public abstract class Vector 
	{
		protected int[] _blockLengths;

		protected Vector(int[] blockLengths)
		{
			_blockLengths = blockLengths;
		}

		public abstract bool? this[int index] 
		{
			get; set;
		}

		public bool IsSolved()
		{
			for (int i = 0; i < 25; i++) {
				if (!this [i].HasValue)
					return false;
			}
			return true;
		}

		public int Ambiguity()
		{
			return Ambiguity (_blockLengths, 25);
		}

		protected int Ambiguity(int[] blockLengths, int space)
		{
			if (blockLengths.Length == 0)
				return 0;
			return space - (blockLengths.Sum () + (blockLengths.Length - 1));
		}

		bool?[] FindCertainties (IReadOnlyList<bool[]> potentials)
		{
			if (!potentials.Any ())
				return new bool?[25];
			return potentials
				.Select(_ => _.Cast<bool?>().ToArray())
				.Aggregate ((a, b) => {
				if (a.Length != 25 || b.Length != 25)
					throw new ArgumentException("Bad length");
				var c = new bool?[25];
				for (int i=0; i<25; i++) {
					c[i] = null;
					if (a[i] == b[i])
						c[i] = a[i];
				}
				return c;
			});
		}

		public bool TrySolve() 
		{
			if (IsSolved ())
				return false;
			
			var potentials = Potentials ().ToList();
			bool?[] certainties = FindCertainties (potentials);
			if (certainties.Any (_ => _.HasValue)) {
				bool solvedSomething = false;
				for (int i = 0; i < 25; i++) {
					if (certainties[i].HasValue && !this[i].HasValue) {
						this [i] = certainties [i];
							solvedSomething = true;
					}
				}
				return solvedSomething;
			} 
			else {
				Console.WriteLine ("Nothing solved.");
			}

			return false;
		}

		public IEnumerable<bool[]> Potentials()
		{
			return Potentials (_blockLengths, new bool[0], Ambiguity (_blockLengths, 25));
		}

		public IEnumerable<bool[]> Potentials(IEnumerable<int> blockLengths, bool[] prefix, int amb) 
		{
			var bl = blockLengths.ToArray ();
			var offset = prefix.Length;
			//var amb = Ambiguity (bl, 25 - offset);

			if (bl.Length == 0) {
				if (prefix.Length != 25)
					throw new Exception ("Shouldn't happen!");
				if (prefix.Length == 25)
					if (IsPossible (prefix))
						yield return prefix;
				yield break;
			}

			for (int a = 0; a <= amb; a++) {
				var head = new bool[a + bl[0] + (bl.Length == 1 ? 0 : 1)];
				for (int i = 0; i < bl [0]; i++) {
					head [a + i] = true;
				}
				var p = prefix.Concat (head).ToArray ();
				if (bl.Length == 1)
					p = p.Concat (Enumerable.Repeat (false, 25)).Take (25).ToArray();
				foreach (var potential in Potentials(bl.Skip(1), p, amb - a).Where(IsPossible)) {
					yield return potential;
				}
			}
		}

		public bool IsPossible(bool[] vec) {
			for (var i = 0; i < 25; i++) {
				if (this [i].HasValue && this [i].Value != vec [i])
					return false;
			}
			return true;
		}
	}
}
