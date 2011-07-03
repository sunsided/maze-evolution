using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Implementierung des Randomized Prim-Algorithmus
	/// </summary>
	public sealed class RandomizedPrim4 : MazeGenerator4
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
			// Startkoordinaten
			const int startCellWidthCoord = 0;
			const int startCellHeightCoord = 0;
			
			// 1. Start with a grid full of walls.
			Wall4[,] cells = PrepareWalls(width, height);
			if (height == 1 && width == 1) return cells;

			// Listen vorbereiten
			bool[,] mazeMap = new bool[width, height];
			List<Tuple<int, int, Wall4>> wallList = new List<Tuple<int, int, Wall4>>();

			// 2. Pick a cell, mark it as part of the maze. Add the walls of the cell to the wall list.
			mazeMap[startCellWidthCoord, startCellHeightCoord] = true;
			AddWallsToWallList(ref cells, new Tuple<int, int>(startCellWidthCoord, startCellHeightCoord), ref wallList);

			// 3. While there are walls in the list: 
			while (wallList.Count > 0)
			{
				//	3.1 Pick a random wall from the list. If the cell on the opposite side isn't in the maze yet: 
				var wall = PickAndRemoveRandomWall(wallList);
				Contract.Assume(wall != null);

				Tuple<int, int> oppositeCell;
				if (!CellOnOppositeSideOfWallIsOnMaze(mazeMap, wall, out oppositeCell))
				{
					//		3.1.1 Make the wall a passage and mark the cell on the opposite side as part of the maze.
					RemoveWallBetween(ref cells, new Tuple<int, int>(wall.Item1, wall.Item2), oppositeCell);
					Contract.Assume(oppositeCell.Item1 >= 0);
					Contract.Assume(oppositeCell.Item2 >= 0);
					mazeMap[oppositeCell.Item1, oppositeCell.Item2] = true;

					//		3.1.2 Add the neighboring walls of the cell to the wall list.
					AddWallsToWallList(ref cells, oppositeCell, ref wallList);
				}
				//	3.2 If the cell on the opposite side already was in the maze, remove it from the list.
			}
			return cells;
		}

		/// <summary>
		/// Ermittelt, ob die Zelle, die gegenüber der in <paramref name="wall.Item3"/> angegebenen Wand der Zelle an Position
		/// <paramref name="wall"/> liegt, bereits Teil des Labyrinths ist.
		/// </summary>
		/// <param name="mazeMap">Die Karte, die angibt, ob eine Zelle Teil des Labyrinthes ist</param>
		/// <param name="wall">Die zu testende Wand</param>
		/// <param name="oppositeCell">Die Zelle auf der gegenüberliegenden Seite</param>
		/// <returns><c>true</c>, wenn die Zelle auf der anderen Seite Teil des Labyrinthes ist, ansonsten <c>false</c></returns>
		/// <exception cref="ArgumentException">Ein ungültiger Wert für <paramref name="wall"/> wurde übergeben.</exception>
		private static bool CellOnOppositeSideOfWallIsOnMaze(bool[,] mazeMap, Tuple<int, int, Wall4> wall, out Tuple<int, int> oppositeCell)
		{
			Contract.Requires(mazeMap != null);
			Contract.Requires(wall != null);
			Contract.Requires(wall.Item3.ExactlyOneWallSet());
			Contract.Requires(wall.Item1 >= 0 && wall.Item2 >= 0);
			Contract.Ensures(Contract.ValueAtReturn(out oppositeCell) != null);

			return CellOnOppositeSideOfWallIsOnMaze(mazeMap, new Tuple<int, int>(wall.Item1, wall.Item2), wall.Item3, out oppositeCell);
		}

		/// <summary>
		/// Ermittelt, ob die Zelle, die gegenüber der in <paramref name="wall"/> angegebenen Wand der Zelle an Position
		/// <paramref name="cell"/> liegt, bereits Teil des Labyrinths ist.
		/// </summary>
		/// <param name="mazeMap">Die Karte, die angibt, ob eine Zelle Teil des Labyrinthes ist</param>
		/// <param name="cell">Die aktuelle Position</param>
		/// <param name="wall">Die zu testende Wand</param>
		/// <param name="oppositeCell">Die Zelle auf der gegenüberliegenden Seite</param>
		/// <returns><c>true</c>, wenn die Zelle auf der anderen Seite Teil des Labyrinthes ist, ansonsten <c>false</c></returns>
		/// <exception cref="ArgumentException">Ein ungültiger Wert für <paramref name="wall"/> wurde übergeben.</exception>
		private static bool CellOnOppositeSideOfWallIsOnMaze(bool[,] mazeMap, Tuple<int, int> cell, Wall4 wall, out Tuple<int, int> oppositeCell)
		{
			Contract.Requires(mazeMap != null);
			Contract.Requires(cell != null);
			Contract.Requires(wall.ExactlyOneWallSet());
			Contract.Requires(cell.Item1 >= 0 && cell.Item2 >= 0);
			Contract.Ensures(Contract.ValueAtReturn(out oppositeCell) != null);

			switch (wall)
			{
				case Wall4.North:
					Contract.Assume(cell.Item2 > 0);
					oppositeCell = new Tuple<int, int>(cell.Item1, cell.Item2 - 1);
					return mazeMap[oppositeCell.Item1, oppositeCell.Item2];
				case Wall4.South:
					Contract.Assume(cell.Item2 < mazeMap.GetLength(1) - 1);
					oppositeCell = new Tuple<int, int>(cell.Item1, cell.Item2 + 1);
					return mazeMap[oppositeCell.Item1, oppositeCell.Item2];
				case Wall4.East:
					Contract.Assume(cell.Item1 < mazeMap.GetLength(0) - 1);
					oppositeCell = new Tuple<int, int>(cell.Item1 + 1, cell.Item2);
					return mazeMap[oppositeCell.Item1, oppositeCell.Item2];
				case Wall4.West:
					Contract.Assume(cell.Item1 > 0);					
					oppositeCell = new Tuple<int, int>(cell.Item1 - 1, cell.Item2);
					return mazeMap[oppositeCell.Item1, oppositeCell.Item2];
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
		private static void AddWallsToWallList(ref Wall4[,] cells, Tuple<int, int> cell, ref List<Tuple<int, int, Wall4>> wallList)
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

		/// <summary>
		/// Wählt eine zufällige Wand, entfernt diese aus der Liste und gibt die Wand zurück
		/// </summary>
		/// <param name="wallList">Die Liste der Wände</param>
		/// <returns>Das Element oder <c>null</c>, wenn die Liste leer war</returns>
		private Tuple<int, int, Wall4> PickAndRemoveRandomWall(IList<Tuple<int, int, Wall4>> wallList)
		{
			Contract.Requires(wallList != null);
			
			if (wallList.Count == 0) return null;
			int index = Randomizer.Next(0, wallList.Count - 1);
			var element = wallList[index];
			wallList.RemoveAt(index);
			return element;
		}
	}
}
