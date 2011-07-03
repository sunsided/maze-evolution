using System;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Implementierung des Randomized Prim-Algorithmus
	/// </summary>
	public abstract class MazeGenerator4 : IMazeGenerator
	{
		/// <summary>
		/// Randomizer
		/// </summary>
		protected Random Randomizer { [Pure] get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		/// <remarks></remarks>
		protected MazeGenerator4()
		{
			Randomizer = new Random();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		/// <remarks></remarks>
		protected MazeGenerator4(int seed)
		{
			Randomizer = new Random(seed);
		}

		/// <summary>
		/// Bereitet das Wall-Array vor
		/// </summary>
		/// <param name="width">Die Breite</param>
		/// <param name="height">Die Höhe</param>
		/// <returns></returns>
		[Pure]
		protected Wall4[,] PrepareWalls(int width, int height)
		{
			Contract.Requires(width > 0 && height > 0);
			Contract.Ensures(Contract.Result<Wall4[,]>() != null);

			// Array vorbereiten und Affentest durchführen
			Wall4[,] cells = new Wall4[width,height];
			for (int y = 0; y < height; ++y) for (int x = 0; x < width; ++x) cells[x, y] = Wall4.All;
			return cells;
		}

		/// <summary>
		/// Erzeugt ein Labyrinth der Dimensionen <paramref name="width"/>x<paramref name="height"/>.
		/// </summary>
		/// <param name="width">Die Breite des Labyrinths in Zellen</param>
		/// <param name="height">Die Höhe des Labyrinths in Zellen</param>
		/// <returns>Das Labyrinth</returns>
		public abstract Wall4[,] Generate(int width, int height);

		/// <summary>
		/// Entfernt die Wand zwischen den beiden Zellen
		/// </summary>
		/// <param name="cells">Die Zellen</param>
		/// <param name="current">Die aktuelle Zelle</param>
		/// <param name="selected">Die ausgewählte Zelle</param>
		[Pure]
		protected static void RemoveWallBetween(ref Wall4[,] cells, Tuple<int, int> current, Tuple<int, int> selected)
		{
			Contract.Requires(cells != null);
			Contract.Requires(current != null);
			Contract.Requires(selected != null);
			Contract.Ensures(cells != null);

			// X-Achse
			if (current.Item1 > selected.Item1)
			{
				cells[selected.Item1, selected.Item2] = cells[selected.Item1, selected.Item2] & ~Wall4.East;
				cells[current.Item1, current.Item2] = cells[current.Item1, current.Item2] & ~Wall4.West;
			}
			else if (current.Item1 < selected.Item1)
			{
				cells[selected.Item1, selected.Item2] = cells[selected.Item1, selected.Item2] & ~Wall4.West;
				cells[current.Item1, current.Item2] = cells[current.Item1, current.Item2] & ~Wall4.East;
			}

			// Y-Achse
			if (current.Item2 > selected.Item2)
			{
				cells[selected.Item1, selected.Item2] = cells[selected.Item1, selected.Item2] & ~Wall4.South;
				cells[current.Item1, current.Item2] = cells[current.Item1, current.Item2] & ~Wall4.North;
			}
			else if (current.Item2 < selected.Item2)
			{
				cells[selected.Item1, selected.Item2] = cells[selected.Item1, selected.Item2] & ~Wall4.North;
				cells[current.Item1, current.Item2] = cells[current.Item1, current.Item2] & ~Wall4.South;
			}
		}
	}
}
