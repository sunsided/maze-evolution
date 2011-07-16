namespace Evolution.Tests.TestClasses
{
	/// <summary>
	/// Testklasse für die Evolution
	/// </summary>
	[EvolutionaryClass]
	public class InvalidSignatureClass : SimpleClass, ICodeProvider<InvalidSignatureClass>
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

		/// <summary>
		/// Gets the code.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public CodeExpression<InvalidSignatureClass> GetCode()
		{
			throw new System.NotImplementedException();
		}
	}
}
