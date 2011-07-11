namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Testklasse für die Evolution
	/// </summary>
	[EvolutionaryClass]
	public class InvalidSignatureClass : SimpleClass
	{
		/// <summary>
		/// Ungültige Signatur: Falscher Rückgabewert
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public double Calculation()
		{
			return 0;
		}

		/// <summary>
		/// Ungültige Signatur: Zu viele Parameter
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public void Calculation(double parameter)
		{
		}
	}
}
