using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Labyrinth
{
	/// <summary>
	/// Implementierung des Randomized Kruskal-Algorithmus
	/// </summary>
	public sealed class RandomizedKruskal4 : MazeGenerator4
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RandomizedPrim4"/> class.
		/// </summary>
		/// <remarks></remarks>
		public RandomizedKruskal4()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomizedPrim4"/> class.
		/// </summary>
		/// <remarks></remarks>
		public RandomizedKruskal4(int seed)
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
			// Zellen erzeugen
			Wall4[,] cells = PrepareWalls(width, height);
			if (height == 1 && width == 1) return cells;

			// 1. Create a list of all walls, and create a set for each cell, each containing just that one cell.
			List<Tuple<int, int, Wall4>> wallList = new List<Tuple<int, int, Wall4>>();
			HashSet<Tuple<int, int>>[,] sets = new HashSet<Tuple<int, int>>[width, height];
			for(int y=0; y<cells.GetLength(1); ++y)
			{
				for (int x = 0; x < cells.GetLength(0); ++x)
				{
					Tuple<int, int> cell = new Tuple<int, int>(x, y);
					AddWallsToWallList(cells, cell, ref wallList);
					sets[x, y] = new HashSet<Tuple<int, int>> {cell};
				}
			}

			// 2. For each wall, in some random order
			while(wallList.Count > 0)
			{
				int index = Randomizer.Next(0, wallList.Count - 1);
				Contract.Assume(index < wallList.Count);
				var wall = wallList[index];
				wallList.RemoveAt(index);
				Contract.Assume(wall != null);

				// 2.1 If the cells divided by this wall belong to distinct sets: 
				var oppositeCellCoords = SelectCellOnOppositeSideOfWall(wall);
				if (sets[wall.Item1, wall.Item2] == sets[oppositeCellCoords.Item1, oppositeCellCoords.Item2]) continue;

				// 2.1.1 Remove the current wall.
				RemoveWallBetween(ref cells, new Tuple<int, int>(wall.Item1, wall.Item2), oppositeCellCoords);

				// 2.1.2 Join the sets of the formerly divided cells.
				var currentSet = sets[wall.Item1, wall.Item2];
				currentSet.UnionWith(sets[oppositeCellCoords.Item1, oppositeCellCoords.Item2]);
				foreach (var cell in currentSet.Where(cell => sets[cell.Item1, cell.Item2] != currentSet))
				{
					sets[cell.Item1, cell.Item2] = currentSet;
				}
			}

			return cells;
		}

		/// <summary>
		/// Bezieht die Zelle, die gegenüber der in <paramref name="wall"/> angegebenen Wand liegt
		/// </summary>
		/// <param name="wall">Die zu testende Wand</param>
		/// <returns>Die Zelle</returns>
		/// <exception cref="ArgumentException">Ein ungültiger Wert für <paramref name="wall"/> wurde übergeben.</exception>
		private static Tuple<int, int> SelectCellOnOppositeSideOfWall(Tuple<int, int, Wall4> wall)
		{
			Contract.Requires(wall != null);
			Contract.Requires(wall.Item3.ExactlyOneValueSet());
			Contract.Requires(wall.Item1 >= 0 && wall.Item2 >= 0);
			return SelectCellOnOppositeSideOfWall(new Tuple<int, int>(wall.Item1, wall.Item2), wall.Item3);
		}

		/// <summary>
		/// Bezieht die Zelle, die gegenüber der in <paramref name="wall"/> angegebenen Wand der Zelle an Position
		/// <paramref name="cell"/> liegt
		/// </summary>
		/// <param name="cell">Die aktuelle Position</param>
		/// <param name="wall">Die zu testende Wand</param>
		/// <returns>Die Zelle</returns>
		/// <exception cref="ArgumentException">Ein ungültiger Wert für <paramref name="wall"/> wurde übergeben.</exception>
		private static Tuple<int, int> SelectCellOnOppositeSideOfWall(Tuple<int, int> cell, Wall4 wall)
		{
			Contract.Requires(cell != null);
			Contract.Requires(wall.ExactlyOneValueSet());
			Contract.Requires(cell.Item1 >= 0 && cell.Item2 >= 0);

			switch (wall)
			{
				case Wall4.North:
					Contract.Assume(cell.Item2 > 0);
					return new Tuple<int, int>(cell.Item1, cell.Item2 - 1);
				case Wall4.South:
					return new Tuple<int, int>(cell.Item1, cell.Item2 + 1);
				case Wall4.East:
					return new Tuple<int, int>(cell.Item1 + 1, cell.Item2);
				case Wall4.West:
					Contract.Assume(cell.Item1 > 0);
					return new Tuple<int, int>(cell.Item1 - 1, cell.Item2);
				default:
					throw new ArgumentException("Ungültige Wand-Werte:" + wall, "wall");
			}
		}

		/// <summary>
		/// Fügt alle Wände einer Zelle zur Wandliste hinzu
		/// </summary>
		/// <param name="cells">Die Karte der Zellen</param>
		/// <param name="cell">Die aktuelle Zelle</param>
		/// <param name="wallList">Die Wandliste</param>
		private static void AddWallsToWallList(Wall4[,] cells, Tuple<int, int> cell, ref List<Tuple<int, int, Wall4>> wallList)
		{
			Contract.Requires(cells != null);
			Contract.Requires(cell != null);
			Contract.Requires(wallList != null);
			Contract.Requires(cell.Item1 >= 0);
			Contract.Requires(cell.Item2 >= 0);
			Contract.Ensures(cells != null);
			Contract.Ensures(wallList != null);

			// Nordwand
			if (cell.Item2 > 0 && cells[cell.Item1, cell.Item2].ContainsWall(Wall4.North))
			{
				wallList.Add(new Tuple<int, int, Wall4>(cell.Item1, cell.Item2, Wall4.North));
			}

			// Südwand
			if (cell.Item2 < cells.GetLength(1) - 1 && cells[cell.Item1, cell.Item2].ContainsWall(Wall4.South))
			{
				wallList.Add(new Tuple<int, int, Wall4>(cell.Item1, cell.Item2, Wall4.South));
			}

			// Ostwand
			if (cell.Item1 < cells.GetLength(0) - 1 && cells[cell.Item1, cell.Item2].ContainsWall(Wall4.East))
			{
				wallList.Add(new Tuple<int, int, Wall4>(cell.Item1, cell.Item2, Wall4.East));
			}

			// Westwand
			if (cell.Item1 > 0 && cells[cell.Item1, cell.Item2].ContainsWall(Wall4.West))
			{
				wallList.Add(new Tuple<int, int, Wall4>(cell.Item1, cell.Item2, Wall4.West));
			}
		}
	}
}
