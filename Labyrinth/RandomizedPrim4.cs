using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Implementierung des Randomized Prim-Algorithmus
	/// </summary>
	public sealed class RandomizedPrim4 : MazeGenerator4, IMazeGenerator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RandomizedPrim4"/> class.
		/// </summary>
		/// <remarks></remarks>
		public RandomizedPrim4()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomizedPrim4"/> class.
		/// </summary>
		/// <remarks></remarks>
		public RandomizedPrim4(int seed)
			: base(seed)
		{
		}

		/// <summary>
		/// Erzeugt ein Labyrinth der Dimensionen <paramref name="width"/>x<paramref name="height"/>.
		/// </summary>
		/// <param name="width">Die Breite des Labyrinths in Zellen</param>
		/// <param name="height">Die Höhe des Labyrinths in Zellen</param>
		/// <returns>Das Labyrinth</returns>
		public override Wall4[,] Generate(int width, int height)
		{
			// Array vorbereiten und Affentest durchführen
			Wall4[,] cells = PrepareWalls(width, height);


			return cells;
		}
	}
}
