namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Testklasse für die Evolution
	/// </summary>
	[EvolutionaryClass]
	public class SimpleClass : IFitnessProvider
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

		/// <summary>
		/// Bezieht die Fitness.
		/// Höhere Werte bedeuten höhere Fitness.
		/// </summary>
		/// <returns>Der Fitness-Faktor</returns>
		/// <remarks></remarks>
		public double GetFitness()
		{
			return 0;
		}
	}
}
