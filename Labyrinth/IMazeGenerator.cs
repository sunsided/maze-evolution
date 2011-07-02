using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Interface für einen Labyrinthgenerator
	/// </summary>
	[ContractClass(typeof(MazeGeneratorCotnract))]
	public interface IMazeGenerator
	{
		/// <summary>
		/// Erzeugt ein Labyrinth der Dimensionen <paramref name="width"/>x<paramref name="height"/>.
		/// </summary>
		/// <param name="width">Die Breite des Labyrinths in Zellen</param>
		/// <param name="height">Die Höhe des Labyrinths in Zellen</param>
		/// <returns>Das Labyrinth</returns>
		Wall4[,] Generate(int width, int height);
	}

	/// <summary>
	/// Vertrag für <see cref="IMazeGenerator"/>
	/// </summary>
	[ContractClassFor(typeof(IMazeGenerator))]
	internal abstract class MazeGeneratorCotnract : IMazeGenerator
	{
		private MazeGeneratorCotnract() {}

		/// <summary>
		/// Erzeugt ein Labyrinth der Dimensionen <paramref name="width"/>x<paramref name="height"/>.
		/// </summary>
		/// <param name="width">Die Breite des Labyrinths in Zellen</param>
		/// <param name="height">Die Höhe des Labyrinths in Zellen</param>
		/// <returns>Das Labyrinth</returns>
		public Wall4[,] Generate(int width, int height)
		{
			Contract.Requires(width > 0 && height > 0);
			Contract.Ensures(Contract.Result<Wall4[,]>() != null);
			return null;
		}
	}
}