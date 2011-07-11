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
		public bool Decision1()
		{
			return true;
		}

		/// <summary>
		/// Eine Entscheidungsfunktion
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public bool Decision2()
		{
			return false;
		}

		/// <summary>
		/// Generelle Methode
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public void Action1()
		{
		}

		/// <summary>
		/// Generelle Methode
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public void Action2()
		{
		}
	}
}
