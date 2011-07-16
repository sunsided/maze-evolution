using System;

namespace Labyrinth
{
	/// <summary>
	/// Ereignisparameter für einen Labyrinthereignis
	/// </summary>
	/// <remarks></remarks>
	public class MazeEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the report.
		/// </summary>
		/// <remarks></remarks>
		public Maze4 Maze { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MazeEventArgs"/> class.
		/// </summary>
		/// <param name="maze">The maze.</param>
		/// <remarks></remarks>
		public MazeEventArgs(Maze4 maze)
		{
			Maze = maze;
		}
	}
}
