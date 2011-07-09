using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Labyrinth
{
	/// <summary>
	/// Helfermethoden für <see cref="Wall4"/>
	/// </summary>
	public static class Wall4Extensions
	{
		/// <summary>
		/// Gibt das <see cref="Wall4"/>-Array als String aus
		/// </summary>
		/// <param name="wallArray">Das Array</param>
		/// <returns></returns>
		[Pure]
		public static string RenderToString(this Wall4[,] wallArray)
		{
			Contract.Requires(wallArray != null);
			Contract.Ensures(Contract.Result<string>() != null);
			return RenderToString(wallArray, true);
		}

		/// <summary>
		/// Gibt das <see cref="Wall4"/>-Array als String aus
		/// </summary>
		/// <param name="wallArray">Das Array</param>
		/// <param name="showIndices">Gibt an, ob die Indizes angezeigt werden sollen</param>
		/// <returns></returns>
		[Pure]
		public static string RenderToString(this Wall4[,] wallArray, bool showIndices)
		{
			Contract.Requires(wallArray != null);
			Contract.Ensures(Contract.Result<string>() != null);

			int widthX = wallArray.GetLength(0);
			int widthY = wallArray.GetLength(1);
			StringBuilder builder = new StringBuilder();

			// Rendern
			if (showIndices)
			{
				builder.Append("\\");
				for (int x = 0; x < widthX; ++x)
				{
					builder.Append(x % 10);
				}
				builder.AppendLine();
			}
			for (int y = 0; y < widthY; ++y )
			{
				if (showIndices) builder.Append(y % 10);
				for (int x = 0; x < widthX; ++x )
				{
					Wall4 value = wallArray[x, y];
					switch(value)
					{
						case Wall4.All:
							builder.Append("\u2591"); // ░
							break;

						case Wall4.None:
							builder.Append("\u256c"); // ╬
							break;

						case Wall4.North:
							builder.Append("\u2566"); // ╦
							break;
						case Wall4.North | Wall4.South:
							builder.Append("\u2550"); // ═
							break;
						case Wall4.North | Wall4.East:
							builder.Append("\u2557"); // ╗
							break;
						case Wall4.North | Wall4.West:
							builder.Append("\u2554"); // ╔
							break;

						case Wall4.South:
							builder.Append("\u2569"); // ╩
							break;
						case Wall4.South | Wall4.East:
							builder.Append("\u255D"); // ╝
							break;
						case Wall4.South | Wall4.West:
							builder.Append("\u255A"); // ╚
							break;

						case Wall4.East:
							builder.Append("\u2563"); // ╣
							break;
						case Wall4.East | Wall4.West:
							builder.Append("\u2551"); // ║
							break;
						
						case Wall4.East | Wall4.West | Wall4.South:
							builder.Append("\u2568"); // ╨
							break;
						case Wall4.East | Wall4.West | Wall4.North:
							builder.Append("\u2565"); // ╥
							break;

						case Wall4.North | Wall4.South| Wall4.West:
							builder.Append("\u255E"); // ╞
							break;
						case Wall4.North | Wall4.South | Wall4.East:
							builder.Append("\u2561"); // ╡
							break;

						case Wall4.West:
							builder.Append("\u2560"); // ╠
							break;
					}
				}
				builder.AppendLine();
			}

			return builder.ToString().Trim();
		}

		/// <summary>
		/// Ermittelt, ob die in <paramref name="wall"/> gegebenen Wände vorhanden sind.
		/// </summary>
		/// <param name="item">Das Referenzelement</param>
		/// <param name="wall">Die zu testenden Wände</param>
		/// <returns><c>true</c>, wenn vorhanden, ansonsten <c>false</c></returns>
		[Pure]
		public static bool ContainsWall(this Wall4 item, Wall4 wall)
		{
			return (item & wall) == wall;
		}

		/// <summary>
		/// Ermittelt, ob die in <paramref name="door"/> gegebenen Türen vorhanden sind.
		/// </summary>
		/// <param name="item">Das Referenzelement</param>
		/// <param name="door">Die zu testenden Türen</param>
		/// <returns><c>true</c>, wenn vorhanden, ansonsten <c>false</c></returns>
		[Pure]
		public static bool ContainsDoor(this Door4 item, Door4 door)
		{
			return (item & door) == door;
		}

		/// <summary>
		/// Ermittelt, ob exakt ein Element gesetzt ist
		/// </summary>
		/// <param name="item">Der zu testende Wert</param>
		/// <returns><c>true</c>, wenn exakt ein Element gesetzt ist, ansonsten <c>false</c></returns>
		[Pure]
		public static bool ExactlyOneValueSet(this Enum item)
		{
			if (item == null) return false;

			// Die Idee ist, den Zahlenwert des Flags von allen
			// rechtsseitigen Nullen zu befreien und dann zu testen,
			// ob die noch gesetzten Bits einen Zahlenwert gleich 1 haben.
			// 0100 --> 0001 --> 1 --> true  (genau eine Wand)
			// 1100 --> 0011 --> 3 --> false (mehr als eine Wand)
			// 0000 --> 0000 --> 0 --> false (keine Wand)

			int value = (int)(object)item;
			if (value == 0) return false;
			while ((value & 1) == 0) value = value >> 1;
			return value == 1;
		}

		/// <summary>
		/// Wandelt einen <see cref="Door4"/>-Wert in einen <see cref="Wall4"/>-Wert um
		/// </summary>
		/// <param name="door">Der umzuwandeldne Wert</param>
		/// <returns></returns>
		public static Wall4 ToWall(this Door4 door)
		{
			Wall4 wall = Wall4.None;
			if (!door.ContainsDoor(Door4.North)) wall |= Wall4.North;
			if (!door.ContainsDoor(Door4.South)) wall |= Wall4.South;
			if (!door.ContainsDoor(Door4.East)) wall |= Wall4.East;
			if (!door.ContainsDoor(Door4.West)) wall |= Wall4.West;
			return wall;
		}

		/// <summary>
		/// Wandelt einen <see cref="Wall4"/>-Wert in einen <see cref="Door4"/>-Wert um
		/// </summary>
		/// <param name="wall">Der umzuwandeldne Wert</param>
		/// <returns></returns>
		public static Door4 ToDoor(this Wall4 wall)
		{
			Door4 door = Door4.None;
			if (!wall.ContainsWall(Wall4.North)) door |= Door4.North;
			if (!wall.ContainsWall(Wall4.South)) door |= Door4.South;
			if (!wall.ContainsWall(Wall4.East)) door |= Door4.East;
			if (!wall.ContainsWall(Wall4.West)) door |= Door4.West;
			return door;
		}

		/// <summary>
		/// Wandelt ein <see cref="Wall4"/>-Array in ein <see cref="Door4"/>-Array um
		/// </summary>
		/// <param name="walls">Die Wände.</param>
		/// <returns>Das <see cref="Door4"/>-Array</returns>
		/// <remarks></remarks>
		public static Door4[,] ToDoorArray(this Wall4[,] walls)
		{
			Contract.Requires(walls != null);
			Contract.Ensures(Contract.Result<Door4[,]>() != null);

			int width = walls.GetLength(0);
			int height = walls.GetLength(1);
			Door4[,] doors = new Door4[width, height];
			for (int y=0; y<height; ++y)
			{
				for (int x = 0; x < height; ++x)
				{
					doors[x, y] = walls[x, y].ToDoor();
				}
			}
			return doors;
		}

		/// <summary>
		/// Wandelt ein <see cref="Door4"/>-Array in ein <see cref="Wall4"/>-Array um
		/// </summary>
		/// <param name="doors">Die Türen.</param>
		/// <returns>Das <see cref="Door4"/>-Array</returns>
		/// <remarks></remarks>
		public static Wall4[,] ToWallArray(this Door4[,] doors)
		{
			Contract.Requires(doors != null);
			Contract.Ensures(Contract.Result<Wall4[,]>() != null);

			int width = doors.GetLength(0);
			int height = doors.GetLength(1);
			Wall4[,] walls = new Wall4[width, height];
			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < height; ++x)
				{
					walls[x, y] = doors[x, y].ToWall();
				}
			}
			return walls;
		}
	}
}
