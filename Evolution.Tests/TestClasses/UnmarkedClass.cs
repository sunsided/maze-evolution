namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Klasse, die nicht als Evo-Klasse markiert ist
	/// </summary>
	public class UnmarkedClass : IFitnessProvider, ICodeProvider<UnmarkedClass>
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
		/// Ermittelt, ob das Element verhungert ist, d.h. unabhängig
		/// von der Fitness aus dem Pool genommen werden soll.
		/// </summary>
		/// <remarks></remarks>
		public bool IsStarved
		{
			get { return false; }
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

		/// <summary>
		/// Gets the code.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public CodeExpression<UnmarkedClass> GetCode()
		{
			throw new System.NotImplementedException();
		}
	}
}
