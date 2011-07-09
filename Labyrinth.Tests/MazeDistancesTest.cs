using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Labyrinth.Tests
{
	[TestFixture]
	public class MazeDistancesTest
	{
		[Test]
		public void TestMazeDistancesFix()
		{
			Trace.AutoFlush = true;
			IMazeGenerator generator = new RecursiveBacktracker4(227);

			const int width = 10;
			const int height = 5;

			Maze4 maze = new Maze4(generator);
			maze.GenerateNew(width, height);

			Trace.WriteLine("Labyrinth:");
			Trace.WriteLine(maze.RenderToString());

			Trace.WriteLine(Environment.NewLine + "Ermittle Distanzen ...");
			int[,] distances;
			IList<Tuple<int, IRoom4>> remoteRooms = maze.SetStartingPoint(0, 0, out distances);

			if (remoteRooms.Count > 0)
			{
				Trace.WriteLine(Environment.NewLine + "Sackgassen:");
				foreach (var room in remoteRooms)
				{
					var position = maze.GetPosition(room.Item2);
					Trace.WriteLine("  + " + position.Item1 + "," + position.Item2 + " (Distanz: " + room.Item1 + ")");
				}

				Trace.WriteLine(Environment.NewLine + "Distanzkarte:");
				PrintDistanceMap(distances, remoteRooms);
			}
			else
			{
				Trace.Fail("Keine Sackgassen ermittelt.");
			}
		}

		[Test]
		public void TestMazeDistancesRandom()
		{
			Trace.AutoFlush = true;
			IMazeGenerator generator = new RecursiveBacktracker4();

			const int width = 20;
			const int height = 10;

			Maze4 maze = new Maze4(generator);
			maze.GenerateNew(width, height);

			Trace.WriteLine("Labyrinth:");
			Trace.WriteLine(maze.RenderToString());

			Trace.WriteLine(Environment.NewLine + "Ermittle Distanzen ...");
			int[,] distances;
			maze.MarcherMoved += (sender, marcher) => Trace.WriteLine("marcher at " + marcher.X + "," + marcher.Y + " facing " + marcher.CurrentDirection);
			IList<Tuple<int, IRoom4>> remoteRooms = maze.SetStartingPoint(0, 0, out distances);

			if (remoteRooms.Count > 0)
			{
				Trace.WriteLine(Environment.NewLine + "Sackgassen:");
				foreach (var room in remoteRooms)
				{
					var position = maze.GetPosition(room.Item2);
					Trace.WriteLine("  + " + position.Item1 + "," + position.Item2 + " (Distanz: " + room.Item1 + ")");
				}

				Trace.WriteLine(Environment.NewLine + "Distanzkarte:");
				PrintDistanceMap(distances, remoteRooms);
			}
			else
			{
				Trace.Fail("Keine Sackgassen ermittelt.");
			}
		}

		/// <summary>
		/// Gibt die Distanzkarte aus
		/// </summary>
		/// <param name="distances">Die Distanzkarte</param>
		/// <param name="remoteRooms">Die Sackgassen</param>
		private static void PrintDistanceMap(int[,] distances, IList<Tuple<int, IRoom4>> remoteRooms)
		{
			int width = distances.GetLength(0);
			int height = distances.GetLength(1);
			int maxDistanceFormattingSize = remoteRooms[0].Item1.ToString().Length + 1;

			// X-Indizes schreiben
			int yindexwidth = height.ToString().Length;
			for (int x = 0; x <= yindexwidth + 1; ++x)
			{
				Trace.Write(" ");
			}
			for (int x = 0; x < width; ++x)
			{
				Trace.Write(String.Format("{0," + maxDistanceFormattingSize + ":0}", x));
			}

			// Trennlinie für X-Indizes
			Trace.WriteLine("");
			for (int x = 0; x <= yindexwidth + 1; ++x)
			{
				Trace.Write(" ");
			}
			for (int x = yindexwidth; x < width*maxDistanceFormattingSize; ++x)
			{
				Trace.Write("-");
			}
			Trace.WriteLine("");

			// Karte ausgeben
			for (int y = 0; y < height; ++y)
			{
				Trace.Write(String.Format("{0," + yindexwidth + ":0} |", y));
				for (int x = 0; x < width; ++x)
				{
					Trace.Write(String.Format("{0," + maxDistanceFormattingSize + ":0}", distances[x, y]));
				}
				Trace.WriteLine("");
			}
		}
	}
}
