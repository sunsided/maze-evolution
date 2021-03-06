﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Implementierung des Recursive Backtracking-Algorithmus
	/// </summary>
	public sealed class RecursiveBacktracker4 : MazeGenerator4
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RecursiveBacktracker4"/> class.
		/// </summary>
		/// <remarks></remarks>
		public RecursiveBacktracker4()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RecursiveBacktracker4"/> class.
		/// </summary>
		/// <remarks></remarks>
		public RecursiveBacktracker4(int seed)
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

			// Array vorbereiten und Affentest durchführen
			Wall4[,] cells = PrepareWalls(width, height);

			// Besuchskarte erzeugen
			bool[,] visitMap = new bool[width,height];

			// Backtracking starten
			Stack<Tuple<int, int>> backtrace = new Stack<Tuple<int, int>>();
			Backtrack(ref cells, ref visitMap, new Tuple<int, int>(startCellWidthCoord, startCellHeightCoord), backtrace);
			return cells;
		}

		/// <summary>
		/// Der eigentliche Backtracking-Algorithmus
		/// </summary>
		/// <param name="cells">Die Liste der Zellen</param>
		/// <param name="visitMap">Die Besuchskarte</param>
		/// <param name="currentCell">Die aktuelle Position</param>
		/// <param name="backtrace">Der Stack für die Zurückverfolgung</param>
		private void Backtrack(ref Wall4[,] cells, ref bool[,] visitMap, Tuple<int, int> currentCell, Stack<Tuple<int, int>> backtrace)
		{
			Contract.Requires(cells != null);
			Contract.Requires(visitMap != null);
			Contract.Requires(currentCell != null);
			Contract.Requires(backtrace != null);
			Contract.Ensures(cells != null);
			Contract.Ensures(visitMap != null);

			do
			{
				int widthCoord = currentCell.Item1;
				int heightCoord = currentCell.Item2;

				// 1. Mark the current cell as 'Visited'
				visitMap[widthCoord, heightCoord] = true;

				// 2. If the current cell has any neighbours which have not been visited 
				//	2.1 Choose randomly one of the unvisited neighbours
				Tuple<int, int> selectedCell;
				if ((selectedCell = SelectRandomUnvisitedNeighbor(ref visitMap, widthCoord, heightCoord)) != null)
				{
					//	2.2 add the current cell to the stack
					backtrace.Push(currentCell);

					//  2.3 remove the wall between the current cell and the chosen cell
					RemoveWallBetween(ref cells, currentCell, selectedCell);

					//  2.4 Make the chosen cell the current cell
					currentCell = selectedCell;
					
					//  2.5 Recursively call this function
					continue;
				}
				
				// 3. else
				//	3.1 remove the last current cell from the stack
				if (backtrace.Count == 0) break;
				currentCell = backtrace.Pop();

				//  3.2 Backtrack to the previous execution of this function
			} while (true);
		}

		/// <summary>
		/// Sucht einen zufälligen, unbesuchten Nachbarn aus
		/// </summary>
		/// <param name="heightCoord">Die Höhenkoordinate der gewünschten Zelle</param>
		/// <param name="widthCoord">Die Breitenkoordinate der gewünschten Zelle</param>
		/// <param name="visitMap">Die Map der besuchten Zellen</param>
		/// <returns>Die Koordinaten der Zelle oder <c>null</c>, falls keine freien Nachbarn gefunden wurden</returns>
		private Tuple<int, int> SelectRandomUnvisitedNeighbor(ref bool[,] visitMap, int widthCoord, int heightCoord)
		{
			Contract.Requires(visitMap != null);
			Contract.Ensures(visitMap != null);

			int maxWidthCoord = visitMap.GetLength(0) - 1;
			int maxHeightCoord = visitMap.GetLength(1) - 1;
			if (widthCoord < 0 || widthCoord > maxWidthCoord) return null;
			if (heightCoord < 0 || heightCoord > maxHeightCoord) return null;

			// Basiswert ermitteln
			int value = Randomizer.Next(0, 40);

			// Wert finden
			IList<Tuple<int, int>> neighbors = SelectUnvisitedNeighbors(ref visitMap, widthCoord, heightCoord);
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
		private static void AddIfUnvisited(ICollection<Tuple<int, int>> neighbors, bool[,] visitMap, int widthCoord, int heightCoord)
		{
			Contract.Requires(visitMap != null);
			Contract.Requires(neighbors != null);
			Contract.Ensures(visitMap != null);
			Contract.Ensures(neighbors != null);

			if (widthCoord < 0 || widthCoord >= visitMap.GetLength(0)) return;
			if (heightCoord < 0 || heightCoord >= visitMap.GetLength(1)) return;
			if (!visitMap[widthCoord, heightCoord]) neighbors.Add(new Tuple<int, int>(widthCoord, heightCoord));
		}

		/// <summary>
		/// Listet alle unbesuchten Nachbarn auf
		/// </summary>
		/// <param name="heightCoord">Die Höhenkoordinate der gewünschten Zelle</param>
		/// <param name="widthCoord">Die Breitenkoordinate der gewünschten Zelle</param>
		/// <param name="visitMap">Die Map der besuchten Zellen</param>
		/// <returns>Die Liste der unbesuchten Nachbarn</returns>
		private static IList<Tuple<int, int>> SelectUnvisitedNeighbors(ref bool[,] visitMap, int widthCoord, int heightCoord)
		{
			Contract.Requires(visitMap != null);
			Contract.Ensures(Contract.Result<IList<Tuple<int, int>>>() != null);
			Contract.Ensures(visitMap != null);

			if (widthCoord < 0 || widthCoord > visitMap.GetLength(0)) return new Tuple<int, int>[0];
			if (heightCoord < 0 || heightCoord > visitMap.GetLength(1)) return new Tuple<int, int>[0];

			// Hilfsliste erzeugen
			List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>(4);
			AddIfUnvisited(neighbors, visitMap, widthCoord, heightCoord - 1);
			AddIfUnvisited(neighbors, visitMap, widthCoord - 1, heightCoord);
			AddIfUnvisited(neighbors, visitMap, widthCoord + 1, heightCoord);
			AddIfUnvisited(neighbors, visitMap, widthCoord, heightCoord + 1);

			// Und raus damit
			return neighbors.ToArray();
		}
	}
}
