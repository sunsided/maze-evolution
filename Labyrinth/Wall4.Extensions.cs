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

			int widthX = wallArray.GetLength(0);
			int widthY = wallArray.GetLength(1);
			StringBuilder builder = new StringBuilder();
			
			// Rendern
			for (int y = 0; y < widthY; ++y )
			{
				for (int x = 0; x < widthX; ++x )
				{
					Wall4 value = wallArray[x, y];
					switch(value)
					{
						case Wall4.All:
							builder.Append(" ");
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
		public static bool ContainsWall(this Wall4 item, Wall4 wall)
		{
			return (item & wall) == wall;
		}
	}
}
