namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Testklasse für die Evolution
	/// </summary>
	[EvolutionaryClass]
	public class SimpleClass : IFitnessProvider, ICodeProvider<SimpleClass>
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
		public CodeExpression<SimpleClass> GetCode()
		{
			throw new System.NotImplementedException();
		}
	}
}
