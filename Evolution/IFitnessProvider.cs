namespace Evolution
{
	/// <summary>
	/// Interface für Klassen, die die Fitness liefern können
	/// </summary>
	public interface IFitnessProvider
	{
		/// <summary>
		/// Bezieht die Fitness.
		/// Höhere Werte bedeuten höhere Fitness.
		/// </summary>
		/// <returns>Der Fitness-Faktor</returns>
		double GetFitness();
	}
}
