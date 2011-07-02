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
							builder.Append("╬"); // 256C
							break;

						case Wall4.North:
							builder.Append("╦"); // 2566
							break;
						case Wall4.North | Wall4.South:
							builder.Append("═"); // 2550
							break;
						case Wall4.North | Wall4.East:
							builder.Append("╗"); // 2557
							break;
						case Wall4.North | Wall4.West:
							builder.Append("╔"); // 2554
							break;

						case Wall4.South:
							builder.Append("╩"); // 2569
							break;
						case Wall4.South | Wall4.East:
							builder.Append("╝"); // 255D
							break;
						case Wall4.South | Wall4.West:
							builder.Append("╚"); // 255A
							break;

						case Wall4.East:
							builder.Append("╣"); // 2563
							break;
						case Wall4.East | Wall4.West:
							builder.Append("║"); // 2551
							break;
						
						case Wall4.East | Wall4.West | Wall4.South:
							builder.Append("╨"); // 2568
							break;
						case Wall4.East | Wall4.West | Wall4.North:
							builder.Append("╥"); // 2565
							break;

						case Wall4.North | Wall4.South| Wall4.West:
							builder.Append("╞"); // 255E
							break;
						case Wall4.North | Wall4.South | Wall4.East:
							builder.Append("╡"); // 2561
							break;

						case Wall4.West:
							builder.Append("╠"); // 2560
							break;
					}
				}
				builder.AppendLine();
			}

			return builder.ToString().Trim();
		}
	}
}
