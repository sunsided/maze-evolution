namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Klasse, die nicht als Evo-Klasse markiert ist
	/// </summary>
	public class UnmarkedClass : IFitnessProvider
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
