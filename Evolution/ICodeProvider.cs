namespace Evolution
{
	/// <summary>
	/// Interface für Klassen, die die Fitness liefern können
	/// </summary>
    public interface ICodeProvider<T> where T: class
	{
		/// <summary>
		/// Bezieht die Fitness.
		/// Höhere Werte bedeuten höhere Fitness.
		/// </summary>
		/// <returns>Der Fitness-Faktor</returns>
        CodeExpression<T> GetCode();
	}
}
