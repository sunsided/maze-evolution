namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Testklasse für die Evolution
	/// </summary>
	[EvolutionaryClass]
	public class SimpleClass
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
