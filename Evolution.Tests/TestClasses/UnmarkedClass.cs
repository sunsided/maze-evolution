namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Klasse, die nicht als Evo-Klasse markiert ist
	/// </summary>
	public class UnmarkedClass
	{
		/// <summary>
		/// Eine Entscheidungsfunktion
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public bool Decision()
		{
			return true;
		}

		/// <summary>
		/// Generelle Methode
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public void Action()
		{
		}
	}
}
