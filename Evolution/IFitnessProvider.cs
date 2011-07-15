namespace Evolution
{
	/// <summary>
	/// Interface für Klassen, die die Fitness liefern können
	/// </summary>
	public interface IFitnessProvider
	{
        /// <summary>
        /// Ermittelt, ob das Element verhungert ist, d.h. unabhängig
        /// von der Fitness aus dem Pool genommen werden soll.
        /// </summary>
	    bool IsStarved { get; }

		/// <summary>
		/// Bezieht die Fitness.
		/// Höhere Werte bedeuten höhere Fitness.
		/// </summary>
		/// <returns>Der Fitness-Faktor</returns>
		double GetFitness();
	}
}
