using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Labyrinth
{
	/// <summary>
	/// Implementierung des Recursive Backtracking-Algorithmus
	/// </summary>
	public static class RecursiveBacktracker4
	{
		/// <summary>
		/// Erzeugt ein Labyrinth der Dimensionen <paramref name="width"/>x<paramref name="height"/>.
		/// </summary>
		/// <param name="width">Die Breite des Labyrinths in Zellen</param>
		/// <param name="height">Die Höhe des Labyrinths in Zellen</param>
		/// <returns>Das Labyrinth</returns>
		public static Wall4[,] Generate(int width, int height)
		{
			Contract.Requires(width > 0);
			Contract.Requires(height > 0);
			Contract.Ensures(Contract.Result<Wall4[,]>() != null);

			// Startkoordinaten
			const int startCellWidthCoord = 0;
			const int startCellHeightCoord = 0;

			// Array vorbereiten und Affentest durchführen
			Wall4[,] cells = new Wall4[width, height];
			if (width == 1 || height == 1) return cells;

			// Besuchskarte erzeugen
			bool[,] visitMap = new bool[width,height];

			// Backtracking starten
			Stack<Tuple<int, int>> backtrace = new Stack<Tuple<int, int>>();
			Backtrack(cells, visitMap, new Tuple<int, int>(startCellWidthCoord, startCellHeightCoord), backtrace);
			return cells;
		}

		/// <summary>
		/// Der eigentliche Backtracking-Algorithmus
		/// </summary>
		/// <param name="cells">Die Liste der Zellen</param>
		/// <param name="visitMap">Die Besuchskarte</param>
		/// <param name="currentCell">Die aktuelle Position</param>
		/// <param name="backtrace">Der Stack für die Zurückverfolgung</param>
		private static void Backtrack(Wall4[,] cells, bool[,] visitMap, Tuple<int, int> currentCell, Stack<Tuple<int, int>> backtrace)
		{
			Contract.Requires(cells != null);
			Contract.Requires(visitMap != null);
			Contract.Requires(currentCell != null);
			Contract.Requires(backtrace != null);

			int widthCoord = currentCell.Item1;
			int heightCoord = currentCell.Item2;

			// 1. Mark the current cell as 'Visited'
			visitMap[widthCoord, heightCoord] = true;

			// 2. If the current cell has any neighbours which have not been visited 
			//	2.1 Choose randomly one of the unvisited neighbours
			var selectedCell = visitMap.SelectRandomUnvisitedNeighbor(widthCoord, heightCoord);
			if (selectedCell != null)
			{
				//	2.2 add the current cell to the stack
				backtrace.Push(currentCell);

				//  2.3 remove the wall between the current cell and the chosen cell
				RemoveWallBetween(cells, currentCell, selectedCell);

				//  2.4 Make the chosen cell the current cell
				//  2.5 Recursively call this function
				Backtrack(cells, visitMap, selectedCell, backtrace);
			}
			else // 3. else 
			{
				//	3.1 remove the last current cell from the stack
				//  3.2 Backtrack to the previous execution of this function
				if (backtrace.Count > 0 ) Backtrack(cells, visitMap, backtrace.Pop(), backtrace);
			}
		}

		/// <summary>
		/// Entfernt die Wand zwischen den beiden Zellen
		/// </summary>
		/// <param name="cells">Die Zellen</param>
		/// <param name="current">Die aktuelle Zelle</param>
		/// <param name="selected">Die ausgewählte Zelle</param>
		private static void RemoveWallBetween(Wall4[,] cells, Tuple<int, int> current, Tuple<int, int> selected)
		{
			Contract.Requires(cells != null);
			Contract.Requires(current != null);
			Contract.Requires(selected != null);

			// X-Achse
			if (current.Item1 > selected.Item1)
			{
				cells[selected.Item1, selected.Item2] = cells[selected.Item1, selected.Item2] & ~Wall4.West;
				cells[current.Item1, current.Item2] = cells[current.Item1, current.Item2] & ~Wall4.East;
			}
			else if (current.Item1 < selected.Item1)
			{
				cells[selected.Item1, selected.Item2] = cells[selected.Item1, selected.Item2] & ~Wall4.East;
				cells[current.Item1, current.Item2] = cells[current.Item1, current.Item2] & ~Wall4.West;
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

		/// <summary>
		/// Randomizer für die Nachbarschaftsauswahl
		/// </summary>
		private static readonly Lazy<Random> Randomizer = new Lazy<Random>(() => new Random(), LazyThreadSafetyMode.None);

		/// <summary>
		/// Sucht einen zufälligen, unbesuchten Nachbarn aus
		/// </summary>
		/// <param name="heightCoord">Die Höhenkoordinate der gewünschten Zelle</param>
		/// <param name="widthCoord">Die Breitenkoordinate der gewünschten Zelle</param>
		/// <param name="visitMap">Die Map der besuchten Zellen</param>
		/// <returns>Die Koordinaten der Zelle oder <c>null</c>, falls keine freien Nachbarn gefunden wurden</returns>
		private static Tuple<int, int> SelectRandomUnvisitedNeighbor(this bool[,] visitMap, int widthCoord, int heightCoord)
		{
			Contract.Requires(visitMap != null);

			int maxWidthCoord = visitMap.GetLength(0) - 1;
			int maxHeightCoord = visitMap.GetLength(1) - 1;
			if (widthCoord < 0 || widthCoord > maxWidthCoord) return null;
			if (heightCoord < 0 || heightCoord > maxHeightCoord) return null;

			// Basiswert ermitteln
			int value = Randomizer.Value.Next(0, 40);

			// Wert finden
			IList<Tuple<int, int>> neighbors = SelectUnvisitedNeibhbors(visitMap, widthCoord, heightCoord);
			if (neighbors.Count == 0) return null;
			return neighbors[value%neighbors.Count];
		}

		/// <summary>
		/// Fügt die Zellenkoodinaten zu einer Liste hinzu, wenn die Zelle unbesucht ist
		/// </summary>
		/// <param name="heightCoord">Die Höhenkoordinate der gewünschten Zelle</param>
		/// <param name="widthCoord">Die Breitenkoordinate der gewünschten Zelle</param>
		/// <param name="neighbors">Die Liste der Nachbarn</param>
		/// <param name="visitMap">Die Map der besuchten Zellen</param>
		private static void AddIfUnvisited(this IList<Tuple<int, int>> neighbors, bool[,] visitMap, int widthCoord, int heightCoord)
		{
			Contract.Requires(visitMap != null);
			Contract.Requires(neighbors != null);

			if (widthCoord < 0 || widthCoord > visitMap.GetLength(0)) return;
			if (heightCoord < 0 || heightCoord > visitMap.GetLength(1)) return;
			if (visitMap[widthCoord, heightCoord]) neighbors.Add(new Tuple<int, int>(widthCoord, heightCoord));
		}

		/// <summary>
		/// Listet alle unbesuchten Nachbarn auf
		/// </summary>
		/// <param name="heightCoord">Die Höhenkoordinate der gewünschten Zelle</param>
		/// <param name="widthCoord">Die Breitenkoordinate der gewünschten Zelle</param>
		/// <param name="visitMap">Die Map der besuchten Zellen</param>
		/// <returns>Die Liste der unbesuchten Nachbarn</returns>
		private static IList<Tuple<int, int>> SelectUnvisitedNeibhbors(this bool[,] visitMap, int widthCoord, int heightCoord)
		{
			Contract.Requires(visitMap != null);
			Contract.Ensures(Contract.Result<IList<Tuple<int, int>>>() != null);

			if (widthCoord < 0 || widthCoord > visitMap.GetLength(0)) return new Tuple<int, int>[0];
			if (heightCoord < 0 || heightCoord > visitMap.GetLength(1)) return new Tuple<int, int>[0];

			// Hilfsliste erzeugen
			List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>(4);
			neighbors.AddIfUnvisited(visitMap, widthCoord, heightCoord - 1);
			neighbors.AddIfUnvisited(visitMap, widthCoord - 1, heightCoord);
			neighbors.AddIfUnvisited(visitMap, widthCoord + 1, heightCoord);
			neighbors.AddIfUnvisited(visitMap, widthCoord, heightCoord + 1);

			// Und raus damit
			return neighbors.ToArray();
		}
	}
}
